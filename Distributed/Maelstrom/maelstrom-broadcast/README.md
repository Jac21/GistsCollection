# Installation and Usage

## Go Module

> $ mkdir maelstrom-broadcast
> $ cd maelstrom-broadcast
> $ go mod init maelstrom-broadcast
> $ go mod tidy

> $ go get github.com/jepsen-io/maelstrom/demo/go
> $ go install .

# Maelstrom

> $ brew install openjdk graphviz gnuplot

> ./maelstrom test -w broadcast --bin ~/go/bin/maelstrom-broadcast --node-count 1 --time-limit 20 --rate 10

> ./maelstrom serve
