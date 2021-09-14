require 'rspec'
require 'Player'

describe Player do
    before(:each) do
        @test_player = Player.new("\u{274E}")
    end

    describe '#initialize' do
        it 'sets up the instance variable mark' do
            expect(@test_player.mark).to eq("\u{274E}")
        end
    end
end
