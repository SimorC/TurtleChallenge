using System.Collections.Generic;
using System.Linq;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Domain.Model.Enum;
using TurtleChallenge.Test.Helper;
using Xunit;

namespace TurtleChallenge.Test
{
    public class ActionsTest
    {
        [Fact]
        public void ExecuteSequences_Success()
        {
            Board board = Set2x3GameBoard(out Coordinate initialPos, out Turtle turtle);
            List<TurtleAction> actions = GetExitSequence();

            ActionSequence seq = new ActionSequence() { Actions = actions };
            List<ActionSequence> listSeq = new List<ActionSequence>();
            listSeq.Add(seq);

            List<GameOver> result = board.ExecuteSequences(listSeq, initialPos, turtle).ToList();

            Assert.True(result.Count == 1);
            Assert.True(result[0] == GameOver.Success);
        }

        [Fact]
        public void ExecuteSequences_MineHit()
        {
            Board board = Set2x3GameBoard(out Coordinate initialPos, out Turtle turtle);
            List<TurtleAction> actions = GetMineHitSequence();

            ActionSequence seq = new ActionSequence() { Actions = actions };
            List<ActionSequence> listSeq = new List<ActionSequence>();
            listSeq.Add(seq);

            List<GameOver> result = board.ExecuteSequences(listSeq, initialPos, turtle).ToList();

            Assert.True(result.Count == 1);
            Assert.True(result[0] == GameOver.MineHit);
        }

        [Fact]
        public void ExecuteSequences_StillInDanger()
        {
            Board board = Set2x3GameBoard(out Coordinate initialPos, out Turtle turtle);
            List<TurtleAction> actions = GetStillInDangerSequence();

            ActionSequence seq = new ActionSequence() { Actions = actions };
            List<ActionSequence> listSeq = new List<ActionSequence>();
            listSeq.Add(seq);

            List<GameOver> result = board.ExecuteSequences(listSeq, initialPos, turtle).ToList();

            Assert.True(result.Count == 1);
            Assert.True(result[0] == GameOver.StillInDanger);
        }

        [Fact]
        public void ExecuteSequences_Success_MineHit()
        {
            Board board = Set2x3GameBoard(out Coordinate initialPos, out Turtle turtle);
            List<TurtleAction> actions1 = GetExitSequence();
            List<TurtleAction> actions2 = GetMineHitSequence();

            List<ActionSequence> listSeq = new List<ActionSequence>();

            ActionSequence seq1 = new ActionSequence() { Actions = actions1 };
            ActionSequence seq2 = new ActionSequence() { Actions = actions2 };
            listSeq.Add(seq1);
            listSeq.Add(seq2);

            List<GameOver> result = board.ExecuteSequences(listSeq, initialPos, turtle).ToList();

            Assert.True(result.Count == 2);
            Assert.True(result[0] == GameOver.Success);
            Assert.True(result[1] == GameOver.MineHit);
        }

        [Fact]
        public void ExecuteSequences_Success_MineHit_StillInDanger()
        {
            Board board = Set2x3GameBoard(out Coordinate initialPos, out Turtle turtle);
            List<TurtleAction> actions1 = GetExitSequence();
            List<TurtleAction> actions2 = GetMineHitSequence();
            List<TurtleAction> actions3 = GetStillInDangerSequence();

            List<ActionSequence> listSeq = new List<ActionSequence>();

            ActionSequence seq1 = new ActionSequence() { Actions = actions1 };
            ActionSequence seq2 = new ActionSequence() { Actions = actions2 };
            ActionSequence seq3 = new ActionSequence() { Actions = actions3 };

            listSeq.Add(seq1);
            listSeq.Add(seq2);
            listSeq.Add(seq3);

            List<GameOver> result = board.ExecuteSequences(listSeq, initialPos, turtle).ToList();

            Assert.True(result.Count == 3);
            Assert.True(result[0] == GameOver.Success);
            Assert.True(result[1] == GameOver.MineHit);
            Assert.True(result[2] == GameOver.StillInDanger);
        }

        #region Private non-test methods
        private Board Set2x3GameBoard(out Coordinate initialPos, out Turtle turtle)
        {
            Board board = TestHelper.GetEmptyBoard(2, 3);
            turtle = new Turtle(Direction.North, board);
            Exit exit = new Exit();
            Mine mine = new Mine();

            initialPos = new Coordinate(0, 2);

            // TODO: Ideally this have to be created independent of the Domain - but will leave like this for now
            board.AddGameObject(0, 0, exit);
            board.AddGameObject(initialPos, turtle);
            board.AddGameObject(1, 1, mine);

            return board;
        }

        private List<TurtleAction> GetExitSequence()
        {
            return new List<TurtleAction>()
            {
                TurtleAction.Move,
                TurtleAction.Move
            };
        }

        private List<TurtleAction> GetMineHitSequence()
        {
            return new List<TurtleAction>()
            {
                TurtleAction.Move,
                TurtleAction.Rotate,
                TurtleAction.Move
            };
        }

        private List<TurtleAction> GetStillInDangerSequence()
        {
            return new List<TurtleAction>()
            {
                TurtleAction.Move
            };
        }
        #endregion
    }
}
