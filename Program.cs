using BasicConsoleGame.level;
using System;

namespace BasicConsoleGame {
    class Program {
        static void Main(string[] args) {
            Random random = new Random();
            Level level = new Level(random.Next());
            
            for (int x = 0; x < 19; ++x) {
                for (int y = 0; y < 19; ++y) {
                    RenderTile(x, y, level.GetTile(x, y));
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
