using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;


namespace PolishDraughts
{
    public class Game
    {
        int _boardSize;
        Board _board;
        Player _currentPlayer;

        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }


        public Game(Player player1, Player player2, int boardSize)
        {
            Player1 = player1;
            Player2 = player2;
            _boardSize = boardSize;
        }


        public void Start()
        {

            Console.WriteLine();
            _board = new Board(_boardSize);
            _currentPlayer = Player1;
        }


        public bool Round()
        {
            if (EndOfGame())
                return false;

            _currentPlayer = _currentPlayer == Player1 ? Player2 : Player1;

            Program.Moves++;
            if (ContainsBlack() == false)
            {
                return false;
            }
            else if (ContainsWhite() == false)
            {
                return false;
            }

            return true;
        }

        public bool EndOfGame()
        {
            return false;
        }


        public bool TryToMakeMove(int x1, int y1, int x2, int y2)
        {

            // handle different circumstances, under which moving the Pawn shall be possible or not: 

            bool isMoveIncorrect = true;

            // empty field is chosen,  
            if (_board.Fields[x1, y1] == null)
            {
                Console.WriteLine("Error: \"you didn't chose any pawn!\" <-- ");
                return isMoveIncorrect;
            }

            // or occupied field is target field:
            else if (!(_board.Fields[x2, y2] == null))
            {
                Console.WriteLine("Error: \"target field is being occupied!\" <-- ");
                return isMoveIncorrect;
            }
            // picking up opponents pawns during one's round handled:
            if ((_currentPlayer.IsWhite == true && !_board.Fields[x1, y1].IsWhite) ||
                (_currentPlayer.IsWhite == false && _board.Fields[x1, y1].IsWhite))
            {
                Console.WriteLine("Error: \"this pawn belongs to your opponent!\" <--");
                return isMoveIncorrect;
            }

            // any other circumstances (jumping, multi jumping, not diagonal move etc..) are handled by
            // GetPossibleCoordinatesFor() method
            else
            {
                List<int[]> possibleMovesFor = GetPossibleCoordinatesFor(x1, y1);

                int[] finalCoordinates = new int[] { x2, y2 };

                foreach (var element in possibleMovesFor)
                {
                    if ((element[0] == finalCoordinates[0]) && (element[1] == finalCoordinates[1]))
                    {
                        // actuall "move" is being made here: 
                        _board.Fields[x2, y2] = _board.Fields[x1, y1];
                        _board.Fields[x1, y1] = null;

                        // removing opoonents pawn, IF CAPTURED ;)
                        if ((Math.Abs(x1 - x2) != 1))
                        {
                            _board.RemovePawn((x1 + x2)/2, (y1 + y2)/2);
                            Program.Moves = -1;
                        }

                        return !isMoveIncorrect;
                    }
                }
                Console.WriteLine("Error: \"this move is forbidden!\" <--");
                return isMoveIncorrect;
            }


            // this method should be expanded with multi-jumping and "seeing" more then just
            // four (if possible because of board dimension) diagonal fields around the pawn, as it now does. 
            List<int[]> GetPossibleCoordinatesFor(int x, int y)
            {
                List<int[]> possibleMovesList = new List<int[]>();
                int[] coordinates = new int[2];

                coordinates[0] = x + 1;
                coordinates[1] = y + 1;
                if (coordinates[0] < _boardSize && coordinates[1] < _boardSize)
                {
                    if (_board.Fields[coordinates[0], coordinates[1]] == null)
                    {
                        int[] coordToAdd = coordinates.ToArray();
                        possibleMovesList.Add(coordToAdd);
                    }
                    else 
                    {
                        if (_board.Fields[coordinates[0], coordinates[1]].IsWhite == !_currentPlayer.IsWhite)
                        {
                            coordinates[0]++;
                            coordinates[1]++;
                            if (coordinates[0] < _boardSize && coordinates[1] < _boardSize)
                            {
                                if (_board.Fields[coordinates[0], coordinates[1]] == null)
                                {                                 
                                    int[] coordToAdd = coordinates.ToArray();
                                    possibleMovesList.Add(coordToAdd);
                                }
                            }                           
                        }                      
                    }
                }
                
                coordinates[0] = x - 1;
                coordinates[1] = y - 1;
                if (coordinates[0] >= 0 && coordinates[1] >= 0)
                {
                    if (_board.Fields[coordinates[0], coordinates[1]] == null)
                    {
                        int[] coordToAdd = coordinates.ToArray();
                        possibleMovesList.Add(coordToAdd);
                    }
                    else
                    {
                        if (_board.Fields[coordinates[0], coordinates[1]].IsWhite == !_currentPlayer.IsWhite)
                        {
                            coordinates[0]--;
                            coordinates[1]--;
                            if (coordinates[0] >= 0 && coordinates[1] >= 0)
                            {
                                if (_board.Fields[coordinates[0], coordinates[1]] == null)
                                {
                                    int[] coordToAdd = coordinates.ToArray();
                                    possibleMovesList.Add(coordToAdd);
                                }
                            }
                        }
                    }
                }

                coordinates[0] = x - 1;
                coordinates[1] = y + 1;
                if (coordinates[0] >= 0 && coordinates[1] < _boardSize)
                {
                    if (_board.Fields[coordinates[0], coordinates[1]] == null)
                    {
                        int[] coordToAdd = coordinates.ToArray();
                        possibleMovesList.Add(coordToAdd);
                    }
                    else
                    {
                        if (_board.Fields[coordinates[0], coordinates[1]].IsWhite == !_currentPlayer.IsWhite)
                        {
                            coordinates[0]--;
                            coordinates[1]++;
                            if (coordinates[0] >= 0 && coordinates[1] < _boardSize)
                            {
                                if (_board.Fields[coordinates[0], coordinates[1]] == null)
                                {
                                    int[] coordToAdd = coordinates.ToArray();
                                    possibleMovesList.Add(coordToAdd);
                                }
                            }
                        }
                    }
                }

                coordinates[0] = x + 1;
                coordinates[1] = y - 1;
                if (coordinates[0] < _boardSize && coordinates[1] >= 0)
                {
                    if (_board.Fields[coordinates[0], coordinates[1]] == null)
                    {
                        int[] coordToAdd = coordinates.ToArray();
                        possibleMovesList.Add(coordToAdd);
                    }

                    else
                    {
                        if (_board.Fields[coordinates[0], coordinates[1]].IsWhite == !_currentPlayer.IsWhite)
                        {
                            coordinates[0]++;
                            coordinates[1]--;
                            if (coordinates[0] < _boardSize && coordinates[1] >= 0)
                            {
                                if (_board.Fields[coordinates[0], coordinates[1]] == null)
                                {
                                    int[] coordToAdd = coordinates.ToArray();
                                    possibleMovesList.Add(coordToAdd);
                                }
                            }

                        }
                    }
                }

                return possibleMovesList;
            }
        }


