"""
Problem

In DNA strings, symbols 'A' and 'T' are complements of each other, as are 'C' and 'G'.

The reverse complement of a DNA string ss is the string scsc formed by reversing the symbols of ss, then taking the complement of each symbol (e.g., the reverse complement of "GTCA" is "TGAC").

Given: A DNA string ss of length at most 1000 bp.

Return: The reverse complement scsc of ss.
"""

complement = {'A': 'T', 'C': 'G', 'G': 'C', 'T': 'A'}
dna_seq = 'AAAACCCGGT'

# dataset read-in and processing
r = open('rosalind_revc.txt', 'r')
reverse_complement = "".join(complement.get(base, base) for base in reversed(r.read()))

# new dataset output
f = open('workfile.txt', 'w')
f.write(reverse_complement)

print reverse_complement