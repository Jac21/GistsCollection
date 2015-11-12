"""

set Data Structure - behave like lists, with the distinction 
that they can not duplicate values.

"""

# elegantly checking for dupes
some_list = ['a', 'b', 'c', 'b', 'd', 'm', 'n', 'n']
duplicates = set([x for x in some_list if some_list.count(x) > 1])
print(duplicates) #set['b', 'n']

# intersection
valid = set(['yellow', 'red', 'blue', 'green', 'black'])
input_set = set(['red', 'brown'])
print(input_set.difference(valid)) #set(['brown'])

a_set = {'red', 'blue', 'green'}
print(type(a_set)) #<type 'set'>