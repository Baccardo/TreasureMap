using NUnit.Framework;
using System.Collections.Generic;
using TreasureMap.Business;
using TreasureMap.Data;

namespace TreasureMap.Tests
{
    [TestFixture]
    public class FileManagementTests
    {
        [Test]
        public void TestGetInitialData()
        {
            string path = @".\Data\treasure_map.txt";
            List<string> lines = FileManagement.GetDataLinesFromFile(path);
            Assert.IsNotNull(lines);
            Assert.IsNotEmpty(lines);
            Assert.AreEqual(6, lines?.Count);
        }

        [Test]
        public void TestDataLinesAreNotValidWithFileTypeNotTxt()
        {
            string path = @".\Data\treasure_map";
            (bool check, string msg) = FileManagement.DataLinesAreValid(path);
            Assert.IsFalse(check);
            Assert.AreEqual(Messages.FileTypeError, msg);
        }

        [Test]
        public void TestDataLinesAreNotValidWithEmptyFile()
        {
            string path = @".\Data\treasure_map_empty.txt";
            (bool check, string msg) = FileManagement.DataLinesAreValid(path);
            Assert.IsFalse(check);
            Assert.AreEqual(Messages.NoMapSizeError + System.Environment.NewLine +
                            Messages.NoTreasureError + System.Environment.NewLine +
                            Messages.NoAdventurerError, msg);
        }

        [Test]
        public void TestDataLinesAreNotValidWithoutMapSizeLine()
        {
            string path = @".\Data\treasure_map_without_map_line.txt";
            (bool check, string msg) = FileManagement.DataLinesAreValid(path);
            Assert.IsFalse(check);
            Assert.AreEqual(Messages.NoMapSizeError + System.Environment.NewLine, msg);
        }

        [Test]
        public void TestDataLinesAreNotValidWithoutTreasureLines()
        {
            string path = @".\Data\treasure_map_without_treasure_lines.txt";
            (bool check, string msg) = FileManagement.DataLinesAreValid(path);
            Assert.IsFalse(check);
            Assert.AreEqual(Messages.NoTreasureError + System.Environment.NewLine, msg);
        }

        [Test]
        public void TestDataLinesAreNotValidWithoutAdventurerLines()
        {
            string path = @".\Data\treasure_map_without_adventurer_lines.txt";
            (bool check, string msg) = FileManagement.DataLinesAreValid(path);
            Assert.IsFalse(check);
            Assert.AreEqual(Messages.NoAdventurerError, msg);
        }

        [Test]
        public void TestSaveSimulation()
        {
            string path = @".\Data\Simulation_Results.txt";
            List<string> lines = new List<string> { "C - 3 - 4", "M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2", "T - 1 - 3 - 3",
                                                    "A - Lara - 1 - 1 - S - AADADAGGA" };
            Map map = MapManagement.CreateMapWithInitialData(lines);
            MapManagement.SimulateTreasureSearch(map);
            Assert.AreEqual(Messages.FileSavedOK + path, FileManagement.SaveSimulation(map, path));
        }
    }
}
