class Player
    attr_reader :name

    def initialize(name)
        @name = name
    end

    def get_input
        puts "#{@name}: please input your next spot as row,col (e.g. 0,0)"
        row, col = gets.chomp.split(",").map(&:to_i)
        [row, col]
    end

    def check_input(input) 
        if Board.isValid?(input)
            return input
        else
            puts "Invalid coordinates!"
        end
    end

    def move(game, mark)
        game.show
        while true
            check_input(get_input)
        end
    end
end