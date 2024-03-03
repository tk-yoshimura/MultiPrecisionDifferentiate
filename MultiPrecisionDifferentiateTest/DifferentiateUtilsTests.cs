using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionDifferentiate;

namespace MultiPrecisionDifferentiateTest {
    [TestClass()]
    public class DifferentiateUtilsTests {
        [TestMethod()]
        public void EstimatePrecisionTest() {
            List<(MultiPrecision<Pow2.N16> h, MultiPrecision<Pow2.N16> value)> values = [];

            for (int exponent = -32; exponent <= -16; exponent++) {
                MultiPrecision<Pow2.N16> h = MultiPrecision<Pow2.N16>.Ldexp(1, exponent);
                MultiPrecision<Pow2.N16> value = MultiPrecision<Pow2.N16>.PI + MultiPrecision<Pow2.N16>.Square(exponent + 24) * 1e-20;

                values.Add((h, value));

                Console.WriteLine($"{exponent},{value}");
            }

            (MultiPrecision<Pow2.N16> best_h, MultiPrecision<Pow2.N16> best_value, long actual_bits) =
                DifferentiateUtils.EstimatePrecision(values);

            int actual_digits = (int)(actual_bits * 0.30103);

            Console.WriteLine("best");
            Console.WriteLine(best_h.Exponent);
            Console.WriteLine(best_value);
            Console.WriteLine(best_value.ToString($"e{actual_digits}"));
            Console.WriteLine(actual_digits);
            Console.WriteLine(actual_bits);

            Assert.AreEqual(-24, best_h.Exponent);
            Assert.AreEqual(20, actual_digits);
        }
    }
}