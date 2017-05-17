"""

Generators - 

Iterable - any object in Python which has an __iter__ or a 
__getitem__ method defined which returns an iterator or can
take indexes.

Iterator - an object in python which has a next/__next__ method
defined.

Iteration - the process of taking an item from a collection,
and looping over it

Generators are iterators that are iterated only once, values
are not stored in memory

"""

def generator_function():
	for i in range(10):
		yield i

for item in generator_function():
	print(item)

# using next()
gen  = generator_function()
print(next(gen)) #0
print(next(gen)) #1
print(next(gen)) #2

my_string = "Jeremy"
my_iter = iter(my_string)
print(next(my_iter)) #J
print(next(my_iter)) #e
print(next(my_iter)) #r

# generator for fibonacci numbers
def fibon(n):
	a = b = 1
	for i in range(n):
		yield a
		a, b = b, a + b

for x in fibon(100):
	print(x)