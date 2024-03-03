using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPresicionDifferentiate;

namespace MultiPrecisionDifferentiateTest {
    [TestClass]
    public class CenteredIntwayDifferentialTest {
        [TestMethod]
        public void DifferentiateExpTest() {
            for (int derivative = 0; derivative <= 64; derivative++) {
                MultiPrecision<Pow2.N8> y = CenteredIntwayDifferential<Pow2.N8>.Differentiate(
                    MultiPrecision<Pow2.N8>.Exp, 0, derivative, 0.125
                );

                Console.WriteLine($"{derivative}\t{y}");
            }
        }

        [TestMethod]
        public void DifferentiatePolyTest() {
            for (int derivative = 0; derivative <= 64; derivative++) {
                MultiPrecision<Pow2.N8> y = CenteredIntwayDifferential<Pow2.N8>.Differentiate(
                    (x) => 1 + x + x * x + x * x * x, 0, derivative, 0.125
                );

                Console.WriteLine($"{derivative}\t{y}");
            }
        }

        [TestMethod]
        public void DifferentiateExpArrayTest() {
            foreach ((int derivative, MultiPrecision<Pow2.N8> y) in CenteredIntwayDifferential<Pow2.N8>.Differentiate(
                MultiPrecision<Pow2.N8>.Exp, 0, [0, 1, 2, 3], 0.125)) {
                Console.WriteLine($"{derivative}\t{y}");
            }
        }

        [TestMethod]
        public void DifferentiatePolyArrayTest() {
            foreach ((int derivative, MultiPrecision<Pow2.N8> y) in CenteredIntwayDifferential<Pow2.N8>.Differentiate(
                (x) => 1 + x + x * x + x * x * x, 0, [0, 1, 2, 3], 0.125)) {
                Console.WriteLine($"{derivative}\t{y}");
            }
        }
    }
}
