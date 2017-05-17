"""

Python C Extensions

"""

# ctypes

# For Linux
# $  gcc -shared -Wl,-soname,adder -o adder.so -fPIC add.c
from ctypes import *

# load the shared object file
adder = CDLL('./adder.so')

# Find sum of integers
res_int = adder.add_int(4, 5)
print "Sum of 4 and 5 = " + str(res_int)

# Find sum of floats
a = c_float(5.5)
b = c_float(4.4)

add_float = adder.add_float
add_float.restype = c_float
print "Sum of 5.5 and 4.4 = ", str(add_float(a, b))