using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.Render.Screen {
    internal class LevelScreen : IScreen {
        public LevelScreen(ILevelSource levelSource) {
            debugMenu = DebugMenuPart.create(new Func<string>[] {
                () => "XY:         " + levelSource.GetX().ToString() + ", " + levelSource.GetY().ToString(),
                () => "Section XY: " + (levelSource.GetX() >> 4).ToString() + "," + (levelSource.GetY() >> 4).ToString(),
                () => "Seed:       " + levelSource.GetLevel().seed
            });
        }

        public void Render() {
            Camera camera = Camera.GetCurrent();
            camera.RenderLevel();

            if (displayDebugInfo) {
                RenderDebug(camera);
            }
        }

        public void FlipDebugInfoDisplay() {
            this.displayDebugInfo = !this.displayDebugInfo;
            
            if (!this.displayDebugInfo) {
                this.RemoveDebugInfo();
            }
        }

        private void RenderDebug(Camera camera) {
            int baseY = 21;
            camera.SetColour(ConsoleColor.White);

            for (int i = 0; i < this.debugMenu.Length; ++i) {
                camera.TextLine(0, baseY + i, this.debugMenu[i].GetText());
            }

            Program.PrepareCursorForInput();
        }

        private void RemoveDebugInfo() {
            if (Program.screen == this) {
                int baseY = 21;

                for (int i = 0; i < this.debugMenu.Length; ++i) {
                    Console.SetCursorPosition(0, baseY + i);
                    Console.Write("                                        "); // 40 spaces
                }
            }
        }

        private DebugMenuPart []debugMenu; // god tier array declaration placement
        public bool displayDebugInfo = false;
    }
}
