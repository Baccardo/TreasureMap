using NUnit.Framework;
using System.Collections.Generic;
using TreasureMap.Business;
using TreasureMap.Data;

namespace TreasureMap.Tests
{
    [TestFixture]
    public class MapManagementTests
    {
        [Test]
        public void TestCreateMapWithInitialData()
        {
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3", 
                                                    "A - Lara - 1 - 1 - S - AADADAGGA" };
            Map map = MapManagement.CreateMapWithInitialData(lines);
            Assert.NotNull(map);
            Assert.AreEqual(2, map?.Cells?.FindAll(c => c.Type == CellType.Mountain)?.Count); // 2 mountains
            Assert.AreEqual(2, map?.Cells?.FindAll(c => c.Type == CellType.Treasure)?.Count); // 2 treasures
            Assert.AreEqual(8, map?.Cells?.FindAll(c => c.Type == CellType.Neutral)?.Count); // 8 neutral cells
            Assert.AreEqual(1, map?.Adventurers?.Count); // 1 adventurer
        }

        [Test]
        public void TestCreateMapWithEmptyInitialData()
        {
            List<string> lines = new List<string>();
            Map map = MapManagement.CreateMapWithInitialData(lines);
            Assert.NotNull(map);
            Assert.AreEqual(0, map?.Width); // map width = 0
            Assert.AreEqual(0, map?.Height); // map height = 0
            Assert.IsNull(map?.Cells); // no cells in the map
            Assert.IsNull(map?.Adventurers); // no adventurers in the map
        }

        [Test]
        public void TestCreateMapWithNullInitialData()
        {
            List<string> lines = null;
            Map map = MapManagement.CreateMapWithInitialData(lines);
            Assert.NotNull(map);
            Assert.AreEqual(0, map?.Width); // map width = 0
            Assert.AreEqual(0, map?.Height); // map height = 0
            Assert.IsNull(map?.Cells); // no cells in the map
            Assert.IsNull(map?.Adventurers); // no adventurers in the map
        }

        [Test]
        public void TestSimulateTreasureSearch()
        {
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3",
                                                    "A - Lara - 1 - 1 - S - AADADAGGA" };
            Map map = MapManagement.CreateMapWithInitialData(lines);
            MapManagement.SimulateTreasureSearch(map);
            Assert.AreEqual(0, map?.Adventurers[0].X);
            Assert.AreEqual(3, map?.Adventurers[0].Y);
            Assert.AreEqual(Orientation.S, map?.Adventurers[0].Orientation);
            Assert.AreEqual(3, map?.Adventurers[0].CollectedTreasure);
        }
    }
}
