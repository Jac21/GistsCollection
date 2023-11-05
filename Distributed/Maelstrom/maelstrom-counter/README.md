# Installation and Usage

## Go Module

> $ mkdir maelstrom-counter
> $ cd maelstrom-counter
> $ go mod init maelstrom-counter
> $ go mod tidy

> $ go get github.com/jepsen-io/maelstrom/demo/go
> $ go install .

# Maelstrom

> $ brew install openjdk graphviz gnuplot

> ./maelstrom test -w g-counter --bin ~/go/bin/maelstrom-counter --node-count 3 --rate 100 --time-limit 20 --nemesis partition

> ./maelstrom serve
