using Microsoft.VisualStudio.TestTools.UnitTesting;
using PbpRdfApi.Plays;

namespace PbpRdfApiTests
{
    [TestClass]
    public class PlayTests
    {
        [TestMethod]
        public void JumpBallTest()
        {
            var gameEventFactory = new PlayFactory(TestData.TripleStore);
            var gameIri = "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury";
            var jumpBall = gameEventFactory.Play(gameIri, 1) as JumpBall;
            Assert.AreEqual(jumpBall.Iri, "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/1");
            Assert.AreEqual(jumpBall.Label, "Wings: Liz Cambage vs. Brittney Griner (Kayla Thornton gains possession)");
            Assert.AreEqual(jumpBall.GameIri, gameIri);
            Assert.AreEqual(jumpBall.TeamIri, "http://stellman-greene.com/pbprdf/teams/Wings");
            Assert.AreEqual(jumpBall.EventNumber, 1);
            Assert.AreEqual(jumpBall.Time, "10:00");
            Assert.AreEqual(jumpBall.HomeScore, 0);
            Assert.AreEqual(jumpBall.AwayScore, 0);
            Assert.AreEqual(jumpBall.Period, 1);
            Assert.AreEqual(jumpBall.SecondsIntoGame, 0);
            Assert.AreEqual(jumpBall.SecondsLeftInPeriod, 600);
            Assert.AreEqual(jumpBall.SecondsUntilNextEvent, 15);
            Assert.AreEqual(jumpBall.SecondsSincePreviousEvent, 0);
            Assert.AreEqual(jumpBall.NextEventIri, "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/2");
            Assert.IsNull(jumpBall.PreviousEventIri);
            Assert.AreEqual(jumpBall.HomePlayerIri, "http://stellman-greene.com/pbprdf/players/Brittney_Griner");
            Assert.AreEqual(jumpBall.AwayPlayerIri, "http://stellman-greene.com/pbprdf/players/Liz_Cambage");
            Assert.AreEqual(jumpBall.JumpBallGainedPossessionIri, "http://stellman-greene.com/pbprdf/players/Kayla_Thornton");
        }

        [TestMethod]
        public void BlockAndShotTest()
        {
            // Slock is a subclass of Shot
            var gameEventFactory = new PlayFactory(TestData.TripleStore);
            var gameIri = "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury";
            var block = gameEventFactory.Play(gameIri, 130) as Block;
            Assert.AreEqual(block.Iri, "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/130");
            Assert.AreEqual(block.Label, "Wings: Devereaux Peters blocks Allisha Gray's two point shot");
            Assert.AreEqual(block.GameIri, gameIri);
            Assert.AreEqual(block.TeamIri, "http://stellman-greene.com/pbprdf/teams/Wings");
            Assert.AreEqual(block.EventNumber, 130);
            Assert.AreEqual(block.Time, "6:12");
            Assert.AreEqual(block.HomeScore, 37);
            Assert.AreEqual(block.AwayScore, 35);
            Assert.AreEqual(block.Period, 2);
            Assert.AreEqual(block.SecondsIntoGame, 828);
            Assert.AreEqual(block.SecondsLeftInPeriod, 372);
            Assert.AreEqual(block.SecondsUntilNextEvent, 2);
            Assert.AreEqual(block.SecondsSincePreviousEvent, 0);
            Assert.AreEqual(block.Points, 0);
            Assert.AreEqual(block.PreviousEventIri, "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/129");
            Assert.AreEqual(block.NextEventIri, "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/131");
            Assert.AreEqual(block.PlayerIri, "http://stellman-greene.com/pbprdf/players/Allisha_Gray");
            Assert.AreEqual(block.BlockedByPlayerIri, "http://stellman-greene.com/pbprdf/players/Devereaux_Peters");
        }
    }
}
