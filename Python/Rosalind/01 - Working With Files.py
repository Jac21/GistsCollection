"""
Problem

Given: A file containing at most 1000 lines.

Return: A file containing all the even-numbered lines from the original file. Assume 1-based numbering of lines.

"""

from itertools import islice

def get_even_lines(file_path):
	new_file = ""
	with open(file_path, "r") as f:
		for line in islice(f, 1, None, 2):
			new_file += line
	return new_file


if __name__ == "__main__":

	result = get_even_lines("rosalind_ini5.txt")
	print str(result)

	# new dataset output
	f = open('workfile.txt', 'w')
	f.write(str(result))
