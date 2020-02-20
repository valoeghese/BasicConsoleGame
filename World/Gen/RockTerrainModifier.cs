using BasicConsoleGame.Util;
using System;

namespace BasicConsoleGame.World.Gen {
    public class RockTerrainModifier : TerrainModifier {
        public RockTerrainModifier() : base(0) {
        }

        protected override void ModifyTerrain(int sectionX, int sectionY, LevelSection section) {
            if (this.random.Random(6) == 0) {
                for (int x = 0; x < 16; ++x) {
                    for (int y = 0; y < 16; ++y) {
                        if (section.GetTile(x, y) == Tile.GRASS) {
                            if (this.random.Random(14) == 0) {
                                section.SetTile(x, y, Tile.ROCK);
                            }
                        }
                    }
                }
            }
        }
    }
}
