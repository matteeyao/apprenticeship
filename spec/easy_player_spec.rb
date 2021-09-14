require 'rspec'
require 'EasyPlayer'
require 'Game'
require 'Player'

describe EasyPlayer do
    before(:each) do
        @test_player = Player.new("\u{274C}")
        @computer_player = EasyPlayer.new
        @game = Game.new(@test_player, @computer_player)
    end

    describe '#initialize' do
        it 'sets up the instance variables' do
            expect(@computer_player.mark).to eq("\u{2B55}")
        end
    end

    describe '#random_move' do
        it 'should fill board if called nine times' do
            9.times do |i| 
                pos = @computer_player.random_move(@game)
                @game.board[pos] = :o
            end
            expect(@game.board.is_over?).to be true
        end
    end
end
