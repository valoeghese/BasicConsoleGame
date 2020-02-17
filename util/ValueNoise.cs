using System;

namespace BasicConsoleGame.util {
	// Ported from my Java version of this
	public class ValueNoise {
		public ValueNoise(long seed) {
			this.rand = new SimpleRandom(seed);
			this.rand.Init(0, 0);
			this.offsetX = rand.RandomDouble();
			this.offsetY = rand.RandomDouble();
			this.offsetZ = rand.RandomDouble();
		}

		private double KeyPoint(double x, double y) {
			return (2 * rand.RandomDouble(x, y)) - 1;
		}

		private double KeyPoint(double x, double y, double z) {
			return (2 * rand.RandomDouble(x, y, z)) - 1;
		}

		private double SampleImpl(double x, double y) {
			double xFloor = Math.Floor(x);
			double yFloor = Math.Floor(y);

			double localX = x - xFloor;
			double localY = y - yFloor;

			localX = Fade(localX);
			localY = Fade(localY);

			double NW = KeyPoint(xFloor, yFloor + 1);
			double NE = KeyPoint(xFloor + 1, yFloor + 1);
			double SW = KeyPoint(xFloor, yFloor);
			double SE = KeyPoint(xFloor + 1, yFloor);

			return Lerp(localY,
					Lerp(localX, SW, SE),
					Lerp(localX, NW, NE));
		}

		private double SampleImpl(double x, double y, double z) {
			double xFloor = Math.Floor(x);
			double yFloor = Math.Floor(y);
			double zFloor = Math.Floor(z);

			double localX = x - xFloor;
			double localY = y - yFloor;
			double localZ = z - zFloor;

			localX = Fade(localX);
			localY = Fade(localY);
			localZ = Fade(localZ);

			double NWLow = KeyPoint(xFloor, yFloor + 1, zFloor);
			double NELow = KeyPoint(xFloor + 1, yFloor + 1, zFloor);
			double SWLow = KeyPoint(xFloor, yFloor, zFloor);
			double SELow = KeyPoint(xFloor + 1, yFloor, zFloor);

			double NWHigh = KeyPoint(xFloor, yFloor + 1, zFloor + 1);
			double NEHigh = KeyPoint(xFloor + 1, yFloor + 1, zFloor + 1);
			double SWHigh = KeyPoint(xFloor, yFloor, zFloor + 1);
			double SEHigh = KeyPoint(xFloor + 1, yFloor, zFloor + 1);

			return Lerp(localZ,
					Lerp(localY,
							Lerp(localX, SWLow, SELow),
							Lerp(localX, NWLow, NELow)),
					Lerp(localY,
							Lerp(localX, SWHigh, SEHigh),
							Lerp(localX, NWHigh, NEHigh)));
		}

		private readonly SimpleRandom rand;
		private readonly double offsetX, offsetY, offsetZ;

		public double Sample(double x, double y) {
			return SampleImpl(x + this.offsetX, y + this.offsetY);
		}

		public double Sample(double x, double y, double z) {
			return SampleImpl(x + this.offsetX, y + this.offsetY, z + this.offsetZ);
		}

		private static double Fade(double n) {
			return n * n * n * (n * (n * 6 - 15) + 10);
		}

		private static double Lerp(double progress, double v0, double v1) {
			return v0 + (progress * (v1 - v0));
		}
	}
}
