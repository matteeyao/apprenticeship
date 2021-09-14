require 'rspec'
require 'HumanPlayer'

describe HumanPlayer do
    before(:each) do
        @human_player = HumanPlayer.new("Todd", "\u{274E}")
    end

    describe '#initialize' do
        it 'sets up the instance variables' do
            expect(@human_player.name).to eq("Todd")
            expect(@human_player.mark).to eq("\u{274E}")
        end
    end
end
