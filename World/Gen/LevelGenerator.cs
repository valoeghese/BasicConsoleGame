using BasicConsoleGame.Util;
using System.Collections.Generic;

namespace BasicConsoleGame.World.Gen {
    public class LevelGenerator {
        public LevelGenerator(long seed) {
            this.seed = seed;
            // create value noise
            this.noiseSampler = new ValueNoise(seed);
            this.beachSampler = new ValueNoise(seed + 12);
        }

        public LevelGenerator AddTerrainModifier(TerrainModifier modifier) {
            modifier.Initialise(this.seed);
            this.terrainModifiers.Add(modifier);
            return this;
        }

        public LevelSection Create(int sectionX, int sectionY) {
            LevelSection section = new LevelSection(sectionX, sectionY);
            int x = sectionX << 4;
            int y = sectionY << 4;
            // loop over x and y values in the level section
            for (int localX = 0; localX < 16; ++localX) {
                double noiseX = (x + localX) / NoiseScale;

                for (int localY = 0; localY < 16; ++localY) {
                    double noiseY = (y + localY) / NoiseScale;
                    double noise = noiseSampler.Sample(noiseX, noiseY);

                    // default: water
                    Tile toSet = Tile.WATER;

                    if (noise > 0.0D) { // add beaches and land from noise
                        double beachNoise = beachSampler.Sample(noiseX, noiseY) / 6.0D;
                        double rockBeachNoise = noiseSampler.Sample(noiseX + 2, noiseY + 2) / 7.0D;
                        toSet = noise < (0.0D + rockBeachNoise) ? Tile.STONE : Tile.GRASS;
                        toSet = noise < (0.0D + beachNoise) ? Tile.SAND : toSet;
                    }
                    section.SetTile(localX, localY, toSet);
                }
            }

            // modify terrain with terrain modifiers
            foreach (TerrainModifier modifier in terrainModifiers) {
                modifier.Start(sectionX, sectionY, section);
            }

            return section;
        }

        public readonly long seed;
        private readonly ValueNoise noiseSampler, beachSampler;
        private readonly IList<TerrainModifier> terrainModifiers = new List<TerrainModifier>();

        private const double NoiseScale = 17.0D;
    }
}
