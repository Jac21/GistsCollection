"""

Comprehensions - constructs that allow sequences to be built
from other sequences

Three types -
	List Comprehensions
	Dictionary Comprehensions
	Set Comprehensions

"""

# List Comprehensions
# variable = [out_exp for out_exp in input_list if out_exp == 2]

multiples = [i for i in range(30) if i % 3 is 0]
print(multiples) #[0, 3, 6, 9, 12, 15, 18, 21, 24, 27]

squared = [x**2 for x in range(10)]

# Dict Comprehensions

mcase = {'a': 10, 'b': 34, 'A': 7, 'Z': 3, 'z': 5}

mcase_freq = {
	k.lower(): mcase.get(k.lower(), 0) + mcase.get(k.upper(), 0)
	for k in mcase.keys()
}

print(mcase_freq)
# mcase_freq == {'a': 17, 'z': 8, 'b': 34}

# Set Comprehensions

squared = {x**2 for x in [1, 1, 2]}
print(squared) # {1, 4}