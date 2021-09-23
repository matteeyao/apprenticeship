require_relative 'Player'

class HumanPlayer < Player

    def self.generate_position_dictionary(grid_size)
        position_dictionary = {}
        key = 1
        (0...grid_size).each do |row_idx|
            (0...grid_size).each do |col_idx|
                position_dictionary[key.to_s] = [row_idx, col_idx]
                key += 1
            end
        end
        position_dictionary
    end

    attr_reader :position_dictionary, :grid_size

    def initialize(mark, grid_size = 3)
        super(mark)
        @position_dictionary = self.class.generate_position_dictionary(grid_size)
        @grid_size = grid_size
    end

    def fetch_input
        print "#{mark} enter a position 1-#{grid_size * grid_size}: "
        input = gets.chomp
        return input
    end

    def convert_input_to_pos(input)
        return self.position_dictionary[input]
    end

    def is_valid_input?(input)
        self.position_dictionary.keys.include?(input)
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
