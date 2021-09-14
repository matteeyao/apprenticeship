require 'rspec'
require 'Main'

describe Main do
    before(:each) do
        @text_interface = Main.new
    end

    describe '#prompt' do
        it 'prompts the user for an input' do
            expect do
                @text_interface.prompt
            end.to output("Welcome! Choose one of the following options:\n(1) Play Tic-Tac-Toe against a friend\n(2) Play Tic-Tac-Toe against an easy competitor\n(3) Play Tic-Tac-Toe against a super computer\n\n")
                .to_stdout
        end
    end

    describe '#is_valid_input' do
        it 'returns true when input is valid' do
            expect(@text_interface.verify_input(1)).to be true
            expect(@text_interface.verify_input(2)).to be true
            expect(@text_interface.verify_input(3)).to be true
        end

        it 'returns false when input is invalid' do
            expect(@text_interface.verify_input(0)).to be false
            expect(@text_interface.verify_input(4)).to be false
            expect(@text_interface.verify_input(nil)).to be false         
        end
    end
end