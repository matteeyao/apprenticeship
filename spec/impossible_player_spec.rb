require "rspec"
require "Game"
require "ImpossiblePlayer"

describe ImpossiblePlayer do
    subject { ImpossiblePlayer.new }

    let(:winnable_game) do
        test_board = Board.new
        test_board[[0, 0]] = :x
        test_board[[0, 1]] = :x
        test_board[[1, 0]] = :o
        test_board[[1, 1]] = :o
        double("Game", board: test_board)
    end

    let(:bloackable_win_game) do
        test_board = Board.new
        test_board[[0, 0]] = :x
        test_board[[1, 0]] = :o
        test_board[[1, 1]] = :o
        double("Game", board: test_board)
    end

    let(:unwinnable_game) do
        test_board = Board.new
        test_board[[0, 0]] = :o
        test_board[[2, 2]] = :o
        test_board[[2, 0]] = :o
        double("Game", board: test_board)
    end

    let(:two_moves_to_victory_game) do
        test_board = Board.new
        test_board[[0, 0]] = :x
        test_board[[2, 0]] = :x
        test_board[[1, 0]] = :o
        test_board[[2, 1]] = :o
        double("Game", board: test_board)
    end

    describe "#move" do
        it "chooses winning move if one is available" do
            expect(subject.move(winnable_game, :x)).to eq([0, 2])
        end

        it "can pick a winner that is two moves away" do
            move = subject.move(two_moves_to_victory_game, :x)
            expected_moves = [[0, 2], [1, 1]]
            expect(expected_moves).to include(move)
        end

        it "blocks an opponent's winning move" do
            # remove our opportunity to win with one move
            expect(subject.move(bloackable_win_game, :x)).to eq([1, 2])
        end

        it "raises an error if it cannot find a winning or draw inducing move" do
            expect do
                subject.move(unwinnable_game, :x)
            end.to raise_error
        end
    end
end
