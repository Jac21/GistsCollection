package main

import (
	"html/template"
	"log"
	"net/http"
	"os"
	"regexp"
)

type Page struct {
	Title string
	Body  []byte
}

// global variable utilized to cache templates, panics in error state
var templates = template.Must(template.ParseFiles("edit.html", "view.html"))

// global variable to store validation expression, panics in error state
var validPath = regexp.MustCompile("^/(edit|save|view)/([a-zA-Z0-9]+)$")

func (p *Page) save() error {
	filename := p.Title + ".txt"

	return os.WriteFile(filename, p.Body, 0600)
}

func loadPage(title string) (*Page, error) {
	filename := title + ".txt"

	body, err := os.ReadFile(filename)

	if err != nil {
		return nil, err
	}

	return &Page{
		Title: title,
		Body:  body,
	}, nil
}

func renderTemplate(w http.ResponseWriter, tmpl string, p *Page) {
	err := templates.ExecuteTemplate(w, tmpl+".html", p)

	// send HTTP error response code in the event of an error
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)

		return
	}
}

func makeHandler(fn func(http.ResponseWriter, *http.Request, string)) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		// extract the page title from the request, call the provided handler
		m := validPath.FindStringSubmatch(r.URL.Path)

		if m == nil {
			http.NotFound(w, r)

			return
		}

		fn(w, r, m[2])
	}
}

func viewHandler(w http.ResponseWriter, r *http.Request, title string) {
	p, err := loadPage(title)

	// handle non-existent pages
	if err != nil {
		http.Redirect(w, r, "/edit"+title, http.StatusFound)

		return
	}

	renderTemplate(w, "view", p)
}

func editHandler(w http.ResponseWriter, r *http.Request, title string) {
	p, err := loadPage(title)

	if err != nil {
		p = &Page{
			Title: title,
		}
	}

	renderTemplate(w, "edit", p)
}

// save pages
func saveHandler(w http.ResponseWriter, r *http.Request, title string) {
	body := r.FormValue("body")

	p := &Page{Title: title, Body: []byte(body)}
	err := p.save()

	// send HTTP error response code in the event of an error
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)

		return
	}

	http.Redirect(w, r, "/view/"+title, http.StatusFound)
}

func main() {
	http.HandleFunc("/view/", makeHandler(viewHandler))
	http.HandleFunc("/edit/", makeHandler(editHandler))
	http.HandleFunc("/save", makeHandler(saveHandler))

	log.Fatal(http.ListenAndServe(":8080", nil))
}
