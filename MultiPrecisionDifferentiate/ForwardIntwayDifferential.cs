using MultiPrecision;
using MultiPrecisionDifferentiate;
using System.Collections.ObjectModel;

namespace MultiPresicionDifferentiate {
    public static class ForwardIntwayDifferential<N> where N : struct, IConstant {
        public static MultiPrecision<N> Differentiate(Func<MultiPrecision<N>, MultiPrecision<N>> f, MultiPrecision<N> x, int derivative, MultiPrecision<N> h) {
            if (derivative < 0 || derivative > ForwardIntwayPoints<N>.MaxDerivative) {
                throw new ArgumentOutOfRangeException(nameof(derivative));
            }

            if (derivative == 0) {
                return f(x);
            }

            ReadOnlyCollection<MultiPrecision<N>> ws = ForwardIntwayPoints<N>.Table[derivative];

            MultiPrecision<N> s = MultiPrecision<N>.Zero;

            for (int i = 0; i < ws.Count; i++) {
                MultiPrecision<N> w = ws[i];

                s += w * f(x + i * h);
            }

            s /= MultiPrecision<N>.Pow(h, derivative);

            return s;
        }
    }
}
