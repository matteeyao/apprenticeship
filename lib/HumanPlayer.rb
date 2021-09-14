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

    def initialize(name, mark)
        super(name, mark)
    end

    def fetch_input
        print "#{mark} #{name}, enter a position 1-9: "
        input = gets.chomp
        return input
    end

    def convert_input_to_pos(input)
        return self.class.position_dictionary[input]
    end

    def is_valid_input?(input)
        self.class.position_dictionary.keys.include?(input)
    end

    # TODO: Add unit/integration test/spec for method
    def verify_and_convert_input_to_pos(game, input)
        pos = convert_input_to_pos(input)
        if is_valid_input?(input) && game.board.isEmpty?(pos)
            return pos
        elsif !is_valid_input?(input)
            puts "Invalid input position!\n\n"
        elsif !game.board.isEmpty?(pos)
            puts "Position is already taken!"
        end
    end

    # TODO: Add unit/integration test/spec for method
    def move(game, mark)
        game.show
        loop do
            user_input = fetch_input
            pos = verify_and_convert_input_to_pos(user_input)
            if pos != nil
                return pos
            end
        end
    end
end
