using BasicConsoleGame.Player.Item;
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
            this.inventory = new Inventory(10);
        }

        public void Move(int x, int y) {
            Tile tile = this.level.GetTile(this.x + x, this.y + y);

            if (TileUtils.CanPlayerWalkOn(this.InBoat(), tile)) {
                this.x += x;
                this.y += y;
                this.camera.ReLocate(this.x, this.y);

                this.blockSlowness = tile == Tile.SHALLOW_WATER ? 3 : 2;
            }

            if (x == 0) {
                this.facing = y == 1 ? Facing.NORTH : Facing.SOUTH;
            } else {
                this.facing = x == 1 ? Facing.EAST : Facing.WEST;
            }
        }

        public Vec2 GetFacingPosition() {
            switch (this.facing) {
                case Facing.NORTH:
                    return new Vec2(x, y + 1);
                case Facing.EAST:
                    return new Vec2(x + 1, y);
                case Facing.SOUTH:
                    return new Vec2(x, y - 1);
                case Facing.WEST:
                default:
                    return new Vec2(x - 1, y);
            }
        }

        public bool Harvest() {
            int availableSlot = this.inventory.GetNextAvailableSlot();
           
            if (availableSlot == -1) {
                return false;
            }

            Vec2 pos = this.GetFacingPosition();
            Tile t = this.level.GetTile(pos.x, pos.y);

            if (TileUtils.IsSolid(t)) {
                ItemEntry harvestItem = TileUtils.GetHarvestItem(t);

                if (harvestItem != null) {
                    Tile replacement = this.level.GetTile(pos.x, pos.y + 1);
                    
                    if (TileUtils.IsSolid(replacement)) {
                        replacement = this.level.GetTile(pos.x + 1, pos.y);

                        if (TileUtils.IsSolid(replacement)) {
                            replacement = this.level.GetTile(pos.x, pos.y - 1);

                            if (TileUtils.IsSolid(replacement)) {
                                replacement = this.level.GetTile(pos.x - 1, pos.y);

                                if (TileUtils.IsSolid(replacement)) {
                                    replacement = Tile.STONE;
                                }
                            }
                        }
                    }

                    this.inventory.SetItemInSlot(availableSlot, harvestItem);
                    this.level.SetTile(pos.x, pos.y, replacement);
                    
                    return true;
                }
            }

            return false;
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

        public Facing GetFacing() {
            return this.facing;
        }

        public bool InBoat() {
            return this.inventory[0].GetItem() == ItemType.Boat;
        }

        private Level level;

        private readonly Camera camera;
        public readonly Inventory inventory;
        private int x;
        private int y;
        private Facing facing;
        internal int blockSlowness = 2;
    }
}
