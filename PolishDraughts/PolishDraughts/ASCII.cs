using System;
using System.Collections.Generic;
using System.Text;

namespace PolishDraughts
{
    public class ASCII
    {
        public static void Welcome()
        {
            var title = @"
-------------------------------------------------------------------------------------------------------------------------
[]                                                                                                                     []

                                            @ 2021 IZ & FB & JP
                                                    v. 1.0

                
                ███████████████████████████████████████████████████████████████▀█████████████████
                █▄─▄▄─█─▄▄─█▄─▄███▄─▄█─▄▄▄▄█─█─███▄─▄▄▀█▄─▄▄▀██▀▄─██▄─██─▄█─▄▄▄▄█─█─█─▄─▄─█─▄▄▄▄█
                ██─▄▄▄█─██─██─██▀██─██▄▄▄▄─█─▄─████─██─██─▄─▄██─▀─███─██─██─██▄─█─▄─███─███▄▄▄▄─█
                ▀▄▄▄▀▀▀▄▄▄▄▀▄▄▄▄▄▀▄▄▄▀▄▄▄▄▄▀▄▀▄▀▀▀▄▄▄▄▀▀▄▄▀▄▄▀▄▄▀▄▄▀▀▄▄▄▄▀▀▄▄▄▄▄▀▄▀▄▀▀▄▄▄▀▀▄▄▄▄▄▀

"


;
            Console.WriteLine(title);
         }

        public static void WaitForKey()
        {
            string wait = @"

                                            PRESS ANY KEY TO CONTINUE . . . 
";
            Console.WriteLine(wait);
            Console.ReadKey();
        }

        public static void BlackWon()
        {
            var blackWon = @"

            █▄▄ █░░ ▄▀█ █▀▀ █▄▀   █░█░█ █▀█ █▄░█
            █▄█ █▄▄ █▀█ █▄▄ █░█   ▀▄▀▄▀ █▄█ █░▀█
            
";
            Console.WriteLine(blackWon);
        }

        public static void WhiteWon()
        {
            var whiteWon = @"

            █░█░█ █░█ █ ▀█▀ █▀▀   █░█░█ █▀█ █▄░█
            ▀▄▀▄▀ █▀█ █ ░█░ ██▄   ▀▄▀▄▀ █▄█ █░▀█

";
            Console.WriteLine(whiteWon);
        }

        public static void Player1()
        {
            var player1 = @"

            ▒█▀▀█ ▒█░░░ █▀▀█ █░░█ █▀▀ █▀▀█ 　 ▄█░ 
            ▒█▄▄█ ▒█░░░ █▄▄█ █▄▄█ █▀▀ █▄▄▀ 　 ░█░ 
            ▒█░░░ ▒█▄▄█ ▀░░▀ ▄▄▄█ ▀▀▀ ▀░▀▀ 　 ▄█▄
";


            Console.WriteLine(player1);
        }

        public static void Player2()
        {
            var player1 = @"

            ▒█▀▀█ ▒█░░░ █▀▀█ █░░█ █▀▀ █▀▀█ 　 █▀█ 
            ▒█▄▄█ ▒█░░░ █▄▄█ █▄▄█ █▀▀ █▄▄▀ 　 ░▄▀ 
            ▒█░░░ ▒█▄▄█ ▀░░▀ ▄▄▄█ ▀▀▀ ▀░▀▀ 　 █▄▄
";


            Console.WriteLine(player1);
        }
    }
}
