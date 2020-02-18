using BasicConsoleGame.level;
using System;

namespace BasicConsoleGame {
    class Program {
        static void Main(string[] args) {
            Random random = new Random();
            Level level = new Level(random.Next());

            RenderScreen(level);

            do {
                ConsoleKey key = Console.KeyAvailable ? Console.ReadKey().Key : ConsoleKey.NoName;

                if (key == ConsoleKey.Escape) {
                    goto finale;
                } else if (readCountdown == 0) {
                    readCountdown = 2;

                    switch (key) {
                        case ConsoleKey.W:
                            yOffset -= 1;
                            RenderScreen(level);
                            break;
                        case ConsoleKey.A:
                            xOffset -= 1;
                            RenderScreen(level);
                            break;
                        case ConsoleKey.D:
                            xOffset += 1;
                            RenderScreen(level);
                            break;
                        case ConsoleKey.S:
                            yOffset += 1;
                            RenderScreen(level);
                            break;
                        default:
                            break;
                    }
                } else {
                    Console.SetCursorPosition(18 * 2 + 1, 18);
                    readCountdown--;
                }
            } while (true);

        finale:
            return;
        }

        private static void RenderScreen(Level level) {
            for (int x = 0; x < 19; ++x) {
                int tileX = x + xOffset;

                for (int y = 0; y < 19; ++y) {
                    RenderTile(x, y, level.GetTile(tileX, y + yOffset));
                }
            }
        }

        private static void RenderTile(int x, int y, Tile tile) {
            char c = '?'; // character to render
            switch (tile) {
                case Tile.WATER:
                    c = '≈';
                    SetColor(ConsoleColor.Blue);
                    break;
                case Tile.GRASS:
                    c = '#';
                    SetColor(ConsoleColor.Green);
                    break;
                case Tile.ROCK:
                    c = '0';
                    SetColor(ConsoleColor.Gray);
                    break;
                case Tile.SAND:
                    c = '#';
                    SetColor(ConsoleColor.Yellow);
                    break;
            }

            Draw(x, y, c);
        }

        private static int xOffset = 0;
        private static int yOffset = 0;
        private static int readCountdown = 2;

        private static void SetColor(ConsoleColor colour) {
            if (colour != colourCache) {
                colourCache = colour;
                Console.ForegroundColor = colourCache;
            }
        }

        private static void Draw(int x, int y, char chr) {
            Console.SetCursorPosition(x * 2, y);
            Console.Write(chr);
        }

        private static ConsoleColor colourCache;
    }
}
