using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geeks.SqlUtils.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var guidBytes = new byte[16];

            var longBytes = BitConverter.GetBytes(long.MinValue);

            longBytes.CopyTo(guidBytes, 8);

            var guid = new Guid(guidBytes);
        }
    }
}
