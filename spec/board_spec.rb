require 'rspec'
require 'Board'

describe Board do
    before(:each) do
        @empty_board = Board.new
    end

    let(:filled_board) do
        filled_board = Board.new

        for rowIdx in (0...filled_board.grid.length)
            for colIdx in (0...filled_board.grid[rowIdx].length)
                filled_board[[rowIdx, colIdx]] = :x
            end
        end

        filled_board
    end

    describe '@marks' do
        it 'should have two different marks' do
            expect(Board.marks.length).to eq(2)
            expect(Board.marks[0]).not_to eq(Board.marks[1])
        end
    end

    describe '#initialize' do
        it 'sets up the instance variable grid' do
            expect(@empty_board.grid.length).to eq(3)
            @empty_board.grid.each { |row| expect(row.length).to eq(3)}
            expect(@empty_board.grid.flatten.length).to eq(9)
        end

        it 'should start out empty' do
            for row in @empty_board.grid do
                for col in row do      
                    expect(col).to be_nil
                end
            end
        end
    end

    describe '@is_valid?' do
        it 'should return false when x or y is less than 0' do
            expect(Board.is_valid?([-1, 1])).to be false
            expect(Board.is_valid?([1, -1])).to be false
        end

        it 'should return false when x or y is greater than 2' do
            expect(Board.is_valid?([3, 1])).to be false
            expect(Board.is_valid?([1, 3])).to be false
        end

        it 'should return true otherwise' do
            expect(Board.is_valid?([2, 2])).to be true
        end
    end

    describe '#is_empty?' do
        it 'should return a mark for an occupied position' do
            expect(Board.marks.include?(filled_board[[0, 0]])).to be_truthy()
        end

        it 'should return undefined for an empty position' do
            expect(@empty_board[[0, 0]]).to be_nil
        end

        it 'should throw an error for an invalid position' do
            expect{@empty_board[[3, 3]]}.to raise_error(RuntimeError, "invalid position was entered")
            expect{@empty_board[[3, 1]] = :x}.to raise_error(RuntimeError, "invalid position was entered")
            expect{filled_board[[0, -1]]}.to raise_error(RuntimeError, "invalid position was entered")
        end
    end

    describe '#place_mark' do
        it 'should throw an error for a non-empty position' do
            for rowIdx in (0...@empty_board.grid.length)
                for colIdx in (0...@empty_board.grid[rowIdx].length)
                    @empty_board[[rowIdx, colIdx]] = :x
                    expect{@empty_board[[0, 0]] = :o}.to raise_error(RuntimeError, "mark already placed there")
                end
            end
        end

        it 'should correctly place mark on a position' do
            @empty_board[[1, 1]] = :x
            expect(@empty_board.grid).to eq([
                [nil, nil, nil],
                [nil, :x, nil],
                [nil, nil, nil],
            ]);
        end
    end

    describe '#winner' do
        it 'should return undefined for an empty board' do
            expect(@empty_board.winner).to be_nil
        end

        it 'should return :x for a board filled with x\'s' do
            expect(filled_board.winner).to eq(:x)
        end

        it 'should return :o for a board filled with o\'s in the middle row' do
            for idx in (0...3) do
                @empty_board[[1, idx]] = :o
            end
            expect(@empty_board.winner).to eq(:o)
        end

        it 'should return :o for a board filled with o\'s in the middle column' do
            for idx in (0...3) do
                @empty_board[[idx, 1]] = :o
            end
            expect(@empty_board.winner).to eq(:o)
        end

        it 'should return :x for a board filled with x\'s in the top-right to bottom-left diagonal' do
            for idx in (0...3) do
                @empty_board[[idx, idx]] = :x
            end
            expect(@empty_board.winner).to eq(:x)
        end

        it 'should return :o for a board filled with o\'s in the top-left to bottom-right diagonal' do
            for idx in (0...3) do
                @empty_board[[2 - idx, idx]] = :o
            end
            expect(@empty_board.winner).to eq(:o)
        end
    end

    describe '#is_won?' do
        it 'should return false for an empty board' do
            expect(@empty_board.is_won?).to be false
        end

        it 'should return true for an empty board' do
            expect(filled_board.is_won?).to be true
        end
    end

    describe '#is_tied?' do
        it 'should return false for an empty board' do
            expect(@empty_board.is_tied?).to be false
        end

        it 'should return false for a board filled with :x\'s' do
            expect(filled_board.is_tied?).to be false
        end

        it 'should return false for a partially filled board with no winners' do
            @empty_board[[0, 0]] = :x
            @empty_board[[0, 1]] = :o
            @empty_board[[0, 2]] = :x
            @empty_board[[1, 1]] = :o
            @empty_board[[1, 2]] = :o
            @empty_board[[2, 0]] = :o
            @empty_board[[2, 1]] = :x
            @empty_board[[2, 2]] = :x
            expect(@empty_board.is_tied?).to be false
        end

        it 'should return true for a filled board with no winners' do
            @empty_board[[0, 0]] = :x
            @empty_board[[0, 1]] = :o
            @empty_board[[0, 2]] = :x
            @empty_board[[1, 0]] = :x
            @empty_board[[1, 1]] = :o
            @empty_board[[1, 2]] = :o
            @empty_board[[2, 0]] = :o
            @empty_board[[2, 1]] = :x
            @empty_board[[2, 2]] = :x
            expect(@empty_board.is_tied?).to be true
        end 
    end

    describe '#is_over?' do
        it 'should return false for an empty board' do
            expect(@empty_board.is_over?).to be false
        end

        it 'should return true for a board filled with :x\s' do
            expect(filled_board.is_over?).to be true
        end

        it 'should return false for a partially filled board with no winners' do
            @empty_board[[0, 0]] = :x
            @empty_board[[0, 1]] = :o
            @empty_board[[0, 2]] = :x
            @empty_board[[1, 1]] = :o
            @empty_board[[1, 2]] = :o
            @empty_board[[2, 0]] = :o
            @empty_board[[2, 1]] = :x
            @empty_board[[2, 2]] = :x
            expect(@empty_board.is_tied?).to be false
        end

        it 'should return true for a filled board with no winners' do
            @empty_board[[0, 0]] = :x
            @empty_board[[0, 1]] = :o
            @empty_board[[0, 2]] = :x
            @empty_board[[1, 0]] = :x
            @empty_board[[1, 1]] = :o
            @empty_board[[1, 2]] = :o
            @empty_board[[2, 0]] = :o
            @empty_board[[2, 1]] = :x
            @empty_board[[2, 2]] = :x
            expect(@empty_board.is_tied?).to be true
        end 
    end
end
