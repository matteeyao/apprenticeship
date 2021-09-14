require 'rspec'
require 'HumanPlayer'
require 'Game'

describe HumanPlayer do
    before(:each) do
        @test_player_one = HumanPlayer.new("\u{274C}")
        @test_player_two = HumanPlayer.new("\u{2B55}")
        @test_game = Game.new(@test_player_one, @test_player_two)
    end

    describe '@position_dictionary' do
        it 'should have nine different positional key-value pairs' do
            expect(@test_player_one.class.position_dictionary.length()).to eq(9)
        end
    end

    describe '#initialize' do
        it 'sets up the instance variables' do
            expect(@test_player_one.mark).to eq("\u{274C}")
            expect(@test_player_two.mark).to eq("\u{2B55}")
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
            expect(@test_player_one.convert_input_to_pos("1")).to eq([0, 0])
            expect(@test_player_two.convert_input_to_pos("2")).to eq([0, 1])
            expect(@test_player_one.convert_input_to_pos("3")).to eq([0, 2])
            expect(@test_player_two.convert_input_to_pos("4")).to eq([1, 0])
            expect(@test_player_one.convert_input_to_pos("5")).to eq([1, 1])
            expect(@test_player_two.convert_input_to_pos("6")).to eq([1, 2])
            expect(@test_player_one.convert_input_to_pos("7")).to eq([2, 0])
            expect(@test_player_two.convert_input_to_pos("8")).to eq([2, 1])
            expect(@test_player_one.convert_input_to_pos("9")).to eq([2, 2])
        end
    end

    describe '#is_valid_input?' do
        context 'if the input is included in position dictionary\'s keys' do
            it 'should returns true' do
                expect(@test_player_one.is_valid_input?("1")).to be true
                expect(@test_player_one.is_valid_input?("2")).to be true
                expect(@test_player_one.is_valid_input?("3")).to be true
                expect(@test_player_one.is_valid_input?("4")).to be true
                expect(@test_player_one.is_valid_input?("5")).to be true
                expect(@test_player_one.is_valid_input?("6")).to be true
                expect(@test_player_one.is_valid_input?("7")).to be true
                expect(@test_player_one.is_valid_input?("8")).to be true
                expect(@test_player_one.is_valid_input?("9")).to be true
            end
        end

        context 'if input is not included in position dictionary\'s keys' do
            it 'should return false' do
                expect(@test_player_one.is_valid_input?("")).to be false
                expect(@test_player_one.is_valid_input?("0")).to be false
                expect(@test_player_one.is_valid_input?("10")).to be false
                expect(@test_player_one.is_valid_input?("119")).to be false
                expect(@test_player_one.is_valid_input?("a")).to be false
                expect(@test_player_one.is_valid_input?("bb")).to be false
                expect(@test_player_one.is_valid_input?("z")).to be false
            end
        end
    end

    describe '#is_vacant_pos?' do
        context 'if the position is empty' do
            it 'should return true' do
                expect(@test_player_one.is_vacant_pos?(@test_game, "1")).to be true
                expect(@test_player_one.is_vacant_pos?(@test_game, "2")).to be true
                expect(@test_player_one.is_vacant_pos?(@test_game, "3")).to be true
                expect(@test_player_one.is_vacant_pos?(@test_game, "4")).to be true
                expect(@test_player_one.is_vacant_pos?(@test_game, "5")).to be true
                expect(@test_player_one.is_vacant_pos?(@test_game, "6")).to be true
                expect(@test_player_one.is_vacant_pos?(@test_game, "7")).to be true
                expect(@test_player_one.is_vacant_pos?(@test_game, "8")).to be true
                expect(@test_player_one.is_vacant_pos?(@test_game, "9")).to be true
            end
        end

        context 'if the position is taken' do
            before do
                for i in (0...3) do
                    for j in (0...3) do
                        @test_game.board[[i, j]] = :x
                    end
                end
            end

            it 'should return false' do
                expect(@test_player_one.is_vacant_pos?(@test_game, "1")).to be false
                expect(@test_player_one.is_vacant_pos?(@test_game, "2")).to be false
                expect(@test_player_one.is_vacant_pos?(@test_game, "3")).to be false
                expect(@test_player_one.is_vacant_pos?(@test_game, "4")).to be false
                expect(@test_player_one.is_vacant_pos?(@test_game, "5")).to be false
                expect(@test_player_one.is_vacant_pos?(@test_game, "6")).to be false
                expect(@test_player_one.is_vacant_pos?(@test_game, "7")).to be false
                expect(@test_player_one.is_vacant_pos?(@test_game, "8")).to be false
                expect(@test_player_one.is_vacant_pos?(@test_game, "9")).to be false
            end
        end
    end

    describe '#fetch_pos' do
        # context 'if the input is invalid' do
        #     it 'should print Invalid input position!' do
        #         expect { @test_player_one.fetch_pos(@test_game) }
        #             .to output("Invalid input position!\n\n")
        #             .to_stdout
        #     end
        # end

        # context 'if the input position is occupied on the grid' do
        #     it 'should print Position is already taken!' do
        #         expect { @test_player_one.is_valid_input?("") }
        #             .to output("Position is already taken!\n\n")
        #             .to_stdout
        #     end
        # end
    end
end
