"""

*args and **kwargs are used in function definitions, allow you
to pass a variable number of arguments to a function.

"""

def test_var_args(f_arg, *argv):
	print("first normal arg: ", f_arg)
	for arg in argv:
		print("Another arg through argv: ", arg)

test_var_args('python', 'test', 'function', 'testable')
print '\n'

"""

**kwargs allows one to pass keyworded variable length args, used
to handle named args in a function

"""

def greet_me(**kwargs):
	for key, value in kwargs.items():
		print("{0} == {1}".format(key, value))

greet_me(name="Jeremy")
print '\n'

"""

Using *args and **kwargs to call a function...

"""

# first with *args
def test_args_kwargs(arg1, arg2, arg3):
	print("arg1: ", arg1)
	print("arg2: ", arg2)
	print("arg3: ", arg3)

test_args_kwargs("two", 3, 5)

# now with **kwargs
kwargs = {"arg3": 3, "arg2": "two", "arg1": 5}

test_args_kwargs(**kwargs)