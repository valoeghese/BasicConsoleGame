using BasicConsoleGame.Util;
using BasicConsoleGame.World.Gen;
using System;
using System.Collections.Generic;

namespace BasicConsoleGame.World {
    public class Level : ITileArea {
        public Level(long seed) {
            this.generator = new LevelGenerator(seed);
            this.generator.AddTerrainModifier(new RockTerrainModifier());
            this.seed = seed;
        }

        internal Vec2 GetPlayerSpawn() {
            // loop over section x, section y
            for (int sx = 0; sx < 5; ++sx) {
                for (int sy = 0; sy < 5; ++sy) {
                    IList<LevelSection> sections = new List<LevelSection> {
                        this.generator.Create(sx, sy)
                    };

                    // inverse sections
                    bool sxInverse = sx != 0;
                    bool syInverse = sy != 0;

                    if (sxInverse) {
                        sections.Add(this.generator.Create(-sx, sy));

                        if (syInverse) {
                            sections.Add(this.generator.Create(-sx, -sy));
                        }
                    } else if (syInverse) {
                        sections.Add(this.generator.Create(sx, -sy));
                    }

                    // loop over sections and inverse sections
                    foreach (LevelSection section in sections) {
                        // loop over local x, local y
                        for (int lx = 0; lx < 16; ++lx) {
                            for (int ly = 0; ly < 16; ++ly) {
                                // check if this is valid player spawn
                                if (TileUtils.CanPlayerWalkOn(section.GetTile(lx, ly))) {
                                    return new Vec2((sx << 4) + lx, (sy << 4) + ly);
                                }
                            }
                        }
                    }
                }
            }

            return new Vec2(0, 0);
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
        public readonly long seed;
    }
}
