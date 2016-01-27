"""
Problem

An RNA string is a string formed from the alphabet containing 'A', 'C', 'G', and 'U'.

Given a DNA string tt corresponding to a coding strand, its transcribed RNA string uu is formed by replacing all occurrences of 'T' in tt with 'U' in uu.

Given: A DNA string tt having length at most 1000 nt.

Return: The transcribed RNA string of tt.
"""

# transcription method
def transcribe(dnaString, oldStrand, codingStrand):
	return dnaString.replace(oldStrand, codingStrand)


if __name__ == "__main__":
	# dataset read-in and processing
	r = open('rosalind_rna.txt', 'r')
	transcriptionOutput = transcribe(r.read(), "T", "U")

	# new dataset output
	f = open('workfile.txt', 'w')
	f.write(transcriptionOutput)