using BasicConsoleGame.Util;
using System.Collections.Generic;

namespace BasicConsoleGame.World.Gen {
    public abstract class LevelGenerator {
        protected LevelGenerator(long seed) {
            this.seed = seed;
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

            GenerateBase(section, x, y);

            // modify terrain with terrain modifiers
            foreach (TerrainModifier modifier in terrainModifiers) {
                modifier.Start(sectionX, sectionY, section);
            }

            return section;
        }

        protected abstract void GenerateBase(LevelSection section, int x, int y);

        public readonly long seed;
        private readonly IList<TerrainModifier> terrainModifiers = new List<TerrainModifier>();
    }
}
