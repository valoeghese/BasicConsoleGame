namespace BasicConsoleGame.level {
    public interface ITileArea {
        void SetTile(int x, int y, Tile tile);
        Tile GetTile(int x, int y);
        bool IsSolid(int x, int y);
    }
}
