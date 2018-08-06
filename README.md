## Introduction

This is a single tape deterministic Turing Machine simulator made up in Unity5. To run it, you must add the machines descriptions.

![Turing Machine](https://github.com/Skalnark/Turing/blob/master/images/machine.png)

### Coding

To code your own machines, you must use the following sintaxe:
- machine name#
- alphabet (no space or commas between symbols)#
- initial state (starting from zero)#
- acceptation state (also starting from zero)#
- functions (each **def** defines a state) #
- machine description 

#### Example:

![machine description](https://github.com/Skalnark/Turing/blob/master/images/code.png)

 - The **Ø** represents the empty cell.
- **void** represents a state with no functions,
- Functions must be separated by "**;**".
- The machine stops when, in the current state, there is no delta function for this symbol. 
- Each description mus be between braces "**{** ... **}**".
