"""

Map - applies a function to all the items in an input list
map(function_to_apply, list_of_input)

"""

items = [1, 2, 3, 4, 5]
squared = list(map(lambda x: x**2, items))

for i in squared:
	print(i) #1, 4, 9, 16, 25

# instead of list of inputs, can have list of functions
def multiply(x):
	return (x*x)
def add(x):
	return (x+x)

funcs = [multiply, add]
for i in range(5):
	value = list(map(lambda x: x(i), funcs))
	print(value)