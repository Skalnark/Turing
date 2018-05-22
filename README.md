
# Turing

#### Introduction
This is a deterministic full functional turing machine simulator made up in Unity5. 
To utilize the machine, you'll need to create on its file folder, a directory on tm_Data called "Machines" and will need to put the machine descriptions on there.

This software is in development phase, so we have no proper interface.

We already have 3 descriptions on this repository, on the "Assets/Machines". One of them, the "castor" machine implements a 6+accept states Busy Beaver machine, there is also a "clear" wich recognizes machines with the same number of 'w's and 'q's and then clear the tape, and "palindromo" recognizes palindromos with 'i's and 'o's.

#### Coding
You can write your own machines by following the syntax. The machine is coded on the following structure:

Name#alphabet#number of states#initial state#final state#function1 from state 0|function2 from state 0;function1 from state 1...;'void' to represent a state without functions#machine's description

The function syntax is on the format a,b,R,0

where the 'a' is the symbol readed, b is the symbol writed, R is the side from where to move (R is right, L is left, S is stay) and '0' is the state from where to go.

the alphabet is not seprated by commas or other things, if your alphabet is formed by 'a', 'b', and 'c', so you will input it on the description as 'abc'.

### Loading the machine
After place the description on the folder, You'll need to tape it's name on the input field on the bottom of the tape. The red button stops the machine if it's not working.
