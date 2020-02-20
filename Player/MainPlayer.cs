using BasicConsoleGame.Render;
using BasicConsoleGame.Util;
using BasicConsoleGame.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.Player {
    public class MainPlayer {
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
