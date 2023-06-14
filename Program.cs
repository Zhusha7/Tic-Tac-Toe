using System.Security.Principal;

namespace playground
{

    internal class Program
    {
        static char ToGameChar(int ch)
        {
            if (ch == 1)
                return 'O';
            if (ch == 2)
                return 'X';
            return ' ';
        }

        static int CheckGameState(int[,] arr)
        {
            int diag1 = arr[0, 0] * arr[1, 1] * arr[2, 2];
            int diag2 = arr[0, 0] * arr[1, 1] * arr[2, 2];
            if (diag1 is 1 or 8)
                return diag1 == 1 ? 1 : 2;
            if (diag2 is 1 or 8)
                return diag2 == 1 ? 1 : 2;
            for (int i = 0; i < 3; i++)
            {
                int line1 = 1;
                int line2 = 1;
                for (int j = 0; j < 3; j++)
                {
                    line1 *= arr[i, j];
                    line2 *= arr[j, i];
                }
                if (line1 is 1 or 8)
                    return line1 == 1 ? 1 : 2;
                if (line2 is 1 or 8)
                    return line2 == 1 ? 1 : 2;
            }

            foreach (int it in arr)
                if(it == 0)
                    return -1;
            return 0;
        }

        static void WriteGameTable(int[,] arr, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < 3; i++)
            {
                if (i != 0) Console.WriteLine("\n---+---+---");
                for (int j = 0; j < 3; j++)
                {
                    if (j != 0) Console.Write('|');
                    Console.ForegroundColor = arr[i, j] == 1 ? ConsoleColor.Blue : ConsoleColor.Red;
                    Console.Write($" {ToGameChar(arr[i, j])} ");
                    Console.ForegroundColor = color;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Main(string[] args)
        {
            int turn = 0;
            int endState;
            int[,] gameTableInts = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }};
            do
            {
                turn = turn == 1 ? 2 : 1;
                ConsoleColor color = turn == 1 ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed;
                WriteGameTable(gameTableInts, color);

                while (true)
                {
                    Console.Write($"\nDabar {ToGameChar(turn)} eile! Iveskite koordinate, tarpu nebutina. (pvz: \"2 3\"):\n");
                    string? input = Console.ReadLine()?.Replace(" ","");
                    if (input is { Length: 2 })
                    {
                        int x = (int)Char.GetNumericValue(input[0]) - 1;
                        int y = (int)Char.GetNumericValue(input[1]) - 1;
                        if (x is >= 0 and < 3 && y is >= 0 and < 3)
                        {
                            if (gameTableInts[x, y] != 0)
                            {
                                Console.WriteLine("OOPS! Sis langelis jau uzimtas, pabandykite dar karta.");
                                continue;
                            }
                            gameTableInts[x, y] = turn;
                            break;
                        }
                    }
                    Console.WriteLine("OOPS! Neteisingas ivedimas, pabandykite dar karta.");
                }

                endState = CheckGameState(gameTableInts);
            } while (endState == -1);
            Console.ForegroundColor = ConsoleColor.Green;
            if (endState == 0)
                Console.WriteLine("Zaidimas pasibaige! Niekas nelaimejo");
            Console.WriteLine($"\nZaidimas pasibaige! Laimejo {ToGameChar(turn)}.");
            WriteGameTable(gameTableInts,ConsoleColor.DarkGreen);
        }
    }
}