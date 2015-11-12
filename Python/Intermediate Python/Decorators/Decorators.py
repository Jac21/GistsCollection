"""

Decorators - help to make our code shorter and more Pythonic.

"""

def a_new_decorator(a_func):

	def wrapTheFunction():
		print("I am doing something boring before executing a_func()")

		a_func()

		print("something boring after a_func")

	return wrapTheFunction

def a_function_requiring_decoration():
	print("I am the function which needs some decoration")

a_function_requiring_decoration()
# I am the function...

a_function_requiring_decoration = a_new_decorator(a_function_requiring_decoration)
# now a_function is wrapped by wrapTheFunction()

a_function_requiring_decoration()
# i am doing something boring...