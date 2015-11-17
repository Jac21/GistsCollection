"""

Coroutines - similar to generators, with a few differences:

1. Generators are data PRODUCERS
2. Coroutines are data CONSUMERS

Generator - 

def fib():
	a, b = 0, 1
	while True:
		yield a
		a, b = b, a + b

for i in fib():
	print(i)

"""

def grep(pattern):
	print("Searching for ", pattern)
	while True:
		line = (yield)
		if pattern in line:
			print(line)

search = grep('coroutine')

# required to start the coroutine
next(search)

# Output : Searching for coroutine
search.send("I love lamp.")
search.send("Don't you love coroutines?")
search.send("Yup, I love coroutines!")
# Output: Don't you love coroutines?
# Output: Yup, I love coroutines!

search.close()