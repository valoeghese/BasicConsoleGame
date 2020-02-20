using BasicConsoleGame.Player.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.Player {
    public class Inventory {
        
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
    }
}
