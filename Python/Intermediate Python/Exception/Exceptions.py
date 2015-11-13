"""

Exceptions - 

"""

# basic try/except clause
try:
	file = open('test.txt', 'rb')
except IOError as e:
	print('An IOError occurred. {}'.format(e.args[-1]))

# handling multiple exceptions
try:
	file = open('test.txt', 'rb')
except (IOError, EOFError) as e:
	print('An IOError occurred. {}'.format(e.args[-1]))

try:
	file = open('test.txt', 'rb')
except EOFError as e:
	print("An EOF error occurred...")
	#raise e
except IOError as e:
	print('An IOError occurred...')
	#raise e

# generic exception
try:
	file = open('test.txt', 'rb')
except Exception:
	print("Exception found...")
	#raise

# finally clause
try:
	file = open('test.txt', 'rb')
except IOError as e:
	print('An IOError occurred. {}'.format(e.args[-1]))
finally:
	print("This would be printed whether or not an exception occurred")

# An IOError occurred. No such file or directory
# This would be printed whether or not an exception occurred

# try/else clause
try:
	print("An exception shouldn't happen!")
except Exception:
	print('Exception!')
else:
	# any code that should only run if no exception occurs
	# in the try, but for which exceptions should NOT
	# be caught
	print('This would run only if no exception occurs')
finally:
	print('Printed anyways...')
