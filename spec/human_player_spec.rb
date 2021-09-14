require 'rspec'
require 'HumanPlayer'
require 'Game'

describe HumanPlayer do
    before(:each) do
        @human_player_one = HumanPlayer.new("Player One", "\u{274C}")
        @human_player_two = HumanPlayer.new("Player Two", "\u{2B55}")
        @game = Game.new(@human_player_one, @human_player_two)
    end

    describe '@position_dictionary' do
        it 'should have nine different positional key-value pairs' do
            expect(HumanPlayer.position_dictionary.length()).to eq(9)
        end
    end

    describe '#initialize' do
        it 'sets up the instance variables' do
            expect(@human_player_one.name).to eq("Player One")
            expect(@human_player_one.mark).to eq("\u{274C}")
            expect(@human_player_two.name).to eq("Player Two")
            expect(@human_player_two.mark).to eq("\u{2B55}")
        end
    end

    def fetch_input
        $stdin.gets.chomp
    end

    describe '#fetch_input' do
        before do
            $stdin = StringIO.new("1")
        end

        after do
            $stdin = STDIN
        end

        it 'returns coordinates in an array from input' do
            expect(fetch_input).to eq("1")
        end
    end

    describe '#convert_input_to_pos' do
        it 'converts a inputted string number 1-9 to a valid tic-tac-toe grid coordinate' do
            expect(@human_player_one.convert_input_to_pos("1")).to eq([0, 0])
            expect(@human_player_two.convert_input_to_pos("2")).to eq([0, 1])
            expect(@human_player_one.convert_input_to_pos("3")).to eq([0, 2])
            expect(@human_player_two.convert_input_to_pos("4")).to eq([1, 0])
            expect(@human_player_one.convert_input_to_pos("5")).to eq([1, 1])
            expect(@human_player_two.convert_input_to_pos("6")).to eq([1, 2])
            expect(@human_player_one.convert_input_to_pos("7")).to eq([2, 0])
            expect(@human_player_two.convert_input_to_pos("8")).to eq([2, 1])
            expect(@human_player_one.convert_input_to_pos("9")).to eq([2, 2])
        end
    end

    describe '#is_valid_input?' do
        it 'returns true when input is included in position dictionary\'s keys' do
            expect(@human_player_one.is_valid_input?("1")).to be true
            expect(@human_player_one.is_valid_input?("2")).to be true
            expect(@human_player_one.is_valid_input?("3")).to be true
            expect(@human_player_one.is_valid_input?("4")).to be true
            expect(@human_player_one.is_valid_input?("5")).to be true
            expect(@human_player_one.is_valid_input?("6")).to be true
            expect(@human_player_one.is_valid_input?("7")).to be true
            expect(@human_player_one.is_valid_input?("8")).to be true
            expect(@human_player_one.is_valid_input?("9")).to be true
        end

        it 'returns false when input is not included in position dictionary\'s keys' do
            expect(@human_player_one.is_valid_input?("")).to be false
            expect(@human_player_one.is_valid_input?("0")).to be false
            expect(@human_player_one.is_valid_input?("10")).to be false
            expect(@human_player_one.is_valid_input?("119")).to be false
            expect(@human_player_one.is_valid_input?("a")).to be false
            expect(@human_player_one.is_valid_input?("bb")).to be false
            expect(@human_player_one.is_valid_input?("z")).to be false
        end
    end

    # describe '#check_input' do
    #     context 'if the input is valid' do
    #         it 'should return the input' do
    #             expect(@test_player.check_pos(@test_game,[1,1])).to eq([1,1])
    #         end
    #     end

    #     context 'if the input is invalid' do
    #         it 'should output Invalid coordinates!' do
    #             expect { @test_player.check_pos(@test_game,[0,3]) }
    #                 .to output("Invalid coordinates!\n\n")
    #                 .to_stdout
    #         end
    #     end

    #     context 'if the inputed pos is alraedy take' do
    #         it 'should output Position is already taken!' do
    #             expect { @test_player.check_pos(@test_game,[0,3]) }
    #                 .to output("Invalid coordinates!\n\n")
    #                 .to_stdout
    #         end
    #     end
    # end
end
