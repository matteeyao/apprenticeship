require_relative "EasyPlayer"
require_relative "Game"
require_relative "HumanPlayer"

class UserInterface
    def prompt
        puts "Welcome to Tic-Tac-Toe!"
        puts "(1) Play against a friend"
        puts "(2) Play against an easy competitor"
        # puts "(3) Play against a super computer\n\n"
    end

    # TODO: Add unit test/spec for method
    def fetch_and_verify_input
        print "Choose one of the above options: "
        loop do            
            input = gets.to_i
            if is_valid_input?(input)
                return input
            end
            print "Invalid option. Choose again from options 1-3: "
        end
    end

    def is_valid_input?(input)
        return (1..3).to_a.include?(input)
    end

    # TODO: Add unit test/spec for method
    def launch(num)
        case num
        when 1
            initialize_and_start_custom_game
        when 2
            initialize_and_start_easy_game
        when 3
            initialize_and_start_impossible_game
        end
    end

    # TODO: Add unit test/spec for method
    def initialize_and_start_custom_game
        print "Enter a grid size greater than 2 (Hit enter to default to 3): "
        grid_size = gets.to_i
        grid_size = grid_size == 0 ? 3 : grid_size
        puts
        print "Enter player one's mark (Hit enter to default to \u{274C}): "
        player_one_mark = gets.chomp 
        player_one_mark = player_one_mark == "" ? "\u{274C}" : player_one_mark
        print "Enter player two's mark (Hit enter to default to \u{2B55}): "
        player_two_mark = gets.chomp 
        player_two_mark = player_two_mark == "" ? "\u{2B55}" : player_two_mark
        puts
        player_one = HumanPlayer.new(player_one_mark, grid_size)
        player_two = HumanPlayer.new(player_two_mark, grid_size)
        Game.new(player_one, player_two, grid_size).run
    end

    # TODO: Add unit test/spec for method
    def initialize_and_start_easy_game
        print "Enter a grid size greater than 2 (Hit enter to default to 3): "
        grid_size = gets.to_i
        grid_size = grid_size == 0 ? 3 : grid_size
        puts
        print "Enter your mark (Hit enter to default to \u{274C}): "
        human_player_mark = gets.chomp
        human_player_mark = human_player_mark == "" ? "\u{274C}" : human_player_mark
        puts
        human_player = HumanPlayer.new(human_player_mark, grid_size)
        comp_player = EasyPlayer.new
        Game.new(human_player, comp_player, grid_size).run
    end

    # TODO: Add unit test/spec for method
    # def initialize_and_start_impossible_game
    #     print "Enter your mark (Hit enter to default to \u{274C}): "
    #     human_player_mark = gets.chomp
    #     human_player_mark = human_player_mark == "" ? "\u{274C}" : human_player_mark
    #     puts
    #     human_player = HumanPlayer.new(human_player_mark)
    #     comp_player = ImpossiblePlayer.new
    #     Game.new(human_player, comp_player).run
    # end
end

# TODO: Add integration test/spec for script
if $PROGRAM_NAME == __FILE__
    text_interface = UserInterface.new
    text_interface.prompt
    num = text_interface.fetch_and_verify_input
    text_interface.launch(num)
end
