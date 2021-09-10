class Player
    attr_reader :name

    def initialize(name)
        @name = name
    end

    def get_pos(mark)
        print "#{@name} (#{mark.capitalize()}), please enter a position (e.g. row,col or 0,0): "
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
