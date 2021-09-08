require 'rspec'
require 'Game'

describe Game do
    before(:each) do
        @game = Game.new("Player One", "Player Two")
    end

    describe '#initialize' do
        it 'sets up the instance variables' do
            expect(@game.board).not_to be_nil
            expect(@game.players).to eq({ :x => "Player One", :o => "Player Two" })
            expect(@game.turn).to eq(:x)
        end
    end

    describe '#show' do
        it 'prints an empty board with no marks' do
            expect do
                @game.show
            end.to output("   |   |   \n-----------\n   |   |   \n-----------\n   |   |   \n")
                .to_stdout
        end

        it 'prints a board with several marks played' do
            @game.board[[0, 2]] = :x
            @game.board[[1, 1]] = :o
            @game.board[[2, 2]] = :x
            @game.board[[1, 2]] = :o
            expect do
                @game.show
            end.to output("   |   | x \n-----------\n   | o | o \n-----------\n   |   | x \n")
                .to_stdout
        end

        it 'prints a board with all spots filled' do
            @game.board[[0, 2]] = :x
            @game.board[[1, 1]] = :o
            @game.board[[2, 2]] = :x
            @game.board[[1, 2]] = :o
            @game.board[[1, 0]] = :x
            @game.board[[0, 0]] = :o
            @game.board[[2, 0]] = :x
            @game.board[[2, 1]] = :o
            @game.board[[0, 1]] = :x
            expect do
                @game.show
            end.to output(" o | x | x \n-----------\n x | o | o \n-----------\n x | o | x \n")
                .to_stdout
        end
    end
end
