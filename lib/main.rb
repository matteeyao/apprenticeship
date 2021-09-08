require_relative 'Board'
require_relative 'Game'

test_game = Game.new("Player One", "Player Two")

test_game.board[[0, 0]] = :x
test_game.board[[1, 1]] = :o

# print test_game.board.grid

test_game.show
# print test_board.grid
# puts new_test_board.grid.length
# puts new_test_board.grid[0].length

# puts :x