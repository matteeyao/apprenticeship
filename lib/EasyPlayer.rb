require_relative 'Player'

class EasyPlayer < Player

    def initialize
        super("\u{2B55}")
    end

    def random_move(game)
        board = game.board

        loop do
            range = (0..2).to_a
            pos = [range.sample, range.sample]
            return pos if board.is_empty?(pos)
        end
    end

    def move(game)
        random_move(game)
    end
end
