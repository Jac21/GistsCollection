"""
Single-byte XOR cipher
The hex encoded string:

1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736
... has been XOR'd against a single character. Find the key, decrypt the message.
"""

import binascii
import sys

s = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736"
decodedString = s.decode("hex")
m = 'x'

for i, c in enumerate(decodedString):
    sys.stdout.write(''.join(chr(ord(a) ^ ord(b)) for a,b in zip(c,m)))

print ''