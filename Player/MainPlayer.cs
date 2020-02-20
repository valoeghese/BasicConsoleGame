using BasicConsoleGame.Render;
using BasicConsoleGame.Util;
using BasicConsoleGame.World;

namespace BasicConsoleGame.Player {
    public class MainPlayer : ILevelSource {
        public MainPlayer(Level level) {
            Vec2 spawnPos = level.GetPlayerSpawn();
            this.level = level;
            this.x = spawnPos.x;
            this.y = spawnPos.y;
            this.camera = new Camera(level, x, y);
        }

        public void Move(int x, int y) {
            if (TileUtils.CanPlayerWalkOn(this.level.GetTile(this.x + x, this.y + y))) {
                this.x += x;
                this.y += y;
                this.camera.ReLocate(this.x, this.y);
            }
        }

        public void SetLevel(Level level) {
            this.level = level;
            this.camera.SetLevel(level);
        }

        public Level GetLevel() {
            return this.level;
        }

        public void Teleport(Level level, int x, int y) {
            SetLevel(level);
            Teleport(x, y);
        }

        public void Teleport(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public int GetX() {
            return this.x;
        }

        public int GetY() {
            return this.y;
        }

        private Level level;

        private readonly Camera camera;
        private int x;
        private int y;
    }
}
