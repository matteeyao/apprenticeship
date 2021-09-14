require 'rspec'
require 'Player'
require 'Game'


require 'stringio'

describe Game do
    before(:each) do
        @player_one = Player.new("Player One", "\u{274C}")
        @player_two = Player.new("Player Two", "\u{2B55}")
        @game = Game.new(@player_one, @player_two)
    end

    describe '#initialize' do
        it 'sets up the instance variables' do
            expect(@game.board).not_to be_nil
            expect(@game.players).to eq({ :x => @player_one, :o => @player_two })
            expect(@game.turn).to eq(:x)
        end
    end

    describe '#show' do
        it 'prints an empty board with no marks' do
            expect do
                @game.show
            end.to output("   |   |   \n-----------\n   |   |   \n-----------\n   |   |   \n\n")
                .to_stdout
        end

        it 'prints a board with several marks played' do
            @game.board[[0, 2]] = :x
            @game.board[[1, 1]] = :o
            @game.board[[2, 2]] = :x
            @game.board[[1, 2]] = :o
            expect do
                @game.show
            end.to output("   |   | \u{274C} \n-----------\n   | \u{2B55} | \u{2B55} \n-----------\n   |   | \u{274C} \n\n")
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
            end.to output(" \u{2B55} | \u{274C} | \u{274C} \n-----------\n \u{274C} | \u{2B55} | \u{2B55} \n-----------\n \u{274C} | \u{2B55} | \u{274C} \n\n")
                .to_stdout
        end
    end

    # describe '#play_turn' do
        # subject { @player_one.get_pos }

        # after { subject }

        # context 'a pos has been given' do
        #     before do
        #         allow(@player_one).to receive(:get_pos) { '2,2' }
        #     end

        #     it 'breaks the loop' do
        #         expect(@player_one).to receive(:get_pos).once
        #     end
        # end
        # Google search term: rspec ignore loop
        # RSpec test breaking loops https://gist.github.com/TimothyClayton/7c9fd2e3389ee07f13e07d92aff02b11
    # end

    # describe '#swap_turn' do
    #     it 'switches to the next player after being invoked' do
    #         expect(@game.players[@game.turn].name).to eq("Player One")
    #         @game.swap_turn
    #         expect(@game.players[@game.turn].name).to eq("Player Two")
    #     end
    # end

    # describe 'print_results' do
    #     before(:each) do
    #         @player_one = Player.new("Player One")
    #         @player_two = Player.new("Player Two")
    #         @game = Game.new(@player_one, @player_two)
    #     end

    #     context 'if the game ends in a win' do
    #         before do
    #             for i in (0...3) do
    #                 for j in (0...3) do
    #                     @game.board[[i, j]] = :x
    #                 end
    #             end
    #         end

    #         it "should print 'Player One won the game!'" do
    #             expect { @game.print_results }
    #                 .to output(" x | x | x \n-----------\n x | x | x \n-----------\n x | x | x \n\nPlayer One won the game!\n\n")
    #                 .to_stdout
    #         end
    #     end

    #     context 'if the game ends in a draw' do
    #         before do
    #             @game.board[[0, 0]] = :x
    #             @game.board[[0, 1]] = :x
    #             @game.board[[0, 2]] = :o
    #             @game.board[[1, 0]] = :o
    #             @game.board[[1, 1]] = :o
    #             @game.board[[1, 2]] = :x
    #             @game.board[[2, 0]] = :x
    #             @game.board[[2, 1]] = :o
    #             @game.board[[2, 2]] = :x    
    #         end

    #         it "should print 'No one wins!'" do
    #             expect { @game.print_results }
    #                 .to output(" x | x | o \n-----------\n o | o | x \n-----------\n x | o | x \n\nNo one wins!\n\n")
    #                 .to_stdout
    #         end
    #     end
    # end


    # def get_pos
    #     $stdin.gets.chomp.split(",").map(&:to_i)
    # end

    # describe '#get_input' do
    #     before do
    #         $stdin = StringIO.new("2,2")
    #     end

    #     after do
    #         $stdin = STDIN
    #     end

    #     it 'returns coordinates in an array from input' do
    #         expect(get_pos).to eq([2,2])
    #     end
    # end

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
