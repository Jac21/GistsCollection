# Installation and Usage

## Go Module

> $ mkdir maelstrom-kafka
> $ cd maelstrom-kafka
> $ go mod init maelstrom-kafka
> $ go mod tidy

> $ go get github.com/jepsen-io/maelstrom/demo/go
> $ go install .

# Maelstrom

> $ brew install openjdk graphviz gnuplot

> ./maelstrom test -w kafka --bin ~/go/bin/maelstrom-kafka --node-count 1 --concurrency 2n --time-limit 20 --rate 1000

> ./maelstrom serve
