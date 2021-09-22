require_relative 'Player'

class HumanPlayer < Player

    @@position_dictionary = {
        "1" => [0, 0], "2" => [0, 1], "3" => [0, 2],
        "4" => [1, 0], "5" => [1, 1], "6" => [1, 2],
        "7" => [2, 0], "8" => [2, 1], "9" => [2, 2]
    }

    def self.position_dictionary
        @@position_dictionary
    end

    def initialize(mark)
        super(mark)
    end

    def fetch_input
        print "#{mark} enter a position 1-9: "
        input = gets.chomp
        return input
    end

    def convert_input_to_pos(input)
        return self.class.position_dictionary[input]
    end

    def is_valid_input?(input)
        self.class.position_dictionary.keys.include?(input)
    end

    def is_vacant_pos?(game, input)
        pos = convert_input_to_pos(input)
        game.board.is_empty?(pos)
    end

    # TODO: Add unit/integration test/spec for method
    def fetch_pos(game)
        loop do
            input = fetch_input
            if is_valid_input?(input) && is_vacant_pos?(game, input)
                return convert_input_to_pos(input)
            elsif !is_valid_input?(input)
                puts "Invalid input!\n\n"
            elsif !is_vacant_pos?(game, input)
                puts "Position is already taken!\n\n"
            end
        end
    end

    # TODO: Add unit/integration test/spec for method
    def move(game, _mark)
        game.show
        fetch_pos(game)
    end
end
