using BasicConsoleGame.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.World.Gen {
    public abstract class TerrainModifier {
        protected TerrainModifier(long salt) {
            this.salt = salt;
        }
        public void Initialise(long seed) {
            this.random = new SimpleRandom(seed + salt);
        }
        public void Start(int sectionX, int sectionY, LevelSection section) {
            this.random.Init(sectionX, sectionY);
            ModifyTerrain(sectionX, sectionY, section);
        }

        protected abstract void ModifyTerrain(int sectionX, int sectionY, LevelSection section);

        private readonly long salt;
        protected SimpleRandom random;
    }
}
