using BasicConsoleGame.Util;
using BasicConsoleGame.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.Render {
    public class Camera {
        public Camera(Level level, int initialX, int initialY) {
            this.level = level;
            this.x = initialX;
            this.y = initialY;

            if (currentCamera == null) {
                currentCamera = this;
                this.current = true;
            } else {
                this.current = false;
            }
        }

        public void RenderLevel() {
            for (int x = -10; x < 10; ++x) {
                int tileX = x + this.x;

                for (int y = -10; y < 10; ++y) {
                    TileUtils.RenderTile(x + 10, y + 10, this.level.GetTile(tileX, y + this.y), this);
                }
            }
        }

        public void SetLevel(Level level) {
            this.level = level;
        }

        public void SetCurrent() {
            currentCamera.current = false;
            currentCamera = this;
            this.current = true;
            
            if (this.colour != colourCache) {
                colourCache = this.colour;
                Console.ForegroundColor = this.colour;
            }
        }

        public void SetColour(ConsoleColor colour) {
            this.colour = colour;

            if (this.current) {
                if (colour != colourCache) {
                    colourCache = colour;
                    Console.ForegroundColor = colourCache;
                }
            }
        }

        public void Draw(int x, int y, char chr) {
            if (this.current) {
                Console.SetCursorPosition(x * 2, y);
                Console.Write(chr);
            }
        }

        public void ReLocate(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public void TextLine(int x, int y, string msg) {
            if (this.current) {
                Console.SetCursorPosition(x * 2, y);
                Console.Write("                                        "); // 40 spaces
                Console.SetCursorPosition(x * 2, y);
                Console.Write(msg);
            }
        }

        private Level level;
        private int x;
        private int y;
        private bool current;
        private ConsoleColor colour;

        public static Camera GetCurrent() {
            return currentCamera;
        }

        private static ConsoleColor colourCache;
        private static Camera currentCamera = null;
    }
}
