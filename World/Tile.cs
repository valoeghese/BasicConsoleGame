using BasicConsoleGame.Player;
using BasicConsoleGame.Player.Item;
using BasicConsoleGame.Render;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.World {
    public enum Tile : byte {
        WATER = 0,
        GRASS = 1,
        STONE = 2,
        SAND = 3,
        ROCK = 33,
        SHRUB = 34
    }

    public sealed class TileUtils {
        public static bool IsSolid(Tile tile) {
            return (byte) tile > 32;
        }

        public static bool CanPlayerWalkOn(Tile tile) {
            return !IsSolid(tile) && (tile != Tile.WATER);
        }
        public static void RenderTile(int x, int y, Tile tile, Camera camera) {
            char c = '?'; // character to render

            if (x == 10 && y == 10) {
                c = 'O';
                camera.SetColour(ConsoleColor.Red);
            } else {
                switch (tile) {
                    case Tile.WATER:
                        c = '≈';
                        camera.SetColour(ConsoleColor.Blue);
                        break;
                    case Tile.GRASS:
                        c = '#';
                        camera.SetColour(ConsoleColor.Green);
                        break;
                    case Tile.STONE:
                        c = '#';
                        camera.SetColour(ConsoleColor.Gray);
                        break;
                    case Tile.SAND:
                        c = '#';
                        camera.SetColour(ConsoleColor.Yellow);
                        break;
                    case Tile.ROCK:
                        c = '0';
                        camera.SetColour(ConsoleColor.DarkGray);
                        break;
                    case Tile.SHRUB:
                        c = '@';
                        camera.SetColour(ConsoleColor.DarkYellow);
                        break;
                }
            }

            camera.Draw(x, y, c);
        }

        public static ItemEntry GetHarvestItem(Tile tile) {
            switch (tile) {
                case Tile.SHRUB:
                    return new ItemEntry(ItemType.Boat, 1);
                default:
                    return null;
            }
        }
    }
}
