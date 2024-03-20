What code smells did you see?
opacity, immobility
What problems do you think the Speaker class has?
It:
	-includes too many classes
	-it contains both the entity's properties and the functionality (i think i like them separated)
Which clean code principles (or general programming principles) did it violate?

not readable:
	too many explanatory comments (the functionality should be clear from the code)
	commented code
	magic numbers
	shoted variable names
	using var when the type is not clear (in the nested FORs)
	there were nested if's and you couldn't see their beginning and end at the same time
	there was a long method that was doing a lot of things

understandability:
	didn't used explanatory variables for bool expressions
	used strings instead of dedicated objects

What refactoring techniques did you use?
I broke a big method in smaller ones.
I eliminated nested IF's by checking the condition (negated) before the TRUE branch and throwing the exception if was true.
I created some classes to use instead of strings.
I got rid of explanatory comments, commented code.
I created constants for magic numbers.
I renamed variables.
I tried to make bool expressions understandable.
I separated entity properties from its logic.
