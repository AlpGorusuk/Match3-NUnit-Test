using System;
using NUnit.Framework;

namespace Tests
{
    public class Match3Test
    {
        // A Test behaves as an ordinary method
        [Test]
        public void Match3Test1()
        {
            var gameBoard = new GameBoard(3, 3, new int[]
            {
                1, 2, 1,
                3, 1, 2,
                1, 2, 3
            });

            Assert.IsTrue(Match.MatchExists(gameBoard, 1, 4));
            Assert.IsTrue(Match.MatchExists(gameBoard, 3, 4));
            Assert.IsTrue(Match.MatchExists(gameBoard, 4, 5));

            Assert.IsFalse(Match.MatchExists(gameBoard, 7, 8));
            Assert.IsFalse(Match.MatchExists(gameBoard, 1, 5));

            var possibleMatches = Match.GetAllPossibleMatches(gameBoard);

            Assert.IsNotNull(possibleMatches);
            Assert.AreEqual(3, possibleMatches.Count);

            Assert.IsTrue(possibleMatches.Contains(new Tuple<int, int>(1, 4))
              || possibleMatches.Contains(new Tuple<int, int>(4, 1)));

            Assert.IsTrue(possibleMatches.Contains(new Tuple<int, int>(3, 4))
              || possibleMatches.Contains(new Tuple<int, int>(4, 3)));

            Assert.IsTrue(possibleMatches.Contains(new Tuple<int, int>(4, 5))
              || possibleMatches.Contains(new Tuple<int, int>(5, 4)));

            var gridBeforeShuffle = (int[])gameBoard.Grid.Clone();
            Match.Shuffle(gameBoard);

            Assert.AreNotEqual(gridBeforeShuffle, gameBoard.Grid);

            var possibleMatchesAfterShuffle = Match.GetAllPossibleMatches(gameBoard);
            Assert.IsNotNull(possibleMatchesAfterShuffle);
            Assert.IsNotEmpty(possibleMatchesAfterShuffle);
            Assert.IsFalse(Match.IsAnyMatchExistsInBoard(gameBoard));
        }
        [Test, MaxTime(1000)]
        public void Match3Test2()
        {
            for (var i = 0; i < 10000; i++)
            {
                var gameBoard = new GameBoard(3, 3, new int[]
                {
                    1, 2, 1,
                    3, 1, 2,
                    1, 2, 3
                });

                var gridBeforeShuffle = (int[])gameBoard.Grid.Clone();
                Match.Shuffle(gameBoard);

                Assert.AreNotEqual(gridBeforeShuffle, gameBoard.Grid);

                var possibleMatchesAfterShuffle = Match.GetAllPossibleMatches(gameBoard);
                Assert.IsNotNull(possibleMatchesAfterShuffle);
                Assert.IsNotEmpty(possibleMatchesAfterShuffle);
                Assert.IsFalse(Match.IsAnyMatchExistsInBoard(gameBoard));
            }
        }
        [Test]
        public void Match3Test3()
        {
            var gameBoard = new GameBoard(6, 4, new int[]
            {
                1, 2, 1, 3, 1, 4,
                3, 4, 2, 1, 3, 4,
                1, 2, 3, 4, 3, 1,
                4, 1, 1, 3, 2, 3
            });

            Assert.IsTrue(Match.MatchExists(gameBoard, 3, 9));
            Assert.IsTrue(Match.MatchExists(gameBoard, 7, 8));
            Assert.IsTrue(Match.MatchExists(gameBoard, 12, 18));
            Assert.IsTrue(Match.MatchExists(gameBoard, 22, 21));

            Assert.IsFalse(Match.MatchExists(gameBoard, 13, 19));
            Assert.IsFalse(Match.MatchExists(gameBoard, 16, 17));
            Assert.IsFalse(Match.MatchExists(gameBoard, 0, 6));

            var possibleMatches = Match.GetAllPossibleMatches(gameBoard);
            Assert.IsNotNull(possibleMatches);
            Assert.AreEqual(8, possibleMatches.Count);

            Assert.IsTrue(possibleMatches.Contains(new Tuple<int, int>(3, 4))
                          || possibleMatches.Contains(new Tuple<int, int>(4, 3)));

            Assert.IsTrue(possibleMatches.Contains(new Tuple<int, int>(15, 21))
                          || possibleMatches.Contains(new Tuple<int, int>(21, 15)));

            Assert.IsTrue(possibleMatches.Contains(new Tuple<int, int>(16, 22))
                          || possibleMatches.Contains(new Tuple<int, int>(22, 16)));

            Assert.IsTrue(possibleMatches.Contains(new Tuple<int, int>(22, 23))
                          || possibleMatches.Contains(new Tuple<int, int>(23, 22)));

            var gridBeforeShuffle = (int[])gameBoard.Grid.Clone();
            Match.Shuffle(gameBoard);

            Assert.AreNotEqual(gridBeforeShuffle, gameBoard.Grid);

            var possibleMatchesAfterShuffle = Match.GetAllPossibleMatches(gameBoard);
            Assert.IsNotNull(possibleMatchesAfterShuffle);
            Assert.IsNotEmpty(possibleMatchesAfterShuffle);
            Assert.IsFalse(Match.IsAnyMatchExistsInBoard(gameBoard));
        }
    }
}