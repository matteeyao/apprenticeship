require 'rspec'
require 'game'

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

    
end
