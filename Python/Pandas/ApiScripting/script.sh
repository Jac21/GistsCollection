#!/bin/sh

curl -s https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist.zip \
| bsdtar -xOf - \
| python3 -c 'import sys, pandas as pd
pd.read_csv(sys.stdin).iloc[:, :-1].melt("Date")\
.to_csv(sys.stdout, index=False)'