"""

Object introspection - the ability to determine the type of object at runtime

"""
import inspect

# dir, gets all the methods associated with that type
my_list = [1,2,3]
print(dir(my_list)) #['__add__', '__class__', '...']

# type
print(type(''))
# Output: <type 'str'>

print(type([]))
# Output: <type 'list'>

print(type({}))
# Output: <type 'dict'>

print(type(dict))
# Output: <type 'type'>

print(type(3))
# Output: <type 'int'>

# unique id 
name = "Jeremy"
print(id(name))

# inspect module
print(inspect.getmembers(str)) #['__add__', <slot wrapper '...']