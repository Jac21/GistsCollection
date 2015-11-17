"""

For - Else -

"""

languages = ['C', 'Javascript', 'Python']
for language in languages:
	print(language.capitalize())

for n in range(2, 10):
	for x in range(2, n):
		if n % x == 0:
			print(n, 'equals', x, '*', n/x)
			break
	else:
		print(n, ' is a prime number')