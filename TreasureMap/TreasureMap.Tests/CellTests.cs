using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TreasureMap.Data;

namespace TreasureMap.Tests
{
    [TestFixture]
    public class CellTests
    {
        [Test]
        public void TestDecrementTreasure()
        {
            Cell cell = new Cell { TreasureAmount = 3 };
            cell.DecrementTreasure();
            Assert.AreEqual(2, cell.TreasureAmount);
        }
    }
}
