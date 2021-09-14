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
            end.to output("Welcome to Tic-Tac-Toe!\n(1) Play against a friend\n(2) Play against an easy competitor\n(3) Play against a super computer\n\n")
                .to_stdout
        end
    end

    describe '#is_valid_input?' do
        it 'returns true when input is valid' do
            expect(@text_interface.is_valid_input?(1)).to be true
            expect(@text_interface.is_valid_input?(2)).to be true
            expect(@text_interface.is_valid_input?(3)).to be true
        end

        it 'returns false when input is invalid' do
            expect(@text_interface.is_valid_input?(0)).to be false
            expect(@text_interface.is_valid_input?(4)).to be false
            expect(@text_interface.is_valid_input?(nil)).to be false         
        end
    end
end
