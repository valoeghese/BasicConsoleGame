namespace BasicConsoleGame.Player.Item {
    public class Item {
        public Item(byte id, byte maxStackSize) {
            this.id = id;
            this.maxStackSize = maxStackSize;
        }

        public virtual bool Use(ItemEntry entry, MainPlayer player, int x, int y) {
            return false;
        }

        public readonly byte id;
        public readonly byte maxStackSize;

        public static readonly Item Boat = new BoatItem(0);
    }
}
