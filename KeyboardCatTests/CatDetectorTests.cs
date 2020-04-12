using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KeyboardCat;

namespace KeyboardCatTests
{
    [TestClass]
    public class CatDetectorTests
    {
        [TestMethod]
        public void IsCat()
        {
            CatDetector catDetector = new CatDetector(100);
            bool isCat = false;
            for (int i = 0; i < 10; i++) 
            {
                isCat = isCat || catDetector.IsCat(false, 0, new IntPtr(10), new WindowsKeyboard.KBDLLHOOKSTRUCT(1, 1));
            }

            Assert.IsTrue(isCat);
        }
    }
}
