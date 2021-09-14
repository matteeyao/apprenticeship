require_relative 'Board'
require_relative 'Player'

class Game
    class IllegalMoveError < RuntimeError
    end

    @@position_dictionary = {
        "1": [0, 0], "2": [0, 1], "3": [0, 2],
        "4": [1, 0], "5": [1, 1], "6": [1, 2],
        "7": [2, 0], "8": [2, 1], "9": [2, 2]
    }

    def self.position_dictionary
        @@position_dictionary
    end

    attr_reader :board, :players, :turn

    def initialize(player1, player2)
        @players = { :x => player1, :o => player2 }
        @board = Board.new
        @turn = :x
    end

    def show
        self.board.grid.each_with_index do |row, idx|
            puts " " + row.map { |el| el.nil? ? " " : players[el].mark }.join(" | ") + " "
            puts "-----------" if (idx != 2)
        end
        puts
    end

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
        if self.board.isWon?
            winning_player = self.players[self.board.winner]
            puts "#{winning_player.name} won the game!"
        else
            puts "No one wins!"
        end
        puts
    end

    def run
        until self.board.isOver?
            play_turn
            swap_turn
        end

        print_results
    end


    # Move this to Game.rb (interface)
    def get_pos(mark)
        print "#{name} (#{mark.capitalize()}), please enter a position (e.g. row,col or 0,0): "
        pos = gets.chomp.split(",").map(&:to_i)
        return pos
    end

    def check_pos(game, pos) 
        if Board.isValid?(pos) && game.board.isEmpty?(pos)
            return pos
        elsif !Board.isValid?(pos)
            puts "Invalid coordinates!"
        elsif !game.board.isEmpty?(pos)
            puts "Position is already taken!"
        end
        puts
    end

    def move(game, mark)
        game.show
        while true
            pos = get_pos(mark)
            puts
            if check_pos(game, pos) != nil
                return pos
            end
        end
    end
end
