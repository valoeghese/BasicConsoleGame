using BasicConsoleGame.Player;
using BasicConsoleGame.Render;
using BasicConsoleGame.Util;
using BasicConsoleGame.World;
using System;

namespace BasicConsoleGame {
    class Program {
        static void Main(string[] args) {
            Random random = new Random();
            level = new Level(random.Next());
            MainPlayer player = new MainPlayer(level);

            debugMenu = DebugMenuPart.create(new Func<string>[] {
                () => "XY:         " + player.GetX().ToString() + ", " + player.GetY().ToString(),
                () => "Section XY: " + (player.GetX() >> 4).ToString() + "," + (player.GetY() >> 4).ToString(),
                () => "Seed:       " + level.seed
            });

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
                                if (!tabDownPrev) {
                                    displayDebugInfo = !displayDebugInfo;

                                    if (!displayDebugInfo) {
                                        RemoveDebugInfo();
                                    }
                                    Render();
                                    tabDownPrev = true;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    if (key != ConsoleKey.Tab) {
                        tabDownPrev = false;
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
            Camera camera = Camera.GetCurrent();
            camera.RenderLevel();

            if (displayDebugInfo) {
                RenderDebug(camera);
            }
        }

        private static void RenderDebug(Camera camera) {
            int baseY = 21;
            camera.SetColour(ConsoleColor.White);

            for (int i = 0; i < debugMenu.Length; ++i) {
                camera.TextLine(0, baseY + i, debugMenu[i].GetText());
            }

            PrepareCursorForInput();
        }

        private static void RemoveDebugInfo() {
            int baseY = 21;
            
            for (int i = 0; i < debugMenu.Length; ++i) {
                Console.SetCursorPosition(0, baseY + i);
                Console.Write("                                        "); // 40 spaces
            }
        }

        private static int readCountdown = 2;
        private static DebugMenuPart []debugMenu; // god tier array declaration placement
        private static bool displayDebugInfo = false;
        private static bool tabDownPrev = false;

        private static void PrepareCursorForInput() {
            Console.SetCursorPosition(19 * 2 + 1, 19);
        }

        private static Level level;
    }
}
