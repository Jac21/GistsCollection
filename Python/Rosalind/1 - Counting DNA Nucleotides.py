"""
Problem

A string is simply an ordered collection of symbols selected from some alphabet and formed into a word; the length of a string is the number of symbols that it contains.

An example of a length 21 DNA string (whose alphabet contains the symbols 'A', 'C', 'G', and 'T') is "ATGCTTCAGAAAGGTCTTACG."

Given: A DNA string ss of length at most 1000 nt.

Return: Four integers (separated by spaces) counting the respective number of times that the symbols 'A', 'C', 'G', and 'T' occur in ss.
"""

import re

def countNucleotides(dna_seq, symbol):
	return len(re.findall(symbol, dna_seq))

if __name__ == "__main__":

	result_set = []

	result_set.append(countNucleotides("ATTTCAGAGTTCACATCCTACCCAACTCCCCAACGTGGGGCTTCCGTCACTTAATGGCGTCCATAGCAGAAGAGGATGTTATTAGGATCGAAGCGCCCTTAAGCGACGCCCTAGATACTACATGTCGCCAGATTTTCCTGACACGAAACTCCCTGCACTTTTCTGGAGTAACTAGAAGTACGGTGGCTTAAAAATTGGTCGTCAGGTCGTGAACGCGCATAAAGTCCACGATTGCGTGTGTTCGCACTACGGCACCGAGCATGTCAAATGAACCCGCGTACAGCCTACCCCCTTGCGCGTCTCTAACTAATTGACCCTCTCGCATAGCTATTTTCAGTGTTTATTAAGTAGGCGAGGTGGGTCTTCTCTTAAAGCTCCTTTGGTCCGTAAACATTTAATGGGAGTGCTTACAAGATTTCTAGATCTGACAGTGGGCCACGTGGCTCGGGATACATGCCGCGGCCGTATTTAACGTCCGACAGCGGCTACCGAGCCTCCGCTCTGGCCCTGGTCAGGGTTTTGTGTACCTCAAACGCCTATTGAGCCGAAGTCTAGACAGCATACGCGCCCTTTTAGCCTAGAATATGTCAAACGTATCTTCTATGGCGCCCTGATTGTACACTTTTGTGCGTTCCCTCTGACTGAAAATTCCCTGGGGGTTCAGAGGCAGTCAGTCATGTTGCACCCCAGATCATCGCGTGATTGATCGGACATGTGAGAGCTATTTGGTCTGCTGACCAAGGTACACCTGCGAAGTCAGCCTATATCTACGCACAGATACGGCGCCGCTGCGAACCTTAGAGATTTACAATTAATAATATTCGTTTTACGTGGCCAAGTAGGTATCCCTAACCTGCACACTGCGCTGCTCTTGCCATGTTTGAGGGATGGTATTTACGGTGGTAAGAGC", 'A'))
	result_set.append(countNucleotides("ATTTCAGAGTTCACATCCTACCCAACTCCCCAACGTGGGGCTTCCGTCACTTAATGGCGTCCATAGCAGAAGAGGATGTTATTAGGATCGAAGCGCCCTTAAGCGACGCCCTAGATACTACATGTCGCCAGATTTTCCTGACACGAAACTCCCTGCACTTTTCTGGAGTAACTAGAAGTACGGTGGCTTAAAAATTGGTCGTCAGGTCGTGAACGCGCATAAAGTCCACGATTGCGTGTGTTCGCACTACGGCACCGAGCATGTCAAATGAACCCGCGTACAGCCTACCCCCTTGCGCGTCTCTAACTAATTGACCCTCTCGCATAGCTATTTTCAGTGTTTATTAAGTAGGCGAGGTGGGTCTTCTCTTAAAGCTCCTTTGGTCCGTAAACATTTAATGGGAGTGCTTACAAGATTTCTAGATCTGACAGTGGGCCACGTGGCTCGGGATACATGCCGCGGCCGTATTTAACGTCCGACAGCGGCTACCGAGCCTCCGCTCTGGCCCTGGTCAGGGTTTTGTGTACCTCAAACGCCTATTGAGCCGAAGTCTAGACAGCATACGCGCCCTTTTAGCCTAGAATATGTCAAACGTATCTTCTATGGCGCCCTGATTGTACACTTTTGTGCGTTCCCTCTGACTGAAAATTCCCTGGGGGTTCAGAGGCAGTCAGTCATGTTGCACCCCAGATCATCGCGTGATTGATCGGACATGTGAGAGCTATTTGGTCTGCTGACCAAGGTACACCTGCGAAGTCAGCCTATATCTACGCACAGATACGGCGCCGCTGCGAACCTTAGAGATTTACAATTAATAATATTCGTTTTACGTGGCCAAGTAGGTATCCCTAACCTGCACACTGCGCTGCTCTTGCCATGTTTGAGGGATGGTATTTACGGTGGTAAGAGC", 'C'))
	result_set.append(countNucleotides("ATTTCAGAGTTCACATCCTACCCAACTCCCCAACGTGGGGCTTCCGTCACTTAATGGCGTCCATAGCAGAAGAGGATGTTATTAGGATCGAAGCGCCCTTAAGCGACGCCCTAGATACTACATGTCGCCAGATTTTCCTGACACGAAACTCCCTGCACTTTTCTGGAGTAACTAGAAGTACGGTGGCTTAAAAATTGGTCGTCAGGTCGTGAACGCGCATAAAGTCCACGATTGCGTGTGTTCGCACTACGGCACCGAGCATGTCAAATGAACCCGCGTACAGCCTACCCCCTTGCGCGTCTCTAACTAATTGACCCTCTCGCATAGCTATTTTCAGTGTTTATTAAGTAGGCGAGGTGGGTCTTCTCTTAAAGCTCCTTTGGTCCGTAAACATTTAATGGGAGTGCTTACAAGATTTCTAGATCTGACAGTGGGCCACGTGGCTCGGGATACATGCCGCGGCCGTATTTAACGTCCGACAGCGGCTACCGAGCCTCCGCTCTGGCCCTGGTCAGGGTTTTGTGTACCTCAAACGCCTATTGAGCCGAAGTCTAGACAGCATACGCGCCCTTTTAGCCTAGAATATGTCAAACGTATCTTCTATGGCGCCCTGATTGTACACTTTTGTGCGTTCCCTCTGACTGAAAATTCCCTGGGGGTTCAGAGGCAGTCAGTCATGTTGCACCCCAGATCATCGCGTGATTGATCGGACATGTGAGAGCTATTTGGTCTGCTGACCAAGGTACACCTGCGAAGTCAGCCTATATCTACGCACAGATACGGCGCCGCTGCGAACCTTAGAGATTTACAATTAATAATATTCGTTTTACGTGGCCAAGTAGGTATCCCTAACCTGCACACTGCGCTGCTCTTGCCATGTTTGAGGGATGGTATTTACGGTGGTAAGAGC", 'G'))
	result_set.append(countNucleotides("ATTTCAGAGTTCACATCCTACCCAACTCCCCAACGTGGGGCTTCCGTCACTTAATGGCGTCCATAGCAGAAGAGGATGTTATTAGGATCGAAGCGCCCTTAAGCGACGCCCTAGATACTACATGTCGCCAGATTTTCCTGACACGAAACTCCCTGCACTTTTCTGGAGTAACTAGAAGTACGGTGGCTTAAAAATTGGTCGTCAGGTCGTGAACGCGCATAAAGTCCACGATTGCGTGTGTTCGCACTACGGCACCGAGCATGTCAAATGAACCCGCGTACAGCCTACCCCCTTGCGCGTCTCTAACTAATTGACCCTCTCGCATAGCTATTTTCAGTGTTTATTAAGTAGGCGAGGTGGGTCTTCTCTTAAAGCTCCTTTGGTCCGTAAACATTTAATGGGAGTGCTTACAAGATTTCTAGATCTGACAGTGGGCCACGTGGCTCGGGATACATGCCGCGGCCGTATTTAACGTCCGACAGCGGCTACCGAGCCTCCGCTCTGGCCCTGGTCAGGGTTTTGTGTACCTCAAACGCCTATTGAGCCGAAGTCTAGACAGCATACGCGCCCTTTTAGCCTAGAATATGTCAAACGTATCTTCTATGGCGCCCTGATTGTACACTTTTGTGCGTTCCCTCTGACTGAAAATTCCCTGGGGGTTCAGAGGCAGTCAGTCATGTTGCACCCCAGATCATCGCGTGATTGATCGGACATGTGAGAGCTATTTGGTCTGCTGACCAAGGTACACCTGCGAAGTCAGCCTATATCTACGCACAGATACGGCGCCGCTGCGAACCTTAGAGATTTACAATTAATAATATTCGTTTTACGTGGCCAAGTAGGTATCCCTAACCTGCACACTGCGCTGCTCTTGCCATGTTTGAGGGATGGTATTTACGGTGGTAAGAGC", 'T'))


	# new dataset output
	f = open('workfile.txt', 'w')

	for result in result_set:
		print result
		f.writelines(str(result) + '\n')