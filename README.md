# Game of Life - Corona Edition

This is a take on the traditional programming kata that is about calculating the next generation of Conway’s game of life, given any starting position. See http://en.wikipedia.org/wiki/Conway%27s_Game_of_Life for background. The original kata is described here http://codingdojo.org/kata/GameOfLife/.

## Description

The twist is to make this about the spread of a virus instead, and seeing how this can spread an impact a community. You start with a two dimensional grid of cells, where each cell is either healthy or infected. In this version of the problem, the grid is finite, and the virus does not spread off the edges.

## The rules

Most of the rules have a probability that the rule will come in to play, noted in parentheses. These are suggestions, feel free to play around with them, but make sure your test cases reflect your changes!

- A healthy person in contact with an infected person gets infected (50%)
- An infected person goes into quarantine (80%)
- A healthy person in contact with an infected person goes into isolation (10%)
- An infected person stays infected (50%)
- An infected person is healthy again but not immune (5%)
- An infected person is healthy again and immune (40%)
- An infected person dies (5%)
- An immune person can’t be infected
- A quarantined person can’t infect others

## The constraints

- Use a 10x10 grid, each cell is a person
- All eight persons surrounding an infected person can get infected
- 1 random person should start as infected (patient zero)
- Each round a new evaluation of all persons is run
- Can be run for a predefined set of rounds, eg 100

## Bonus

- Make the probability weights configurable
- Create a GUI to visualize/animate the grid changing over time
