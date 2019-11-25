using System.Collections.Generic;
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
            Set2x3GameBoard();
            List<TurtleAction> actions = GetExitSequence();

            ActionSequence seq = new ActionSequence() { Actions = actions };
            Game.Sequences.Add(seq);

            List<GameOver> result = Game.ExecuteSequences();

            Assert.True(result.Count == 1);
            Assert.True(result[0] == GameOver.Success);
        }

        [Fact]
        public void ExecuteSequences_MineHit()
        {
            Set2x3GameBoard();
            List<TurtleAction> actions = GetMineHitSequence();

            ActionSequence seq = new ActionSequence() { Actions = actions };
            Game.Sequences.Add(seq);

            List<GameOver> result = Game.ExecuteSequences();

            Assert.True(result.Count == 1);
            Assert.True(result[0] == GameOver.MineHit);
        }

        [Fact]
        public void ExecuteSequences_StillInDanger()
        {
            Set2x3GameBoard();
            List<TurtleAction> actions = GetStillInDangerSequence();

            ActionSequence seq = new ActionSequence() { Actions = actions };
            Game.Sequences.Add(seq);

            List<GameOver> result = Game.ExecuteSequences();

            Assert.True(result.Count == 1);
            Assert.True(result[0] == GameOver.StillInDanger);
        }

        [Fact]
        public void ExecuteSequences_Success_MineHit()
        {
            Set2x3GameBoard();
            List<TurtleAction> actions1 = GetExitSequence();
            List<TurtleAction> actions2 = GetMineHitSequence();

            ActionSequence seq1 = new ActionSequence() { Actions = actions1 };
            ActionSequence seq2 = new ActionSequence() { Actions = actions2 };
            Game.Sequences.Add(seq1);
            Game.Sequences.Add(seq2);

            List<GameOver> result = Game.ExecuteSequences();

            Assert.True(result.Count == 2);
            Assert.True(result[0] == GameOver.Success);
            Assert.True(result[1] == GameOver.MineHit);
        }

        [Fact]
        public void ExecuteSequences_Success_MineHit_StillInDanger()
        {
            Set2x3GameBoard();
            List<TurtleAction> actions1 = GetExitSequence();
            List<TurtleAction> actions2 = GetMineHitSequence();
            List<TurtleAction> actions3 = GetStillInDangerSequence();

            ActionSequence seq1 = new ActionSequence() { Actions = actions1 };
            ActionSequence seq2 = new ActionSequence() { Actions = actions2 };
            ActionSequence seq3 = new ActionSequence() { Actions = actions3 };

            Game.Sequences.Add(seq1);
            Game.Sequences.Add(seq2);
            Game.Sequences.Add(seq3);

            List<GameOver> result = Game.ExecuteSequences();

            Assert.True(result.Count == 3);
            Assert.True(result[0] == GameOver.Success);
            Assert.True(result[1] == GameOver.MineHit);
            Assert.True(result[2] == GameOver.StillInDanger);
        }

        #region Private non-test methods
        private void Set2x3GameBoard()
        {
            Game.GameBoard = TestHelper.GetEmptyBoard(2, 3);
            Turtle turtle = new Turtle(Direction.North);
            Exit exit = new Exit();
            Mine mine = new Mine();

            // TODO: Ideally this have to be created independent of the Domain - but will leave like this for now
            Game.GameBoard.AddGameObject(0, 0, exit);
            Game.GameBoard.AddGameObject(0, 2, turtle);
            Game.GameBoard.AddGameObject(1, 1, mine);
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
