using BasicConsoleGame.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.Render.Screen {
    public class InventoryScreen : IScreen {
        public InventoryScreen(Inventory inventory) {
            this.inventory = inventory;
        }

        public void Render() {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(" Inventory");
            Console.WriteLine("========================================");
            for (int i = 0; i < inventory.Slots(); ++i) {
                int y = 2 + (2 * i);
                ItemEntry entry = this.inventory[i];
                
                if (!entry.IsEmpty()) {
                    Console.SetCursorPosition(0, y);
                    Console.Write(entry.GetItem());
                    Console.SetCursorPosition(35, y);
                    Console.Write(entry.GetCount());
                }
            }
        }

        public void ClearScreen() {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < inventory.Slots() * 2; ++i) {
                Console.WriteLine("                                        "); // 40 spaces
            }
        }

        private readonly Inventory inventory;
    }
}
