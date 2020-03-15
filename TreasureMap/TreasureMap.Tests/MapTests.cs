using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TreasureMap.Data;

namespace TreasureMap.Tests
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void TestInitializeCells()
        {
            Map map = new Map();
            map.InitializeCells("C - 3 - 4");
            Assert.NotNull(map);
            Assert.AreEqual(3, map?.Width);
            Assert.AreEqual(4, map?.Height);
            Assert.AreEqual(12, map?.Cells?.Count);
            Assert.IsNull(map?.Adventurers);
        }

        [Test]
        public void TestInitializeMountains()
        {
            Map map = new Map();
            map.InitializeCells("C - 3 - 4");
            List<string> mountainDataLines = new List<string> { "M - 1 - 0", "M - 2 - 1" };
            map.InitializeMountains(mountainDataLines);
            Assert.AreEqual(2, map?.Cells?.FindAll(c => c.Type == CellType.Mountain).Count);
        }

        [Test]
        public void TestInitializeMountainsWithEmptyList()
        {
            Map map = new Map();
            map.InitializeCells("C - 3 - 4");
            List<string> mountainDataLines = new List<string>();
            map.InitializeMountains(mountainDataLines);
            Assert.AreEqual(0, map?.Cells?.FindAll(c => c.Type == CellType.Mountain).Count);
        }

        [Test]
        public void TestInitializeMountainsWithNullList()
        {
            Map map = new Map();
            map.InitializeCells("C - 3 - 4");
            List<string> mountainDataLines = null;
            map.InitializeMountains(mountainDataLines);
            Assert.AreEqual(0, map?.Cells?.FindAll(c => c.Type == CellType.Mountain).Count);
        }

        [Test]
        public void TestInitializeTreasures()
        {
            Map map = new Map();
            map.InitializeCells("C - 3 - 4");
            List<string> treasureDataLines = new List<string> { "T - 0 - 3 - 2", "T - 1 - 3 - 3" };
            map.InitializeTreasures(treasureDataLines);
            Assert.AreEqual(2, map?.Cells?.FindAll(c => c.Type == CellType.Treasure).Count);
        }

        [Test]
        public void TestInitializeTreasuresWithEmptyList()
        {
            Map map = new Map();
            map.InitializeCells("C - 3 - 4");
            List<string> treasureDataLines = new List<string>();
            map.InitializeTreasures(treasureDataLines);
            Assert.AreEqual(0, map?.Cells?.FindAll(c => c.Type == CellType.Treasure).Count);
        }

        [Test]
        public void TestInitializeTreasuresWithNullList()
        {
            Map map = new Map();
            map.InitializeCells("C - 3 - 4");
            List<string> treasureDataLines = null;
            map.InitializeTreasures(treasureDataLines);
            Assert.AreEqual(0, map?.Cells?.FindAll(c => c.Type == CellType.Treasure).Count);
        }

        [Test]
        public void TestInitializeAdventurers()
        {
            Map map = new Map();
            map.InitializeCells("C - 3 - 4");
            List<string> adventurerDataLines = new List<string> { "A - Lara - 1 - 1 - S - AADADAGGA" };
            map.InitializeAdventurers(adventurerDataLines);
            Assert.AreEqual(1, map?.Adventurers?.Count);
            Assert.AreEqual("Lara", map?.Adventurers[0].Name);
            Assert.AreEqual(Orientation.S, map?.Adventurers[0].Orientation);
        }

        [Test]
        public void TestInitializeAdventurersWithEmptyList()
        {
            Map map = new Map();
            map.InitializeCells("C - 3 - 4");
            List<string> adventurerDataLines = new List<string>();
            map.InitializeAdventurers(adventurerDataLines);
            Assert.IsNull(map?.Adventurers);
        }

        [Test]
        public void TestInitializeAdventurersWithNullList()
        {
            Map map = new Map();
            map.InitializeCells("C - 3 - 4");
            List<string> adventurerDataLines = null;
            map.InitializeAdventurers(adventurerDataLines);
            Assert.IsNull(map?.Adventurers);
        }
    }
}
