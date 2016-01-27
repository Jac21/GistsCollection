"""

Problem

The 20 commonly occurring amino acids are abbreviated by using 20 letters from the English alphabet (all letters except for B, J, O, U, X, and Z). Protein strings are constructed from these 20 symbols. Henceforth, the term genetic string will incorporate protein strings along with DNA strings and RNA strings.

The RNA codon table dictates the details regarding the encoding of specific codons into the amino acid alphabet.

Given: An RNA string ss corresponding to a strand of mRNA (of length at most 10 kbp).

Return: The protein string encoded by ss.

"""

import itertools

rna_codon_table = {"UUU":"F", "UUC":"F", "UUA":"L", "UUG":"L",
    "UCU":"S", "UCC":"S", "UCA":"S", "UCG":"S",
    "UAU":"Y", "UAC":"Y", "UAA":"STOP", "UAG":"STOP",
    "UGU":"C", "UGC":"C", "UGA":"STOP", "UGG":"W",
    "CUU":"L", "CUC":"L", "CUA":"L", "CUG":"L",
    "CCU":"P", "CCC":"P", "CCA":"P", "CCG":"P",
    "CAU":"H", "CAC":"H", "CAA":"Q", "CAG":"Q",
    "CGU":"R", "CGC":"R", "CGA":"R", "CGG":"R",
    "AUU":"I", "AUC":"I", "AUA":"I", "AUG":"M",
    "ACU":"T", "ACC":"T", "ACA":"T", "ACG":"T",
    "AAU":"N", "AAC":"N", "AAA":"K", "AAG":"K",
    "AGU":"S", "AGC":"S", "AGA":"R", "AGG":"R",
    "GUU":"V", "GUC":"V", "GUA":"V", "GUG":"V",
    "GCU":"A", "GCC":"A", "GCA":"A", "GCG":"A",
    "GAU":"D", "GAC":"D", "GAA":"E", "GAG":"E",
    "GGU":"G", "GGC":"G", "GGA":"G", "GGG":"G",}

# helper method to iterate over string in n characters at a time
def groups_of_n(n, iterable):
	c = itertools.count()
	for _, gen in itertools.groupby(iterable, lambda x: c.next() / n):
		yield gen

def translate(protein_string):
	result_string = ""
	# iterate over protein in groups of three, join groups into
	# single string, append dict value of string to result
	for symbol in groups_of_n(3, protein_string):
		grouped_symbol = ''.join(symbol)
		result_string += rna_codon_table.get(grouped_symbol)
	# remove resulting STOP string at the end of the result
	STOP = result_string[-4:]
	new_result_string = result_string.replace(STOP, '')
	return new_result_string

if __name__ == "__main__":
	# dataset read-in and processing
	r = open('rosalind_prot.txt', 'r')
	lines = [line.rstrip('\n') for line in r]

	# quick assertion test
	assert translate('AUGGCCAUGGCGCCCAGAACUGAGAUCAAUAGUACCCGUAUUAACGGGUGA') == 'MAMAPRTEINSTRING'

	result = translate(lines[0])
	print result

	# new dataset output
	f = open('workfile.txt', 'w')
	f.write(result)