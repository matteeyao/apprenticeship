class Board
    attr_reader :grid

    def self.blank_grid
        Array.new(3) { Array.new(3) }
    end

    def initialize(grid = self.class.blank_grid)
        @grid = grid
    end
end