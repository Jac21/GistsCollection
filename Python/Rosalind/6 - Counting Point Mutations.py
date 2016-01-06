"""
Problem

Given two strings ss and tt of equal length, the Hamming distance between ss and tt, denoted dH(s,t)dH(s,t), is the number of corresponding symbols that differ in ss and tt. See Figure 2.

Given: Two DNA strings ss and tt of equal length (not exceeding 1 kbp).

Return: The Hamming distance dH(s,t)dH(s,t).
"""

# dataset read-in and processing
r = open('rosalind_hamm.txt', 'r')
lines = [line.rstrip('\n') for line in r]

distance = 0
for character_one, character_two in zip(lines[0], lines[1]):
	if character_one != character_two:
		distance += 1

print distance

# new dataset output
f = open('workfile.txt', 'w')
f.write(str(distance))