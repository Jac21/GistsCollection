"""

Given: Three positive integers kk, mm, and nn, representing a population containing 
k+m+nk+m+n organisms: kk individuals are homozygous dominant for a factor, mm are 
heterozygous, and nn are homozygous recessive.

Return: The probability that two randomly selected mating organisms will produce an 
individual possessing a dominant allele (and thus displaying the dominant phenotype). 
Assume that any two organisms can mate.

"""
import itertools

def dominant_probability(num_homozygous_dominant, num_heterozygous, num_homozygous_recessive):
	total = num_homozygous_dominant + num_heterozygous + num_homozygous_recessive

	recessive_probability = (num_homozygous_recessive / total) * ((num_homozygous_recessive - 1) / (total - 1))
	heterozygous_probability = (num_heterozygous / total) * ((num_heterozygous - 1) / (total - 1))
	hetero_recessive_probability = (num_heterozygous / total) * (num_homozygous_recessive / (total - 1)) + (num_homozygous_recessive / total) * (num_heterozygous / (total - 1))

	recessive_total = recessive_probability + heterozygous_probability * (.25) + hetero_recessive_probability * (.5)
	return (1 - recessive_total)

if __name__ == "__main__":

	result = round(dominant_probability(22.0, 23.0, 26.0), 5)
	print result

	# new dataset output
	f = open('workfile.txt', 'w')
	f.write(str(result))