using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureMap.Business;
using TreasureMap.Data;

namespace TreasureMap.Tests
{
    [TestFixture]
    public class AdventurerManagementTests
    {
        [Test]
        public void TestAdventurerCanMoveForwardToFreeCell()
        {
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3", 
                                                    "A - Lara - 1 - 1 - S - AADADAGGA" };
            Map map = MapManagement.CreateMapWithInitialData(lines);
            Assert.IsTrue(AdventurerManagement.AdventurerCanMoveForward(map?.Adventurers[0], map));
        }

        [Test]
        public void TestAdventurerCanNotMoveForwardToMountainCell()
        {
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3", 
                                                    "A - Lara - 1 - 1 - N - AADADAGGA" };
            Map map = MapManagement.CreateMapWithInitialData(lines);
            Assert.IsFalse(AdventurerManagement.AdventurerCanMoveForward(map?.Adventurers[0], map));
        }

        [Test]
        public void TestAdventurerCanNotMoveForwardToCellOutOfMapEdges()
        {
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3",
                                                    "A - Lara - 1 - 0 - N - AADADAGGA", "A - Sara - 0 - 3 - S - AADADAGGA",
                                                    "A - Fara - 0 - 1 - O - AADADAGGA", "A - Tara - 2 - 1 - E - AADADAGGA"};
            Map map = MapManagement.CreateMapWithInitialData(lines);
            Assert.IsFalse(AdventurerManagement.AdventurerCanMoveForward(map?.Adventurers[0], map));
            Assert.IsFalse(AdventurerManagement.AdventurerCanMoveForward(map?.Adventurers[1], map));
            Assert.IsFalse(AdventurerManagement.AdventurerCanMoveForward(map?.Adventurers[2], map));
            Assert.IsFalse(AdventurerManagement.AdventurerCanMoveForward(map?.Adventurers[3], map));
        }

        [Test]
        public void TestAdventurerCanNotMoveForwardToCellOccupiedByOtherAdventurer()
        {
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3", 
                                                    "A - Lara - 1 - 1 - O - AADADAGGA", "A - Sara - 0 - 1 - N - AADADAGGA" };
            Map map = MapManagement.CreateMapWithInitialData(lines);
            Assert.IsFalse(AdventurerManagement.AdventurerCanMoveForward(map?.Adventurers[0], map));
        }

        [Test]
        public void TestProcessAdventurerMoveForwardToFreeCell()
        {
            char move = 'A';
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3",
                                                    "A - Lara - 1 - 1 - S - AADADAGGA" };
            Map map = MapManagement.CreateMapWithInitialData(lines);
            AdventurerManagement.ProcessAdventurerMove(map.Adventurers[0], map, move);
            Assert.AreEqual(1, map.Adventurers[0].X);
            Assert.AreEqual(2, map.Adventurers[0].Y);
        }

        [Test]
        public void TestProcessAdventurerMoveForwardToTreasureCell()
        {
            char move = 'A';
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3",
                                                    "A - Lara - 1 - 2 - S - AADADAGGA" };
            Map map = MapManagement.CreateMapWithInitialData(lines);
            AdventurerManagement.ProcessAdventurerMove(map.Adventurers[0], map, move);
            Assert.AreEqual(1, map.Adventurers[0].X);
            Assert.AreEqual(3, map.Adventurers[0].Y);
            Assert.AreEqual(1, map.Adventurers[0].CollectedTreasure);
            Assert.AreEqual(2, map.Cells.FirstOrDefault(c => c.X == 1 && c.Y == 3)?.TreasureAmount);
        }

        [Test]
        public void TestProcessAdventurerMoveForwardToBlockedCell()
        {
            char move = 'A';
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3",
                                                    "A - Lara - 1 - 1 - N - AADADAGGA", "A - Sara - 2 - 0 - N - AADADAGGA",
                                                    "A - Mara - 2 - 2 - E - AADADAGGA", "A - Fara - 2 - 3 - S - AADADAGGA",
                                                    "A - Tara - 0 - 1 - O - AADADAGGA"};
            Map map = MapManagement.CreateMapWithInitialData(lines);
            AdventurerManagement.ProcessAdventurerMove(map.Adventurers[0], map, move);
            Assert.AreEqual(1, map.Adventurers[0].X);
            Assert.AreEqual(1, map.Adventurers[0].Y);
            AdventurerManagement.ProcessAdventurerMove(map.Adventurers[1], map, move);
            Assert.AreEqual(2, map.Adventurers[1].X);
            Assert.AreEqual(0, map.Adventurers[1].Y);
            AdventurerManagement.ProcessAdventurerMove(map.Adventurers[2], map, move);
            Assert.AreEqual(2, map.Adventurers[2].X);
            Assert.AreEqual(2, map.Adventurers[2].Y);
            AdventurerManagement.ProcessAdventurerMove(map.Adventurers[3], map, move);
            Assert.AreEqual(2, map.Adventurers[3].X);
            Assert.AreEqual(3, map.Adventurers[3].Y);
            AdventurerManagement.ProcessAdventurerMove(map.Adventurers[4], map, move);
            Assert.AreEqual(0, map.Adventurers[4].X);
            Assert.AreEqual(1, map.Adventurers[4].Y);
        }

        [Test]
        public void TestProcessAdventurerTurnLeft()
        {
            char move = 'G';
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3",
                                                    "A - Lara - 1 - 1 - N - AADADAGGA" };
            Map map = MapManagement.CreateMapWithInitialData(lines);
            AdventurerManagement.ProcessAdventurerMove(map.Adventurers[0], map, move);
            Assert.AreEqual(Orientation.O, map.Adventurers[0].Orientation);
        }

        [Test]
        public void TestProcessAdventurerTurnRight()
        {
            char move = 'D';
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3",
                                                    "A - Lara - 1 - 1 - N - AADADAGGA" };
            Map map = MapManagement.CreateMapWithInitialData(lines);
            AdventurerManagement.ProcessAdventurerMove(map.Adventurers[0], map, move);
            Assert.AreEqual(Orientation.E, map.Adventurers[0].Orientation);
        }
    }
}
