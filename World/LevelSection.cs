namespace BasicConsoleGame.World {
    public class LevelSection : ITileArea {
        public LevelSection(int x, int y) {
            this.sectionX = x;
            this.sectionY = y;
        }

        public Tile GetTile(int x, int y) {
            return tiles[(x << 4) + y];
        }

        public void SetTile(int x, int y, Tile tile) {
            int position = (x << 4) + y;

            tiles[position] = tile;
            solid[position] = TileUtils.IsSolid(tile);
        }

        public bool IsSolid(int x, int y) {
            return solid[x << 4 + y];
        }

        private readonly Tile[] tiles = new Tile[256];
        private readonly bool[] solid = new bool[256];

        public readonly int sectionX;
        public readonly int sectionY;
    }
}
