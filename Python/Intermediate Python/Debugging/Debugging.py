"""

Debugging with pdb - >python -m pdf my_script.py

"""

import pdb

# set a breakpoint
def make_item():
	pdb.set_trace()
	return "I don't have the time"

print(make_item())

"""

Commands - 

c: continue execution

w: shows the context of the current line it is executing.

a: print the argument list of the current function

s: Execute the current line and stop at the first possible 
occasion.

n: Continue execution until the next line in the current 
function is reached or it returns.

"""