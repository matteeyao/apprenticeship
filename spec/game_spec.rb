require 'HumanPlayer'
require 'Game'
require 'rspec'
require 'stringio'

describe Game do
    before(:each) do
        @human_player_one = HumanPlayer.new("\u{274C}")
        @human_player_two = HumanPlayer.new("\u{2B55}")
        @game = Game.new(@human_player_one, @human_player_two)
    end

    describe '#initialize' do
        it 'sets up the instance variables' do
            expect(@game.board).not_to be_nil
            expect(@game.players).to eq({ :x => @human_player_one, :o => @human_player_two })
            expect(@game.turn).to eq(:x)
        end
    end

    describe '#show' do
        it 'prints an empty board with no marks' do
            expect do
                @game.show
            end.to output("   |    |   \n------------\n   |    |   \n------------\n   |    |   \n\n")
                .to_stdout
        end

        it 'prints a board with several marks played' do
            @game.board[[0, 2]] = :x
            @game.board[[1, 1]] = :o
            @game.board[[2, 2]] = :x
            @game.board[[1, 2]] = :o
            expect do
                @game.show
            end.to output("   |    | \u{274C}\n------------\n   | \u{2B55} | \u{2B55}\n------------\n   |    | \u{274C}\n\n")
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
            end.to output("\u{2B55} | \u{274C} | \u{274C}\n------------\n\u{274C} | \u{2B55} | \u{2B55}\n------------\n\u{274C} | \u{2B55} | \u{274C}\n\n")
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

    describe '#swap_turn' do
        it 'switches to the next player after being invoked' do
            expect(@game.players[@game.turn].mark).to eq("\u{274C}")
            @game.swap_turn
            expect(@game.players[@game.turn].mark).to eq("\u{2B55}")
        end
    end

    describe 'print_results' do
        context 'if the game ends in a win' do
            before do
                for i in (0...3) do
                    for j in (0...3) do
                        @game.board[[i, j]] = :x
                    end
                end
            end

            it "should print '\u{274C} won the game!'" do
                expect { @game.print_results }
                    .to output("\u{274C} | \u{274C} | \u{274C}\n------------\n\u{274C} | \u{274C} | \u{274C}\n------------\n\u{274C} | \u{274C} | \u{274C}\n\n\u{274C} won the game!\n\n")
                    .to_stdout
            end
        end

        context 'if the game ends in a draw' do
            before do
                @game.board[[0, 0]] = :x
                @game.board[[0, 1]] = :x
                @game.board[[0, 2]] = :o
                @game.board[[1, 0]] = :o
                @game.board[[1, 1]] = :o
                @game.board[[1, 2]] = :x
                @game.board[[2, 0]] = :x
                @game.board[[2, 1]] = :o
                @game.board[[2, 2]] = :x    
            end

            it "should print 'No one wins!'" do
                expect { @game.print_results }
                    .to output("\u{274C} | \u{274C} | \u{2B55}\n------------\n\u{2B55} | \u{2B55} | \u{274C}\n------------\n\u{274C} | \u{2B55} | \u{274C}\n\nNo one wins!\n\n")
                    .to_stdout
            end
        end
    end
end
