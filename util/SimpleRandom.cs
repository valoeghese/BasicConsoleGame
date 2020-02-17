using System;

namespace BasicConsoleGame.util {
    public class SimpleRandom {
		private readonly long seed;
		private long localSeed;

		public SimpleRandom(long seed) {
			this.seed = seed;
			this.localSeed = seed;
		}

		public int Random(double x, double y, int bound) {
			Init(x, y);

			int result = (int)((this.localSeed >> 24) % bound);
			if (result < 0) {
				result += (bound - 1);
			}

			return result;
		}

		public int Random(double x, double y, double z, int bound) {
			Init(x, y, z);

			int result = (int)((this.localSeed >> 24) % bound);
			if (result < 0) {
				result += (bound - 1);
			}

			return result;
		}

		public double RandomDouble(double x, double y) {
			return (double)this.Random(x, y, Int32.MaxValue) / Int32.MaxValue;
		}

		public double RandomDouble(double x, double y, double z) {
			return (double)this.Random(x, y, z, Int32.MaxValue) / Int32.MaxValue;
		}

		public int Random(int bound) {
			int result = (int)((localSeed >> 24) % bound);
			if (result < 0) {
				result += (bound - 1);
			}

			this.localSeed += this.seed;
			this.localSeed *= 3412375462423L * this.localSeed + 834672456235L;

			return result;
		}

		public double RandomDouble() {
			return (double)this.Random(Int32.MaxValue) / (double)Int32.MaxValue;
		}

		public void Init(double x, double y) {
			this.localSeed = this.seed;
			this.localSeed += (long) (x * 72624D);
			this.localSeed *= 3412375462423L * this.localSeed + 834672456235L;
			this.localSeed += (long) (y * 8963D);
			this.localSeed *= 3412375462423L * this.localSeed + 834672456235L;
		}

		public void Init(double x, double y, double z) {
			this.localSeed = this.seed;
			this.localSeed += (long) (x * 72624D);
			this.localSeed *= 3412375462423L * this.localSeed + 834672456235L;
			this.localSeed += (long) (y * 8963D);
			this.localSeed *= 3412375462423L * this.localSeed + 834672456235L;
			this.localSeed += (long) (z * 56385D);
			this.localSeed *= 3412375462423L * this.localSeed + 834672456235L;
		}
	}
}
