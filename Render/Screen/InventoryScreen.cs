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
            int selectedSlot = this.inventory.GetSelectedSlot();

            for (int i = 0; i < inventory.Slots(); ++i) {
                int y = 2 + (2 * i);
                ItemEntry entry = this.inventory[i];
                
                if (!entry.IsEmpty()) {
                    if (i == selectedSlot) {
                        Console.SetCursorPosition(0, y);
                        Console.Write(">");
                        Console.SetCursorPosition(39, y);
                        Console.Write("<");
                    } else {
                        Console.SetCursorPosition(39, y);
                        Console.Write(" ");
                        Console.SetCursorPosition(0, y);
                        Console.Write(" ");
                    }

                    Console.Write(entry.GetItem().name);
                    Console.SetCursorPosition(35, y);
                    Console.Write(entry.GetCount());
                }
            }

            Console.ForegroundColor = Camera.colourCache;
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
