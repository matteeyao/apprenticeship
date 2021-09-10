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
end

if $PROGRAM_NAME == __FILE__
    print "Enter player one's name: "
    player_one_name = gets.chomp
    print "Enter player two's name: "
    player_two_name = gets.chomp
    puts

    player_one = Player.new(player_one_name)
    player_two = Player.new(player_two_name)

    Game.new(player_one, player_two).run
end
