using BasicConsoleGame.util;
using System;

namespace BasicConsoleGame.level.gen {
    public class RockTerrainModifier : ITerrainModifier {
        public void Initialise(long seed) {
            this.random = new SimpleRandom(seed);
        }

        public void ModifyTerrain(int sectionX, int sectionY, LevelSection section) {
            this.random.Init(sectionX, sectionY);

            for (int x = 0; x < 16; ++x) {
                for (int y = 0; y < 16; ++y) {
                    if (section.GetTile(x, y) == Tile.GRASS) {
                        if (this.random.Random(15) == 0) {
                            section.SetTile(x, y, Tile.ROCK);
                        }
                    }
                }
            }
        }

        private SimpleRandom random;
    }
}
