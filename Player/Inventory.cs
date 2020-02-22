using BasicConsoleGame.Player.Item;

namespace BasicConsoleGame.Player {
    public class Inventory {
        public Inventory(int slots) {
            this.slotCount = slots;
            this.slots = new ItemEntry[slots];
            
            for (int i = 0; i < slots; ++i) {
                this.slots[i] = ItemEntry.Empty;
            }
        }

        public int Slots() {
            return this.slotCount;
        }

        public int GetSelectedSlot() {
            return this.selectedSlot;
        }

        public void SetSelectedSlot(int slot) {
            this.selectedSlot = slot;
        }

        public void SetItemInSlot(int slot, ItemEntry entry) {
            this.slots[slot] = entry;
        }
        
        public int GetNextAvailableSlot() {
            for (int i = 0; i < slotCount; ++i) {
                if (slots[i].IsEmpty()) {
                    return i;
                }
            }

            return -1;
        }

        public ItemEntry this[int slot] => slots[slot];

        private readonly int slotCount;
        private readonly ItemEntry[] slots;
        private int selectedSlot = -1;
    }

    public sealed class ItemEntry {
        public ItemEntry(ItemType item, int count) {
            this.item = item;
            this.count = count;
        }

        public bool IsEmpty() {
            return this.item == null || this.count < 1;
        }

        public bool IsFullStack() {
            return this.item == null ? true : this.count >= this.item.maxCount;
        }

        public ItemType GetItem() {
            return this.item;
        }

        public int GetCount() {
            return this.count;
        }

        public void SetCount(int count) {
            if (count > this.item.maxCount) {
                this.count = this.item.maxCount;
            } else {
                this.count = count < 1 ? 0 : count;
            }
        }

        public void AddCount(int countModifier) {
            this.SetCount(this.count + countModifier);
        }

        private int count;
        private readonly ItemType item;

        public static readonly ItemEntry Empty = new ItemEntry(null, 0);
    }
}
