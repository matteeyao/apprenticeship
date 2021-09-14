require 'rspec'
require 'EasyPlayer'

describe EasyPlayer do
    before(:each) do
        @computer_player = EasyPlayer.new
    end

    describe '#initialize' do
        it 'sets up the instance variables' do
            expect(@computer_player.name).to eq("Easybot")
            expect(@computer_player.mark).to eq("\u{2B55}")
        end
    end
end
