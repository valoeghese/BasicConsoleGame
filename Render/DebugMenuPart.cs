using System;
using System.Collections.Generic;
using System.Text;

namespace BasicConsoleGame.Render {
    public class DebugMenuPart {
        private DebugMenuPart(Func<string> textSupplier) {
            this.text = textSupplier;
        }

        public string GetText() {
            return this.text.Invoke();
        }

        private readonly Func<string> text;

        public static DebugMenuPart of(Func<string> textSupplier) {
            return new DebugMenuPart(textSupplier);
        }

        public static DebugMenuPart[] create(Func<string>[] textSuppliers) {
            DebugMenuPart[] result = new DebugMenuPart[textSuppliers.Length];
            
            for (int i = 0; i < textSuppliers.Length; ++i) {
                result[i] = of(textSuppliers[i]);
            }

            return result;
        }
    }
}
