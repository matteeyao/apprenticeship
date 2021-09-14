require 'rspec'
require 'Player'

describe Player do
    before(:each) do
        @test_player = Player.new("Todd", "\u{274E}")
    end

    describe '#initialize' do
        it 'sets up the instance variable name' do
            expect(@test_player.name).to eq("Todd")
            expect(@test_player.mark).to eq("\u{274E}")
        end
    end
end
