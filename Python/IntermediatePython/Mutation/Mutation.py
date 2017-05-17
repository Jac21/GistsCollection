"""

Mutation - 'able to be changed'

"""

foo = ['h1']
print(foo) #['hi']

bar = foo
bar += ['bye']
print(foo) #['hi', 'bye']

# Bad example
def add_to(num, target=[]):
    target.append(num)
    return target

add_to(1)
# Output: [1]

add_to(2)
# Output: [1, 2]

print(add_to(3))
# Output: [1, 2, 3]

# Good one
def add_to_safe(element, target=None):
    if target is None:
        target = []
    target.append(element)
    return target

add_to_safe(1)
# Output: [1]

add_to_safe(2)
# Output: [2]

print(add_to_safe(42))
# Output: [42]