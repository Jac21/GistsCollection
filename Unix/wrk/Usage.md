# Usage

> wrk -t12 -c400 -d30s http://127.0.0.1:8080/index.html

This runs a benchmark for 30 seconds, using 12 threads, and keeping 400 HTTP connections open.

> wrk -c1 -t1 -d5s -s ./my-script.lua --latency http://localhost:8000

Runs a benchmark with a defined script.