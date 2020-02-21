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
            inventoryScreen = new InventoryScreen(player.inventory);
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
                                if (screen is LevelScreen) {
                                    player.Move(0, -1);
                                }
                                Render();
                                break;
                            case ConsoleKey.A:
                                if (screen is LevelScreen) {
                                    player.Move(-1, 0);
                                }
                                Render();
                                break;
                            case ConsoleKey.D:
                                if (screen is LevelScreen) {
                                    player.Move(1, 0);
                                }
                                Render();
                                break;
                            case ConsoleKey.S:
                                if (screen is LevelScreen) {
                                    player.Move(0, 1);
                                }
                                Render();
                                break;
                            case ConsoleKey.Tab:
                                levelScreen.FlipDebugInfoDisplay();
                                Render();
                                break;
                            case ConsoleKey.I:
                                if (screen is LevelScreen) {
                                    SwitchScreen(inventoryScreen);
                                } else if (screen is InventoryScreen) {
                                    SwitchScreen(levelScreen);
                                }
                                break;
                            case ConsoleKey.E:
                                if (screen is LevelScreen) {
                                    player.Harvest();
                                }
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

        private static void SwitchScreen(IScreen newScreen) {
            screen.ClearScreen();
            screen = newScreen;
            screen.Render();
        }

        private static int readCountdown = 2;
        private static Level level;
        private static LevelScreen levelScreen;
        private static InventoryScreen inventoryScreen;
        internal static IScreen screen;
    }
}
