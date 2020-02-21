using BasicConsoleGame.Util;

namespace BasicConsoleGame.World.Gen {
    public class BushTerrainModifier : TerrainModifier {
        public BushTerrainModifier() : base(2L) {
        }

        public override void Initialise(long seed) {
            base.Initialise(seed);
            treeDensityNoise1 = new ValueNoise(seed + 2L);
            treeDensityNoise2 = new ValueNoise(seed + 4L);
        }

        protected override void ModifyTerrain(int sectionX, int sectionY, LevelSection section) {
            double noise1 = 0.75 + this.treeDensityNoise1.Sample(sectionX / noiseScale1, sectionY / noiseScale1);
            double noise2 = 0.75 + this.treeDensityNoise2.Sample(sectionY / noiseScale2, sectionY / noiseScale2);

            int shrubCount = (int)(((noise1 * 0.75) + (noise2 * 0.25)) * 4.0);

            if (shrubCount > 0) {
                for (int i = 0; i < shrubCount; ++i) {
                    int x = this.random.Random(16);
                    int y = this.random.Random(16);
                    
                    if (section.GetTile(x, y) == Tile.GRASS) {
                        section.SetTile(x, y, Tile.SHRUB);
                    }
                }
            }
        }

        private const double noiseScale1 = 16.0D;
        private const double noiseScale2 = 4.0D;

        private ValueNoise treeDensityNoise1;
        private ValueNoise treeDensityNoise2;
    }
}
