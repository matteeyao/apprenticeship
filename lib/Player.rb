class Player
    attr_reader :name

    def initialize(name)
        @name = name
    end

    def get_pos(mark)
        print "#{@name} (#{mark.capitalize()}): please input a position to place your mark (e.g. row,col) "
        pos = gets.chomp.split(",").map(&:to_i)
        return pos
    end

    def check_pos(pos) 
        if Board.isValid?(pos)
            return pos
        else
            puts "Invalid coordinates!"
        end
    end

    def move(game, mark)
        game.show
        puts
        while true
            pos = get_pos(mark)
            puts
            if check_pos(pos) != nil
                return pos
            end
        end
    end
end
