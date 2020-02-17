using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.level {
    public enum Tile : byte {
        WATER = 0,
        GRASS = 1,
        ROCK = 2,
        SAND = 3
    }

    public sealed class TileUtils {
        public static bool IsSolid(Tile tile) {
            return (byte) tile > 32;
        }
    }
}
