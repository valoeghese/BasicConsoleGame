namespace BasicConsoleGame.Player.Item {
    public class ItemType {
        public ItemType(byte id, byte maxCount, string name) {
            this.id = id;
            this.maxCount = maxCount;
            this.name = name;
        }

        public virtual bool Use(ItemEntry entry, MainPlayer player, int x, int y) {
            return false;
        }

        public readonly byte id;
        public readonly byte maxCount;
        public readonly string name;

        public static readonly ItemType Boat = new BoatItem(0);
        public static readonly ItemType Wood = new ItemType(1, 128, "wood");
    }
}
