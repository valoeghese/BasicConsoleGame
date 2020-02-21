using BasicConsoleGame.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.Player.Item {
    class BoatItem : ItemType { // doesn't need to be public since it's only refrenced in Item
        public BoatItem(byte id) : base(id, 1, "Boat") {
        }

        public override bool Use(ItemEntry entry, MainPlayer player, int x, int y) {
            if (player.GetLevel().GetTile(x, y) == Tile.WATER) {
                // todo spawn boat
                return true;
            }

            return false;
        }
    }
}
