# Search Word Puzzle

API Project to resolve the board game search word puzzle.

## Description

The end-point SearchPuzzle/search, receives a list of strings of the same length. This is the
string board to find words. The word to find are sent as a list on the query string.

## Rules

* Words may appear horizontally, from left to right, or vertically, from top to bottom.
* The matrix size to play does not exceed 64x64.
* All strings contain the same number of characters.
* The API returns the top 10 most repeated words from the word stream found in the matrix.
* If no words are found, the API returns an empty set of strings.
* If any word in the word stream is found more than once within the stream, the search results should count it only once.

## Logic behind

The API uses a prefix tree to store and find the words to search. It tries to find the words cell by cell in the grid.
Searches are performed using the TPL, to get the results quickly. Also, it has some short circuits to avoid unnecessary
executions of the main logic.