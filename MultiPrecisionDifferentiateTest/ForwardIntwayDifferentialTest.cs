using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPresicionDifferentiate;

namespace MultiPrecisionDifferentiateTest {
    [TestClass]
    public class ForwardIntwayDifferentialTest {
        [TestMethod]
        public void DifferentiateExpTest() {
            for (int derivative = 0; derivative <= 64; derivative++) {
                MultiPrecision<Pow2.N8> y = ForwardIntwayDifferential<Pow2.N8>.Differentiate(MultiPrecision<Pow2.N8>.Exp, 0, derivative, 0.125);

                Console.WriteLine($"{derivative}\t{y}");
            }
        }

        [TestMethod]
        public void DifferentiatePolyTest() {
            for (int derivative = 0; derivative <= 64; derivative++) {
                MultiPrecision<Pow2.N8> y = ForwardIntwayDifferential<Pow2.N8>.Differentiate((x) => 1 + x + x * x + x * x * x, 0, derivative, 0.125);

                Console.WriteLine($"{derivative}\t{y}");
            }
        }
    }
}
