require_relative 'Board'
require_relative 'Player'

class Game
    class IllegalMoveError < RuntimeError
    end

    attr_reader :board, :players, :turn

    def initialize(player1, player2)
        @players = { :x => player1, :o => player2 }
        @board = Board.new
        @turn = :x
    end

    def show
        self.board.grid.each_with_index do |row, idx|
            puts row.map { |el| el.nil? ? "  " : players[el].mark }.join(" | ")
            puts "------------" if (idx != 2)
        end
        puts
    end

    # TODO: Add unit/integration test/spec for method
    def play_turn
        current_player = self.players[self.turn]
        pos = current_player.move(self, self.turn)
        self.board[pos] = self.turn
    end

    def swap_turn
        # swap next whose turn it will be next
        @turn = ((self.turn == :x) ? :o : :x)
    end

    def print_results
        self.show
        if self.board.is_won?
            winning_player = self.players[self.board.winner]
            puts "#{winning_player.mark} won the game!"
        else
            puts "No one wins!"
        end
        puts
    end

    def run
        until self.board.is_over?
            play_turn
            puts
            swap_turn
        end

        print_results
    end
end

# TEST
if $PROGRAM_NAME == __FILE__
    puts Board.new(4).grid
end
