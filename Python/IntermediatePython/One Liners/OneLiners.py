"""

One-Liners

"""

# Simple Web Server
# python -m SimpleHTTPServer (python 2)
# python -m http.server (python 3)

# pretty printing
from pprint import pprint

my_dict = {'name': 'Jeremy', 'age': 'undefined', 'personality': 'also undefined'}
pprint(my_dict)

# cat file.json | python -m json.tool

"""
Profiling a script

This can be extremely helpful in pinpointing the bottlenecks in your scripts:

python -m cProfile my_script.py
"""

"""
CSV to json

Run this in the terminal:

python -c "import csv,json;print json.dumps(list(csv.reader(open('csv_file.csv'))))"
"""

# List flattening
import itertools

a_list = [[1, 2], [3, 4], [5, 6]]
print(list(itertools.chain.from_iterable(a_list)))
# [1, 2, 3, 4, 5, 6]

# or
print(list(itertools.chain(*a_list)))

# One-Line Constructors
class A(object):
	def __init__(self, a, b, c, d, e, f):
		self.__dict__.update({k: v for k, v in locals().items() if k != 'self'})