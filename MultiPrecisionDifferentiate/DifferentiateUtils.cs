using MultiPrecision;

namespace MultiPrecisionDifferentiate {
    public static class DifferentiateUtils {
        public static (MultiPrecision<N> h, MultiPrecision<N> value, long actual_bits) EstimatePrecision<N>(
            IEnumerable<(MultiPrecision<N> h, MultiPrecision<N> value)> values) where N : struct, IConstant {

            if (values.Count() <= 1 || values.Select(v => v.h).Distinct().Count() != values.Count()) {
                throw new ArgumentException("array require multiple and unique 'h'.", nameof(values));
            }

            static long match_bits2(MultiPrecision<N> v1, MultiPrecision<N> v2) {
                if (!MultiPrecision<N>.IsFinite(v1) || !MultiPrecision<N>.IsFinite(v2)) {
                    return 0;
                }

                MultiPrecision<N> dv = v1 - v2;

                long bits = long.Clamp(long.Max(v1.Exponent, v2.Exponent) - dv.Exponent, 0, MultiPrecision<N>.Bits);

                return bits;
            }

            static long match_bits3(MultiPrecision<N> v1, MultiPrecision<N> v2, MultiPrecision<N> v3) {
                return long.Min(match_bits2(v1, v2), match_bits2(v2, v3));
            }

            List<(MultiPrecision<N> h, MultiPrecision<N> value)> values_sorted = [.. values.OrderByDescending(v => v.h)];

            if (values.Count() == 2) {
                return (
                    values_sorted[1].h,
                    values_sorted[1].value,
                    match_bits2(values_sorted[0].value, values_sorted[1].value)
                );
            }

            MultiPrecision<N> best_h = MultiPrecision<N>.NaN, best_value = MultiPrecision<N>.NaN;
            long max_match_bits = 0;

            for (int i = 1; i < values_sorted.Count - 1; i++) {
                long match_bits = match_bits3(values_sorted[i - 1].value, values_sorted[i].value, values_sorted[i + 1].value);

                if (match_bits > max_match_bits) {
                    (best_h, best_value) = values_sorted[i];
                    max_match_bits = match_bits;
                }
            }

            return (best_h, best_value, max_match_bits);
        }
    }
}
