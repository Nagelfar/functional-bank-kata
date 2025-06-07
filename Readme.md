# Functional Calisthenics Bank Kata

Attempt to implement the [Bank](https://github.com/sandromancuso/Bank-kata) [Kata](https://www.codurance.com/publications/2017/11/16/katas-for-functional-calisthenics) with the Functional Calisthenics Constraint in C#

## Constraints

These constraints should push you into writing your code more functional (for more details see <https://www.codurance.com/publications/2017/10/12/functional-calisthenics>).

1. **Name everything** - All functions need to be named, you shouldn't use lambdas2.
2. **No mutable state** - You shouldn't use any variable of any type that can mutate 
3. **Exhaustive conditionals** - No if without an else, switches and pattern matching always have all paths considered
4. **Do not use intermediate variables** - No variables declared in the body of a function, only parameters and other functions appear on the body
5. **Expressions not statements** - All lines are be expressions. All lines return a value
6. **No Explicit recursion** - Do not use explicit recursion
7. **Generic building blocks** - Use generic types for your functions, outside of the boundaries of your application
8. **Side effects at the boundaries** - Side effects appear only on the boundaries of your application
9. **Infinite Sequences** - Don't  use collections that have a fixed size specified.
10. **One argument functions** - Each function has a single parameter.

## Implementation

Notes:
- The implementation was done in `C#`
- A lot of [Expression-bodied members](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members) were used during the implementation and to the authors' interpretation they are not lambdas and therefore do not violate the rule #1.
- As long as constructors are not called from outside the class, they are not considered to violate rule #10.

To run the example install .NET core >= 9.0 and run in the `Bank` directory `dotnet run`