        /// <summary>
        /// Returns Player who won. If draw returns null
        /// </summary>
        /// <returns></returns>
        /// 


        public bool ContainsWhite()
        {
            for (int i = 0; i < _board.Fields.GetLength(0); i++)
            {
                for (int j = 0; j < _board.Fields.GetLength(1); j++)
                {
                    ///if (_board.Fields[i, j] == Pawn && Pawn.isWhite == true)
                    if (_board.Fields[i, j] != null && _board.Fields[i, j].IsWhite == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public bool ContainsBlack()
        {
            for (int i = 0; i < _board.Fields.GetLength(0); i++)
            {
                for (int j = 0; j < _board.Fields.GetLength(1); j++)
                {
                    ///if (_board.Fields == Pawn && Pawn.isWhite == false)
                    if (_board.Fields[i, j] != null && _board.Fields[i, j].IsWhite == false)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Player CheckForWinner()
        {
            Player player=null;
           
            if (ContainsWhite() == false)
            {
                ASCII.BlackWon();
                player = Player1.IsWhite ? Player2 : Player1;
            }
            else if (ContainsBlack() == false)
            {
                ASCII.WhiteWon();
                player = Player1.IsWhite ? Player1 : Player2;
            };

            /// jeszcze nie skończone
            return player;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"\n   Player {_currentPlayer.Name} turn.\n");
            stringBuilder.Append(_board.ToString());

            return stringBuilder.ToString();
        }

    }
}