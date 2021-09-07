require 'rspec'
require 'board'

describe Board do
    let(:empty_board) do
        Board.new
    end

    describe '#initialize' do
        it 'sets up the instance variables' do
            expect(empty_board.grid.length).to eq(3)
            empty_board.grid.each { |row| expect(row.length).to eq(3)}
            expect(empty_board.grid.flatten.length).to eq(9)
        end
    end
end