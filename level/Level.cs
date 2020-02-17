using BasicConsoleGame.level.gen;
using System.Collections.Generic;

namespace BasicConsoleGame.level {
    class Level : ITileArea {
        public Level(long seed) {
            this.generator = new LevelGenerator(seed);
            this.generator.AddTerrainModifier(new RockTerrainModifier());
        }

        public Tile GetTile(int x, int y) {
            return this.GetSection(x >> 4, y >> 4).GetTile(x & 0xF, y & 0xF);
        }

        public void SetTile(int x, int y, Tile tile) {
            this.GetSection(x >> 4, y >> 4).SetTile(x & 0xF, y & 0xF, tile);
        }

        public bool IsSolid(int x, int y) {
            return this.GetSection(x >> 4, y >> 4).IsSolid(x & 0xF, y & 0xF);
        }

        private LevelSection GetSection(int x, int y) {
            // long representing x and y coordinate
            long coordinate = ((long) y & 0xFFFFFFFFL) | (((long) x & 0xFFFFFFFFL) << 32);

            if (sections.ContainsKey(coordinate)) {
                return sections[coordinate];
            } else {
                LevelSection section = this.generator.Create(x, y);
                sections.Add(coordinate, section); // add section to the cache

                sCoordCache.Add(coordinate);

                if (sCoordCache.Count > 16) { // check if cache is above capacity
                    long keyToRemove = sCoordCache[0]; // get coord key to remove (first in cache list)

                    sections.Remove(keyToRemove); // remove the specified key
                    sCoordCache.RemoveAt(0); // remove the key from the coord key cache since it is no longer in the dictionary
                }

                return section;
            }
        }

        private readonly IList<long> sCoordCache = new List<long>(); // section coordinate cache
        private readonly Dictionary<long, LevelSection> sections = new Dictionary<long, LevelSection>();
        private readonly LevelGenerator generator;
    }
}
