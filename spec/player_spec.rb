require 'rspec'
require 'stringio'
require 'Game'
require 'Player'

describe Player do
    before(:each) do
        @test_player = Player.new("Todd")
    end

    before(:each) do
        @test_game = Game.new("Todd", "Computer")
    end

    describe '#initialize' do
        it 'sets up the instance variable name' do
            expect(@test_player.name).to eq("Todd")
        end
    end

    def get_pos
        $stdin.gets.chomp.split(",").map(&:to_i)
    end

    describe '#get_input' do
        before do
            $stdin = StringIO.new("2,2")
        end

        after do
            $stdin = STDIN
        end

        it 'returns coordinates in an array from input' do
            expect(get_pos).to eq([2,2])
        end
    end

    describe '#check_input' do
        context 'if the input is valid' do
            it 'should return the input' do
                expect(@test_player.check_pos([1,1])).to eq([1,1])
            end
        end

        context 'if the input is invalid' do
            it 'should output Invalid coordinates!' do
                expect { @test_player.check_pos([0,3]) }
                    .to output("Invalid coordinates!\n")
                    .to_stdout
            end
        end
    end
end
