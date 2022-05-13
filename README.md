# Automated Proof Checker

An automated proof checker that verifies correctness of proofs in natural deduction (and semantic tableaux + transformational proof in the future).
Will also handle program correctness and z-specification rule/type checking. 

George uses maximal munch to scan input tokens in predicate logic, and then uses a modified version of the railroad-shunt algorithm to generate a parse tree. Then, rules are checked to ensure correct usage.
