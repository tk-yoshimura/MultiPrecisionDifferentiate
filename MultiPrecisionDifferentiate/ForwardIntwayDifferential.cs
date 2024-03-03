﻿using MultiPrecision;
using MultiPrecisionDifferentiate;
using System.Collections.ObjectModel;

namespace MultiPresicionDifferentiate {
    public static class ForwardIntwayDifferential<N> where N : struct, IConstant {
        public static MultiPrecision<N> Differentiate(
            Func<MultiPrecision<N>, MultiPrecision<N>> f, MultiPrecision<N> x, int derivative,
            MultiPrecision<N> h, bool h_scale = true, int taylor_scale = 1) {

            return Differentiate(f, x, new int[] { derivative }, h, h_scale, taylor_scale).First().value;
        }

        public static IEnumerable<(int derivative, MultiPrecision<N> value)> Differentiate(
            Func<MultiPrecision<N>, MultiPrecision<N>> f, MultiPrecision<N> x, IEnumerable<int> derivatives,
            MultiPrecision<N> h, bool h_scale = true, int taylor_scale = 1) {

            if (!(h > 0) || !MultiPrecision<N>.IsFinite(h)) {
                throw new ArgumentOutOfRangeException(nameof(h));
            }

            if (derivatives.Any(derivative => derivative < 0 || derivative > CenteredIntwayPoints<N>.MaxDerivative)) {
                throw new ArgumentOutOfRangeException(nameof(derivatives));
            }

            Dictionary<int, MultiPrecision<N>> fs = [];

            foreach (int derivative in derivatives) {
                if (derivative == 0) {
                    MultiPrecision<N> f0 = Table(f, x, h, fs, 0);

                    yield return (0, f0);
                    continue;
                }

                ReadOnlyCollection<MultiPrecision<N>> ws = ForwardIntwayPoints<N>.Table[derivative];

                MultiPrecision<N> s = MultiPrecision<N>.Zero;

                for (int i = 0; i < ws.Count; i++) {
                    MultiPrecision<N> fi = Table(f, x, h, fs, i);

                    MultiPrecision<N> w = ws[i];

                    s += w * fi;
                }

                if (h_scale) {
                    s /= MultiPrecision<N>.Pow(h, derivative);
                }
                if (taylor_scale != 0) {
                    s *= MultiPrecision<N>.Pow(MultiPrecision<N>.TaylorSequence[derivative], taylor_scale);
                }

                yield return (derivative, s);
            }
        }

        private static MultiPrecision<N> Table(Func<MultiPrecision<N>, MultiPrecision<N>> f, MultiPrecision<N> x, MultiPrecision<N> h, Dictionary<int, MultiPrecision<N>> fs, int i) {
            if (!fs.TryGetValue(i, out MultiPrecision<N>? fi)) {
                fi = f(x + i * h);
                fs.Add(i, fi);
            }

            return fi;
        }
    }
}
