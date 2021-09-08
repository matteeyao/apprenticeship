class Player
    attr_reader :name

    def initialize(name)
        @name = name
    end

    def get_pos
        puts "#{@name}: please input your next spot as row,col (e.g. 0,0)"
        return gets.chomp.split(",").map(&:to_i)
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
        while true
            check_pos(get_pos)
        end
    end
end
