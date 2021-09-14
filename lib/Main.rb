require_relative 'Player'
require_relative 'EasyPlayer'
require_relative 'Game'

class Main
    def prompt
        puts "Welcome to Tic-Tac-Toe!"
        puts "(1) Play against a friend"
        puts "(2) Play against an easy competitor"
        puts "(3) Play against a super computer\n\n"
    end

    # TODO: Add unit test/spec for method
    def fetch_and_verify_input
        print "Choose one of the above options: "
        loop do            
            input = gets.to_i
            if is_valid_input(input)
                puts
                return input
            end
            print "Invalid option. Choose again from options 1-3: "
        end
    end

    def is_valid_input(input)
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
        print "Enter player one's name: "
        player_one_name = gets.chomp
        print "Enter player one's mark (Hit enter to default to \u{274C}): "
        player_one_mark = gets.chomp == "" ? "\u{274C}" : gets.chomp
        print "Enter player two's name: "
        player_two_name = gets.chomp
        print "Enter player two's mark (Hit enter to default to \u{2B55}): "
        player_two_mark = gets.chomp == "" ? "\u{2B55}" : gets.chomp
        puts
        player_one = Player.new(player_one_name, player_one_mark)
        player_two = Player.new(player_two_name, player_two_mark)
        Game.new(player_one, player_two).run
    end

    # TODO: Add unit test/spec for method
    def initialize_and_start_easy_game
        print "Enter your name: "
        player_one_name = gets.chomp
        print "Enter your mark (Hit enter to default to \u{274C}): "
        player_one_mark = gets.chomp == "" ? "\u{274C}" : gets.chomp
        puts
        human_player = Player.new(player_one_name, player_one_mark)
        comp_player = EasyPlayer.new
        Game.new(human_player, comp_player).run
    end

    # TODO: Add unit test/spec for method
    def initialize_and_start_impossible_game
        # print "Enter your name: "
        # player_one_name = gets.chomp
        # print "Enter your mark (Hit enter to default to \u{274C}): "
        # player_one_mark = gets.chomp == "" ? "\u{274C}" : gets.chomp
        # puts
        # human_player = Player.new(player_one_name, player_one_mark)
        # comp_player = EasyPlayer.new
        # Game.new(human_player, comp_player)
    end
end

# TODO: Add integration test/spec for script
if $PROGRAM_NAME == __FILE__
    text_interface = Main.new
    text_interface.prompt
    num = text_interface.fetch_and_verify_input
    text_interface.launch(num)
end
