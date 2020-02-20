using BasicConsoleGame.Player;
using BasicConsoleGame.Render.Screen;
using BasicConsoleGame.World;
using BasicConsoleGame.World.Gen;
using System;

namespace BasicConsoleGame {
    class Program {
        static void Main(string[] args) {
            Random random = new Random();
            level = new Level(random.Next(), OverworldLevelGenerator.Create);
            MainPlayer player = new MainPlayer(level);
            levelScreen = new LevelScreen(player);
            screen = levelScreen;

            Render();

            do {
                ConsoleKey key = Console.KeyAvailable ? Console.ReadKey().Key : ConsoleKey.NoName;

                if (key == ConsoleKey.Escape) {
                    goto finale;
                } else if (readCountdown == 0) {
                    if (key != ConsoleKey.NoName) {
                        readCountdown = 2;

                        switch (key) {
                            case ConsoleKey.W:
                                player.Move(0, -1);
                                Render();
                                break;
                            case ConsoleKey.A:
                                player.Move(-1, 0);
                                Render();
                                break;
                            case ConsoleKey.D:
                                player.Move(1, 0);
                                Render();
                                break;
                            case ConsoleKey.S:
                                player.Move(0, 1);
                                Render();
                                break;
                            case ConsoleKey.Tab:
                                levelScreen.FlipDebugInfoDisplay();
                                Render();
                                break;
                            case ConsoleKey.I:
                                break;
                            default:
                                break;
                        }
                    }
                } else {
                    PrepareCursorForInput();
                    readCountdown--;
                }
            } while (true);

        finale:
            return;
        }

        private static void Render() {
            screen.Render();
        }

        internal static void PrepareCursorForInput() {
            Console.SetCursorPosition(19 * 2 + 1, 19);
        }

        private static int readCountdown = 2;
        private static Level level;
        private static LevelScreen levelScreen;
        internal static IScreen screen;
    }
}
