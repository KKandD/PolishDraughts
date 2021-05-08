using System;

namespace PolishDraughts
{
    class Program
    {
        public static int Moves { get; set; }

        static void Main()
        {
            Player player1 = null;
            Player player2 = null;
            int boardSize = 8;

            Console.Clear();
            ASCII.Welcome();
            ASCII.WaitForKey();

            boardSize = AskUserAboutBoardSize();
      
            Console.Clear();
            ASCII.Welcome();
            ASCII.Player1();
            player1 = AskUserAboutPlayer(null);

            Console.Clear();
            ASCII.Welcome();
            ASCII.Player2();
            player2 = AskUserAboutPlayer(!player1.IsWhite);

            var game = new Game(player1, player2, boardSize);

            game.Start();

            do
            {
                Console.Clear();
                ASCII.Welcome();
                Console.WriteLine($"{ game.ToString()}");

                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;
                do
                {
                    (x1, y1, x2, y2) = AskForMove(boardSize);
                } while (game.TryToMakeMove(x1, y1, x2, y2));

            } while (game.Round());

            Console.Clear();
            Console.WriteLine(game.ToString());

            var winner = game.CheckForWinner();

            PrintWinner(winner);
            ASCII.WaitForKey();
            AskForNewGame();
        }

        private static (int, int, int, int) AskForMove(int boardSize)
        {
            int x1 = 0;
            int x2 = 0;
            int y1 = 0;
            int y2 = 0;
            bool isCorrect = false;

            while (!isCorrect)
            {

                if (CheckForDraw(boardSize))
                {
                    AskForNewGame();
                }
                Console.WriteLine(@"
    < Correct input format is e.g: 'a1 b2', where 'a1' is coordianate for pawn you want to move, 
    and 'b2' is coordinate where you want to move pawn >


|   PROVIDE YOUR MOVE ('m' for menu):
|
v
");
                //Console.WriteLine("         MOVES without capturing: " + Moves);
                var input = Console.ReadLine().ToLower();
                var moveArray = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (input == "m")
                {
                    AskForNewGame();
                }
                if (moveArray.Length < 2 || moveArray[0].Length < 2 || moveArray[1].Length < 2)
                {
                    Console.WriteLine("         Incorrect input");
                    continue;
                }

                char start = 'a';

                y1 = moveArray[0][0] - start; //Getting first char of first sequence
                y2 = moveArray[1][0] - start; //Getting first char of second sequence

                if (!int.TryParse(moveArray[0].Substring(1), out x1))
                {
                    Console.WriteLine("         Incorrect input");
                    continue;
                }

                if (!int.TryParse(moveArray[1].Substring(1), out x2))
                {
                    Console.WriteLine("         Incorrect input");
                    continue;
                }

                x1--;
                x2--;

                if ((y1 < 0 || y1 >= boardSize) ||
                    (y2 < 0 || y2 >= boardSize) ||
                    (x1 < 0 || x1 >= boardSize) ||
                    (x2 < 0 || x2 >= boardSize))
                {
                    Console.WriteLine("         Incorrect input");
                    continue;
                }
                else
                {
                    isCorrect = true;
                }
            }
            return (x1, y1, x2, y2);
        }


        private static bool CheckForDraw(int boardSize)
        {
            if (boardSize < 10)
            {
                if (!(Moves < 20))
                {
                    Console.Clear();
                    Console.WriteLine("IT'S A DRAW");
                    return true;
                }
            }
            else if (boardSize >= 10)
            {
                if (!(Moves < 25))
                {
                    Console.Clear();
                    Console.WriteLine("IT'S A DRAW");
                    return true;
                }
            }
            return false;
        }


        private static void AskForNewGame()
        {
            bool check = true;

            do
            {
                Console.Clear();
                ASCII.Welcome();
                Console.WriteLine("     Create new Game? ('y' to play, 'n' to exit)");
                var newGame = Console.ReadLine();
                var newGame1 = newGame.ToLower();


                if (newGame1 == "y")
                {
                    Program.Moves = 0;
                    Main();

                }
                else if (newGame1 == "n")
                {
                    Environment.Exit(0);

                }
                else if (newGame1 != "y" && newGame1 != "n")
                {
                    Console.WriteLine("         Please type (y) for new game or (n) to exit.");

                }
            } while (check);
        }


        private static void PrintWinner(Player winner)
        {
            Console.WriteLine("         END OF GAME \n The result of game:");

            if (winner == null)
                Console.WriteLine("         DRAW");
            else
                Console.WriteLine($"            THE WINNER IS: {winner.Name}");
        }

        private static int AskUserAboutBoardSize()
        {
            int boardSize;
            do
            {
                Console.Clear();
                ASCII.Welcome();
                Console.WriteLine("\n           Please provide board size (range between 8-20)");

                var input = Console.ReadLine();
                if (!int.TryParse(input, out boardSize))
                {
                    Console.WriteLine($"            Incorrect input...\nYou typed \"{boardSize}\". Did you mean something else?");
                }

            } while (boardSize < 8 || boardSize > 20);

            return boardSize;
        }

        private static Player AskUserAboutPlayer(bool? isWhite)
        {
            int playerType;
            Player player;

            do
            {
                
                Console.WriteLine(@"             
                Please choose type of player:  

                1 - Human player             
                2 - AI player 

                For continue press <ENTER>

");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out playerType))
                {
                    
                    Console.WriteLine("             Incorrect input. Please provide correct option.");
                    Console.WriteLine("");
                }
                else if (playerType == 2)
                {
                    
                    Console.WriteLine("             Sorry not implemented yet....");
                    playerType = 0;
                }
            } while (playerType == 0 || playerType > 2);

            if (!isWhite.HasValue)
            {
                //Ask for color
                do
                {
                    
                    
                    Console.WriteLine("             Please choose pawns color: \n              (W) - white \n              (B) - black");

                    var color = Console.ReadLine().Trim();

                    if (color.ToLower() == "w")
                        isWhite = true;
                    else if (color.ToLower() == "b")
                        isWhite = false;
                    else
                        
                        Console.WriteLine("             Incorrect input. Try again...");


                } while (!isWhite.HasValue);
            }

            string name = "";
            do
            {
                
                
                Console.WriteLine("             Please provide player name");
                name = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(name));

            if (playerType == 1)
                player = new Player
                {
                    IsWhite = isWhite.Value,
                    Name = name
                };
            else
            {
                throw new NotImplementedException("             AI player is not implemented. Sorry...");
            }

            Console.WriteLine();
            Console.WriteLine("             Player created.");

            return player;
        }


    }
}
