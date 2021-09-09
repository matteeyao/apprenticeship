class Board
    attr_reader :grid

    def self.blank_grid
        Array.new(3) { Array.new(3) }
    end

    def initialize(grid = self.class.blank_grid)
        @grid = grid
    end

    @@marks = [:x, :o]

    def self.marks
        @@marks
    end

    def [](pos)
        raise "invalid position was entered" unless self.class.isValid?(pos)
        rowIdx, colIdx = pos[0], pos[1]
        @grid[rowIdx][colIdx]
    end

    def []=(pos, mark)
        raise "invalid position was entered" unless self.class.isValid?(pos)
        raise "mark already placed there" unless isEmpty?(pos)
        rowIdx, colIdx = pos[0], pos[1]
        @grid[rowIdx][colIdx] = mark
    end

    def cols
        cols = [[], [], []]
        @grid.each do |row|
            row.each_with_index do |mark, col_idx|
                cols[col_idx] << mark
            end
        end
        cols
    end

    def diagonals
        down_diag = [[0, 0], [1, 1], [2, 2]]
        up_diag = [[0, 2], [1, 1], [2, 0]]

        [down_diag, up_diag].map do |diag|
            # Note the `row, col` inside the block; this unpacks, or
            # "destructures" the argument. Read more here:
            # http://tony.pitluga.com/2011/08/08/destructuring-with-ruby.html
            diag.map { |row, col| @grid[row][col] }
        end
    end

    def dup
        duped_rows = rows.map(&:dup)
        self.class.new(duped_rows)
    end

    def self.isValid?(pos)
        rowIdx, colIdx = pos[0], pos[1]
        [rowIdx, colIdx].all? { |coord| (0..2).include?(coord) }
    end

    def isEmpty?(pos)
        self[pos].nil?
    end

    def isTied?
        return false if self.isWon?

        # no empty space?
        @grid.all? { |row| row.none? { |el| el.nil? }}
    end

    def isOver?
        # don't use Ruby's `or` operator; always prefer `||`
        self.isWon? || self.isTied?
    end

    def winner
        rows = grid
        (rows + cols + diagonals).each do |triple|
            return :x if triple == [:x, :x, :x]
            return :o if triple == [:o, :o, :o]
        end

        nil
    end

    def isWon?
        !winner.nil?
    end
end
