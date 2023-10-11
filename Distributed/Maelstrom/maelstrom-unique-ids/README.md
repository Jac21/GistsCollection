# Installation and Usage

## Go Module

> $ mkdir maelstrom-unique-ids
> $ cd maelstrom-unique-ids
> $ go mod init maelstrom-unique-ids
> $ go mod tidy

> $ go get github.com/jepsen-io/maelstrom/demo/go
> $ go install .

# Maelstrom

> $ brew install openjdk graphviz gnuplot

> ./maelstrom test -w unique-ids --bin ~/go/bin/maelstrom-unique-ids --time-limit 30 --rate 1000 --node-count 3 --availability total --nemesis partition

> ./maelstrom serve
