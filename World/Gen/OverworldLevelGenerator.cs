using BasicConsoleGame.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.World.Gen {
    public class OverworldLevelGenerator : LevelGenerator {
        protected OverworldLevelGenerator(long seed) : base(seed) {
            // create value noise
            this.noiseSampler = new ValueNoise(seed);
            this.beachSampler = new ValueNoise(seed + 12);
        }

        public static OverworldLevelGenerator Create(long seed) {
            return new OverworldLevelGenerator(seed);
        }

        protected override void GenerateBase(LevelSection section, int x, int y) {
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
        }

        private const double NoiseScale = 17.0D;
        private readonly ValueNoise noiseSampler, beachSampler;
    }
}
