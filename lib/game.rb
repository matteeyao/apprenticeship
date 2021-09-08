require_relative 'Board'
require_relative 'Player'

class Game
    class IllegalMoveError < RuntimeError
    end

    attr_reader :board, :players, :turn

    def initialize(player1, player2)
        @board = Board.new
        @players = { :x => player1, :o => player2 }
        @turn = :x
    end

    def show
        self.board.grid.each_with_index do |row, idx|
            puts " " + row.map { |el| el.nil? ? " " : el }.join(" | ") + " "
            if (idx != 2)
                puts "-----------"
            end
        end
    end

    def place_mark(pos, mark)
        if self.board.isEmpty?(pos)
            self.board[pos] = mark
            true
        else
            false
        end
    end

    def play_turn
        loop do
            current_player = self.players[self.turn]
            pos = current_player.move(self, self.turn)

            break if place_mark(pos, self.turn)
        end

        swap_turns
    end

    def swap_turns
        # swap next whose turn it will be next
        @turn = ((self.turn == :x) ? :o : :x)
    end

    def print_results
        if self.board.isWon?
            winning_player = self.players[self.board.winner]
            puts "#{winning_player.name} won the game!"
        else
            puts "No one wins!"
        end
    end

    def run
        until self.board.isOver?
            play_turn
        end

        print_results
    end
end