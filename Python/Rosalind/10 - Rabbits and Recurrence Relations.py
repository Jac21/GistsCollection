"""

Problem

When finding the nn-th term of a sequence defined by a recurrence relation, we can 
simply use the recurrence relation to generate terms for progressively larger values 
of nn. This problem introduces us to the computational technique of dynamic programming, 
which successively builds up solutions by using the answers to smaller cases.

"""

import itertools

def fibRab(num_month, num_offspring):
	if num_month == 1:
		return 1
	elif (num_month == 2):
		return num_offspring

	generation_one = fibRab(num_month -1, num_offspring)
	generation_two = fibRab(num_month -2, num_offspring)

	if num_month <= 4:
		return generation_one + generation_two

	return generation_one + (generation_two * num_offspring)

if __name__ == "__main__":

	assert fibRab(5, 3) == 19

	result = fibRab(3, 5)
	print result

	# new dataset output
	f = open('workfile.txt', 'w')
	f.write(str(result))