using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectToTest;

namespace ProjectToTest_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodSomme()
        {
            int x = 5;
            int y = 7;
            int resultat = 12;

            var c = new Calculs();

            Assert.AreEqual(resultat, c.Somme(x, y));
        }
    }
}
