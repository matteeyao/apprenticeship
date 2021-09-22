require_relative 'Player'

class EasyPlayer < Player

    def initialize
        super("\u{2B55}")
    end

    def move(game, mark)
        winning_move(game, mark) || random_move(game)
    end

    def random_move(game)
        board = game.board

        loop do
            range = (0..2).to_a
            pos = [range.sample, range.sample]
            return pos if board.is_empty?(pos)
        end
    end

    private

    def winning_move(game, mark)
        (0..2).each do |row|
            (0..2).each do |col|
                dup_board = game.board.dup
                pos = [row, col]

                next unless dup_board.is_empty?(pos)
                dup_board[pos] = mark
                
                return pos if dup_board.winner == mark
            end
        end

        # no winning move
        nil
    end
end
