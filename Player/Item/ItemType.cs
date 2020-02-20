namespace BasicConsoleGame.Player.Item {
    public class ItemType {
        public ItemType(byte id, byte maxCount) {
            this.id = id;
            this.maxCount = maxCount;
        }

        public virtual bool Use(ItemEntry entry, MainPlayer player, int x, int y) {
            return false;
        }

        public readonly byte id;
        public readonly byte maxCount;

        public static readonly ItemType Boat = new BoatItem(0);
    }
}
