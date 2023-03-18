using MultiPrecision;
using MultiPrecisionDifferentiate;
using System.Collections.ObjectModel;

namespace MultiPresicionDifferentiate {
    public static class CenteredIntwayDifferential<N> where N : struct, IConstant {
        public static MultiPrecision<N> Differentiate(Func<MultiPrecision<N>, MultiPrecision<N>> f, MultiPrecision<N> x, int derivative, MultiPrecision<N> h) {
            if (derivative < 0 || derivative > CenteredIntwayPoints<N>.MaxDerivative) {
                throw new ArgumentOutOfRangeException(nameof(derivative));
            }

            if (derivative == 0) {
                return f(x);
            }

            ReadOnlyCollection<MultiPrecision<N>> ws = CenteredIntwayPoints<N>.Table[derivative];

            MultiPrecision<N> s = MultiPrecision<N>.Zero;

            if ((derivative & 1) == 1) {
                for (int i = 1; i < ws.Count; i++) {
                    MultiPrecision<N> w = ws[i];

                    s += w * (f(x + i * h) - f(x - i * h));
                }
            }
            else {
                s += ws[0] * f(x);

                for (int i = 1; i < ws.Count; i++) {
                    MultiPrecision<N> w = ws[i];

                    s += w * (f(x + i * h) + f(x - i * h));
                }
            }

            s /= MultiPrecision<N>.Pow(h, derivative);

            return s;
        }
    }
}
