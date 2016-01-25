"""

Problem

Return: The total number of permutations of length nn, followed by a list of all such permutations (in any order).

"""
import itertools

def fac(n):
	if n == 0:
		return 1
	else:
		return n*fac(n-1)

def permu(n):
	print fac(n)
	array = range(n+1)
	permu_l = list(itertools.permutations(array[1:n+1],3))
	for x in permu_l:
		print (str(list(x)).strip('[]')).replace(',', ' ')

if __name__ == "__main__":
	# dataset read-in and processing
	r = open('rosalind_perm.txt', 'r')
	n = int(r.readline().strip())

	total_perms = permu(n)

	# new dataset output
	f = open('workfile.txt', 'w')
	f.write(str(total_perms))

	print str(total_perms)