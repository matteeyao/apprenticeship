require_relative 'board'

test_board = Board.new

x = test_board[[0, 0]]

def fill_board(board)
    for i in (0...3)
        for j in (0...3)
            board[[i, j]] = :x
            # puts("#{i}, #{j} = #{board[[i, j]]}")
        end
    end
    board
end

new_test_board = fill_board(test_board)

print new_test_board.winner
# print test_board.grid
# puts new_test_board.grid.length
# puts new_test_board.grid[0].length