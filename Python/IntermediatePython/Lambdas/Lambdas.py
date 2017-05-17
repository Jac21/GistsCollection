"""

Lambdas - one line functions, AKA, anon functions
lambda argument: manipulate(argument)

"""

add = lambda x, y: x + y
print (add(3, 5)) # 8

# List sorting
a = [(1, 2), (4, 1), (9, 10), (13, -3)]
a.sort(key=lambda x: x[1])
print(a) # [(13, -3), (4, 1)...]

# parallel sorting
list1 = [1, 2, 3]
list2 = [4, 5, 6]

data = zip(list1, list2)
data.sort()
list1, list2 = map(lambda t: list(t), zip(*data))
print(data) # [(1, 4), (2, 5), (3, 6)]