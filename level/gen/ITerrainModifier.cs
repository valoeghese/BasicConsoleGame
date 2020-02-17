using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.level.gen {
    public interface ITerrainModifier {
        void Initialise(long seed);
        void ModifyTerrain(int sectionX, int sectionY, LevelSection section);
    }
}
