using NUnit.Framework;
using TreasureMap.Data;

namespace TreasureMap.Tests
{
    [TestFixture]
    public class AdventurerTests
    {
        [Test]
        public void TestPickTreasure()
        {
            Adventurer adventurer = new Adventurer();
            adventurer.PickTreasure();
            Assert.AreEqual(1, adventurer?.CollectedTreasure);
        }

        [Test]
        public void TestMoveForwardWithDirectionE()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.E };
            adventurer.MoveForward();
            Assert.AreEqual(2, adventurer.X);
            Assert.AreEqual(1, adventurer.Y);
        }

        [Test]
        public void TestMoveForwardWithDirectionO()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.O };
            adventurer.MoveForward();
            Assert.AreEqual(0, adventurer.X);
            Assert.AreEqual(1, adventurer.Y);
        }

        [Test]
        public void TestMoveForwardWithDirectionN()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.N };
            adventurer.MoveForward();
            Assert.AreEqual(1, adventurer.X);
            Assert.AreEqual(0, adventurer.Y);
        }

        [Test]
        public void TestMoveForwardWithDirectionS()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.S };
            adventurer.MoveForward();
            Assert.AreEqual(1, adventurer.X);
            Assert.AreEqual(2, adventurer.Y);
        }

        [Test]
        public void TestTurnRightWithDirectionE()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.E };
            adventurer.TurnRight();
            Assert.AreEqual(Orientation.S, adventurer.Orientation);
        }

        [Test]
        public void TestTurnRightWithDirectionO()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.O };
            adventurer.TurnRight();
            Assert.AreEqual(Orientation.N, adventurer.Orientation);
        }

        [Test]
        public void TestTurnRightWithDirectionN()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.N };
            adventurer.TurnRight();
            Assert.AreEqual(Orientation.E, adventurer.Orientation);
        }

        [Test]
        public void TestTurnRightWithDirectionS()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.S };
            adventurer.TurnRight();
            Assert.AreEqual(Orientation.O, adventurer.Orientation);
        }

        [Test]
        public void TestTurnLeftWithDirectionE()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.E };
            adventurer.TurnLeft();
            Assert.AreEqual(Orientation.N, adventurer.Orientation);
        }

        [Test]
        public void TestTurnLeftWithDirectionO()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.O };
            adventurer.TurnLeft();
            Assert.AreEqual(Orientation.S, adventurer.Orientation);
        }

        [Test]
        public void TestTurnLeftWithDirectionN()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.N };
            adventurer.TurnLeft();
            Assert.AreEqual(Orientation.O, adventurer.Orientation);
        }

        [Test]
        public void TestTurnLeftWithDirectionS()
        {
            Adventurer adventurer = new Adventurer { X = 1, Y = 1, Orientation = Orientation.S };
            adventurer.TurnLeft();
            Assert.AreEqual(Orientation.E, adventurer.Orientation);
        }
    }
}
