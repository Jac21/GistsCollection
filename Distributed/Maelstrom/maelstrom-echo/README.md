# Installation and Usage

## Go Module

> $ mkdir maelstrom-echo
> $ cd maelstrom-echo
> $ go mod init maelstrom-echo
> $ go mod tidy

> $ go get github.com/jepsen-io/maelstrom/demo/go
> $ go install .

# Maelstrom

> $ brew install openjdk graphviz gnuplot

> ./maelstrom test -w echo --bin ~/go/bin/maelstrom-echo --node-count 1 --time-limit 10

> ./maelstrom serve
