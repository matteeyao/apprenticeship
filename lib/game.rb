require_relative 'board'

class Game
    class IllegalMoveError < RuntimeError
    end

    attr_reader :board, :players, :turn

    def initialize(player1, player2)
        @board = Board.new
        @players = { :x => player1, :o => player2 }
        @turn = :x
    end

    # def run
    #     until self.board.over?
    #     play_turn
    #     end

    #     if self.board.won?
    #     winning_player = self.players[self.board.winner]
    #     puts "#{winning_player.name} won the game!"
    #     else
    #     puts "No one wins!"
    #     end
    # end

    # def show
    #     # not very pretty printing!
    #     self.board.rows.each { |row| p row }
    # end

    # private
    # def place_mark(pos, mark)
    #     if self.board.empty?(pos)
    #     self.board[pos] = mark
    #     true
    #     else
    #     false
    #     end
    # end

    # def play_turn
    #     loop do
    #         current_player = self.players[self.turn]
    #         pos = current_player.move(self, self.turn)

    #         break if place_mark(pos, self.turn)
    #     end

    #     # swap next whose turn it will be next
    #     @turn = ((self.turn == :x) ? :o : :x)
    # end
end