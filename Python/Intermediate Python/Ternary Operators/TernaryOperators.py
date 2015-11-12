"""

Ternary Operators - 
Blueprint:

condition_is_true if condition else condition_is_false

Another more obscure and not widely used example involves tuples. Here is some sample code:

Blueprint:

(if_test_is_false, if_test_is_true)[test]

"""

is_far = True
state = "far" if is_far else "not far"
print(state)

far = True
farness = ('way out there', 'way too close')[far]
print('Jeremy is ', farness) #Jeremy is way too close

# avoiding tupled ternery example (both being built and evaled)
condition = True
print(2 if condition else 1/0) # 2

print((1/0, 2)[condition]) #ZeroDivisionError