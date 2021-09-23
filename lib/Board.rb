class Board
    @@marks = [:x, :o]

    def self.marks
        @@marks
    end

    attr_reader :grid_size, :grid

    def initialize(grid_size, grid = self.class.blank_grid(grid_size))
        @grid_size = grid_size
        @grid = grid
    end

    def self.is_valid_grid_size?(grid_size)
        if grid_size < 3
            raise "Invalid board size"
        end
    end

    def self.blank_grid(grid_size)
        begin
            is_valid_grid_size?(grid_size)
            Array.new(grid_size) { Array.new(grid_size) } 
        rescue Exception => e
            puts e.to_s
        end
    end

    def [](pos)
        raise "invalid position was entered" unless self.is_valid?(pos)
        rowIdx, colIdx = pos[0], pos[1]
        @grid[rowIdx][colIdx]
    end

    def []=(pos, mark)
        raise "invalid position was entered" unless self.is_valid?(pos)
        raise "mark already placed there" unless is_empty?(pos)
        rowIdx, colIdx = pos[0], pos[1]
        @grid[rowIdx][colIdx] = mark
    end

    def rows
        grid
    end

    def generate_columns
        cols = []
        grid_size.times { cols << [] }
        cols
    end

    def cols
        cols = self.generate_columns
        @grid.each do |row|
            row.each_with_index do |mark, col_idx|
                cols[col_idx] << mark
            end
        end
        cols
    end

    def generate_down_diagonal_coordinates
        (0...grid_size).to_a.map { |idx| [idx, idx] }
    end

    def generate_up_diagonal_coordinates
        (0...grid_size).to_a.map { |idx| [idx, grid_size - idx - 1] }
    end

    def diagonals
        down_diag = self.generate_down_diagonal_coordinates
        up_diag = self.generate_up_diagonal_coordinates

        [down_diag, up_diag].map do |diag|
            # Note the `row, col` inside the block; this unpacks, or
            # "destructures" the argument. Read more here:
            # http://tony.pitluga.com/2011/08/08/destructuring-with-ruby.html
            diag.map { |row, col| @grid[row][col] }
        end
    end

    def dup
        duped_rows = grid.map(&:dup)
        self.class.new(grid_size, duped_rows)
    end

    def is_valid?(pos)
        row_idx, col_idx = pos[0], pos[1]
        [row_idx, col_idx].all? { |coord| (0...grid_size).include?(coord) }
    end

    def is_empty?(pos)
        self[pos].nil?
    end

    def is_tied?
        return false if self.is_won?

        # no empty space?
        @grid.all? { |row| row.none? { |el| el.nil? }}
    end

    def is_over?
        # don't use Ruby's `or` operator; always prefer `||`
        self.is_won? || self.is_tied?
    end

    def generate_x_winning_sequence
        sequence = []
        @grid_size.times { sequence << :x }
        sequence
    end

    def generate_o_winning_sequence
        sequence = []
        @grid_size.times { sequence << :o }
        sequence
    end

    def winner
        (rows + cols + diagonals).each do |sequence|
            return :x if sequence == self.generate_x_winning_sequence
            return :o if sequence == self.generate_o_winning_sequence
        end

        nil
    end

    def is_won?
        !winner.nil?
    end
end
