package main

import (
	"fmt"
	"html/template"
	"log"
	"net/http"
	"os"
)

type Page struct {
	Title string
	Body  []byte
}

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
	t, _ := template.ParseFiles(tmpl + ".html")
	t.Execute(w, p)
}

func viewHandler(w http.ResponseWriter, r *http.Request) {
	title := r.URL.Path[len("/view/"):]

	p, err := loadPage(title)

	if err != nil {
		http.Redirect(w, r, "/edit"+title, http.StatusFound)
		return
	}

	renderTemplate(w, "view", p)
}

func editHandler(w http.ResponseWriter, r *http.Request) {
	title := r.URL.Path[len("/edit/"):]

	p, err := loadPage(title)

	if err != nil {
		p = &Page{
			Title: title,
		}
	}

	renderTemplate(w, "edit", p)
}

func saveHandler(w http.ResponseWriter, r *http.Request) {
	title := r.URL.Path[len("/save/"):]

	body := r.FormValue("body")

	p := &Page{Title: title, Body: []byte(body)}
	p.save()

	http.Redirect(w, r, "/view/"+title, http.StatusFound)
}

func main() {
	testTitle := "Test"

	pageOne := &Page{
		Title: testTitle,
		Body:  []byte("Sample page content."),
	}

	pageOne.save()

	pageTwo, _ := loadPage(testTitle)

	fmt.Println(string(pageTwo.Body))

	http.HandleFunc("/save", saveHandler)
	http.HandleFunc("/view/", viewHandler)
	http.HandleFunc("/edit/", editHandler)
	log.Fatal(http.ListenAndServe(":8080", nil))
}
