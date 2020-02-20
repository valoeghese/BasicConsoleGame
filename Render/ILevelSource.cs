using BasicConsoleGame.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.Render {
    public interface ILevelSource {
        Level GetLevel();
        int GetX();
        int GetY();
    }
}
