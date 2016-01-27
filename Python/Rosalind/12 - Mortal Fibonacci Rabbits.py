"""

Problem

Given: Positive integers n <= 100n <= 100 and m <= 20m <= 20.

Return: The total number of pairs of rabbits that will remain after the nn-th month if all rabbits live for mm months.
"""

import itertools

def mortal_fib_rab(num_month, num_death_month):
	if num_month <= 0:
		return 0

	if num_month == 1:
		return 1

	if num_month <= num_death_month:
		return mortal_fib_rab(num_month - 1, num_death_month) + mortal_fib_rab(num_month - 2, num_death_month)
	elif (num_month == num_death_month + 1):
		return mortal_fib_rab(num_month - 1, num_death_month) + mortal_fib_rab(num_month - 2, num_death_month) - 1
	else:
		return mortal_fib_rab(num_month - 1, num_death_month) + mortal_fib_rab(num_month - 2, num_death_month) - mortal_fib_rab(num_month - (num_death_month + 1), num_death_month)

if __name__ == "__main__":

	assert mortal_fib_rab(6, 3) == 4

	result = mortal_fib_rab(29, 5)
	print result

	# new dataset output
	f = open('workfile.txt', 'w')
	f.write(str(result))