from __future__ import print_function
print(print)
# Output: <built-in function print>

"""

Targeting Python 2 + 3 -  highlight some of the tricks which you can employ 
to make a script compatible with both of them.

"""

"""
# Future imports

The first and most important method is to use __future__ imports. It 
allows you to import Python 3 functionality in Python 

https://docs.python.org/3/howto/pyporting.html

"""

"""

Module renaming

"""

try:
    import urllib.request as urllib_request  # for Python 3
except ImportError:
    import urllib2 as urllib_request  # for Python 2

"""

Obsolete Python 2 builtins

"""

# abandon the 12 Python 2 built-ins while in Python 3
# from future.builtins.disabled import *

# apply()
# Output: NameError: obsolete Python 2 builtin apply is disabled