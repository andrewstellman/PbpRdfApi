using InCube.Core.Functional;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PbpRdfApi.Plays;

namespace PbpRdfApiTests
{
    [TestClass]
    public class PlayTests
    {
        readonly PlayFactory playFactory = new PlayFactory(TestData.TripleStore);

        [TestMethod]
        public void JumpBallTest()
        {
            var gameIri = "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury";
            var jumpBall = playFactory.Play(gameIri, 1) as JumpBall;
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/1", jumpBall.Iri);
            Assert.AreEqual("Wings: Liz Cambage vs. Brittney Griner (Kayla Thornton gains possession)", jumpBall.Label);
            Assert.AreEqual(gameIri, jumpBall.GameIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Wings", jumpBall.TeamIri);
            Assert.AreEqual(1, jumpBall.EventNumber);
            Assert.AreEqual("10:00", jumpBall.Time);
            Assert.AreEqual(0, jumpBall.HomeScore);
            Assert.AreEqual(0, jumpBall.AwayScore);
            Assert.AreEqual(1, jumpBall.Period);
            Assert.AreEqual(0, jumpBall.SecondsIntoGame);
            Assert.AreEqual(600, jumpBall.SecondsLeftInPeriod);
            Assert.AreEqual(Option<int>.None, jumpBall.SecondsSincePreviousEvent);
            Assert.AreEqual(15, jumpBall.SecondsUntilNextEvent);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/2", jumpBall.NextEventIri);
            Assert.AreEqual(Option<string>.None, jumpBall.PreviousEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Brittney_Griner", jumpBall.HomePlayerIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Liz_Cambage", jumpBall.AwayPlayerIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Kayla_Thornton", jumpBall.JumpBallGainedPossessionIri);
        }

        [TestMethod]
        public void BlockAndShotTest()
        {
            // Block is a subclass of Shot
            var gameIri = "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury";
            var block = playFactory.Play(gameIri, 130) as Block;
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/130", block.Iri);
            Assert.AreEqual("Wings: Devereaux Peters blocks Allisha Gray's two point shot", block.Label);
            Assert.AreEqual(gameIri, block.GameIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Wings", block.TeamIri);
            Assert.AreEqual(130, block.EventNumber);
            Assert.AreEqual("6:12", block.Time);
            Assert.AreEqual(37, block.HomeScore);
            Assert.AreEqual(35, block.AwayScore);
            Assert.AreEqual(2, block.Period);
            Assert.AreEqual(828, block.SecondsIntoGame);
            Assert.AreEqual(372, block.SecondsLeftInPeriod);
            Assert.AreEqual(0, block.SecondsSincePreviousEvent);
            Assert.AreEqual(2, block.SecondsUntilNextEvent);
            Assert.AreEqual(Option<int>.None, block.Points);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/129", block.PreviousEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/131", block.NextEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Allisha_Gray", block.PlayerIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Devereaux_Peters", block.BlockedByPlayerIri);
        }

        [TestMethod]
        public void EjectionTest()
        {
            var gameIri = "http://stellman-greene.com/pbprdf/games/2017-09-06_Wings_at_Mystics";
            var ejection = playFactory.Play(gameIri, 391) as Ejection;
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2017-09-06_Wings_at_Mystics/391", ejection.Iri);
            Assert.AreEqual("Wings: Aerial Powers ejected", ejection.Label);
            Assert.AreEqual(gameIri, ejection.GameIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Wings", ejection.TeamIri);
            Assert.AreEqual(391, ejection.EventNumber);
            Assert.AreEqual("1:10", ejection.Time);
            Assert.AreEqual(76, ejection.HomeScore);
            Assert.AreEqual(71, ejection.AwayScore);
            Assert.AreEqual(4, ejection.Period);
            Assert.AreEqual(2330, ejection.SecondsIntoGame);
            Assert.AreEqual(70, ejection.SecondsLeftInPeriod);
            Assert.AreEqual(0, ejection.SecondsSincePreviousEvent);
            Assert.AreEqual(0, ejection.SecondsUntilNextEvent);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2017-09-06_Wings_at_Mystics/390", ejection.PreviousEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2017-09-06_Wings_at_Mystics/392", ejection.NextEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Aerial_Powers", ejection.PlayerEjectedIri);
        }

        [TestMethod]
        public void EndOfPeriodTest()
        {
            var gameIri = "http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream";
            var endOfPeriod = playFactory.Play(gameIri, 370) as EndOfPeriod;
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream/370", endOfPeriod.Iri);
            Assert.AreEqual("Dream: End of the 4th Quarter", endOfPeriod.Label);
            Assert.AreEqual(gameIri, endOfPeriod.GameIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Dream", endOfPeriod.TeamIri);
            Assert.AreEqual(370, endOfPeriod.EventNumber);
            Assert.AreEqual("0.0", endOfPeriod.Time);
            Assert.AreEqual(81, endOfPeriod.HomeScore);
            Assert.AreEqual(86, endOfPeriod.AwayScore);
            Assert.AreEqual(4, endOfPeriod.Period);
            Assert.AreEqual(2400, endOfPeriod.SecondsIntoGame);
            Assert.AreEqual(0, endOfPeriod.SecondsLeftInPeriod);
            Assert.AreEqual(2, endOfPeriod.SecondsSincePreviousEvent);
            Assert.AreEqual(0, endOfPeriod.SecondsUntilNextEvent);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream/369", endOfPeriod.PreviousEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream/371", endOfPeriod.NextEventIri);
        }

        [TestMethod]
        public void EndOfGameTest()
        {
            var gameIri = "http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream";
            var endOfPeriod = playFactory.Play(gameIri, 371) as EndOfGame;
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream/371", endOfPeriod.Iri);
            Assert.AreEqual("Dream: End of Game", endOfPeriod.Label);
            Assert.AreEqual(gameIri, endOfPeriod.GameIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Dream", endOfPeriod.TeamIri);
            Assert.AreEqual(371, endOfPeriod.EventNumber);
            Assert.AreEqual("0.0", endOfPeriod.Time);
            Assert.AreEqual(81, endOfPeriod.HomeScore);
            Assert.AreEqual(86, endOfPeriod.AwayScore);
            Assert.AreEqual(4, endOfPeriod.Period);
            Assert.AreEqual(2400, endOfPeriod.SecondsIntoGame);
            Assert.AreEqual(0, endOfPeriod.SecondsLeftInPeriod);
            Assert.AreEqual(0, endOfPeriod.SecondsSincePreviousEvent);
            Assert.AreEqual(Option<int>.None, endOfPeriod.SecondsUntilNextEvent);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream/370", endOfPeriod.PreviousEventIri);
            Assert.AreEqual(Option<string>.None, endOfPeriod.NextEventIri);
        }

        [TestMethod]
        public void EntersTest()
        {
            var gameIri = "http://stellman-greene.com/pbprdf/games/2017-09-06_Wings_at_Mystics";
            var enters = playFactory.Play(gameIri, 392) as Enters;
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2017-09-06_Wings_at_Mystics/392", enters.Iri);
            Assert.AreEqual("Wings: Allisha Gray enters the game for Aerial Powers", enters.Label);
            Assert.AreEqual(gameIri, enters.GameIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Wings", enters.TeamIri);
            Assert.AreEqual(392, enters.EventNumber);
            Assert.AreEqual("1:10", enters.Time);
            Assert.AreEqual(76, enters.HomeScore);
            Assert.AreEqual(71, enters.AwayScore);
            Assert.AreEqual(4, enters.Period);
            Assert.AreEqual(2330, enters.SecondsIntoGame);
            Assert.AreEqual(70, enters.SecondsLeftInPeriod);
            Assert.AreEqual(0, enters.SecondsSincePreviousEvent);
            Assert.AreEqual(0, enters.SecondsUntilNextEvent);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2017-09-06_Wings_at_Mystics/391", enters.PreviousEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2017-09-06_Wings_at_Mystics/393", enters.NextEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Aerial_Powers", enters.PlayerExitingIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Allisha_Gray", enters.PlayerEnteringIri);
        }

        [TestMethod]
        public void FiveSecondViolationTest()
        {
            var gameIri = "http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream";
            var fiveSecondViolation = playFactory.Play(gameIri, 350) as FiveSecondViolation;
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream/350", fiveSecondViolation.Iri);
            Assert.AreEqual("Mystics: 5 second violation", fiveSecondViolation.Label);
            Assert.AreEqual(gameIri, fiveSecondViolation.GameIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Mystics", fiveSecondViolation.TeamIri);
            Assert.AreEqual(350, fiveSecondViolation.EventNumber);
            Assert.AreEqual("32.0", fiveSecondViolation.Time);
            Assert.AreEqual(79, fiveSecondViolation.HomeScore);
            Assert.AreEqual(82, fiveSecondViolation.AwayScore);
            Assert.AreEqual(4, fiveSecondViolation.Period);
            Assert.AreEqual(2368, fiveSecondViolation.SecondsIntoGame);
            Assert.AreEqual(32, fiveSecondViolation.SecondsLeftInPeriod);
            Assert.AreEqual(0, fiveSecondViolation.SecondsSincePreviousEvent);
            Assert.AreEqual(0, fiveSecondViolation.SecondsUntilNextEvent);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream/349", fiveSecondViolation.PreviousEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream/351", fiveSecondViolation.NextEventIri);
        }

        [TestMethod]
        public void TechnicalFoulTest()
        {
            var gameIri = "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury";
            var technicalFoul = playFactory.Play(gameIri, 316) as TechnicalFoul;
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/316", technicalFoul.Iri);
            Assert.AreEqual("Wings: Liz Cambage technical foul (1st technical foul)", technicalFoul.Label);
            Assert.AreEqual(gameIri, technicalFoul.GameIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Wings", technicalFoul.TeamIri);
            Assert.AreEqual(316, technicalFoul.EventNumber);
            Assert.AreEqual("3:26", technicalFoul.Time);
            Assert.AreEqual(93, technicalFoul.HomeScore);
            Assert.AreEqual(75, technicalFoul.AwayScore);
            Assert.AreEqual(4, technicalFoul.Period);
            Assert.AreEqual(2194, technicalFoul.SecondsIntoGame);
            Assert.AreEqual(206, technicalFoul.SecondsLeftInPeriod);
            Assert.AreEqual(0, technicalFoul.SecondsSincePreviousEvent);
            Assert.AreEqual(0, technicalFoul.SecondsUntilNextEvent);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/315", technicalFoul.PreviousEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/317", technicalFoul.NextEventIri);
            Assert.AreEqual(false, technicalFoul.IsDelayOfGame);
            Assert.AreEqual(false, technicalFoul.IsThreeSecond);
            Assert.AreEqual(1, technicalFoul.TechnicalFoulNumber);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Liz_Cambage", technicalFoul.FoulCommittedByPlayerIri);
        }

        [TestMethod]
        public void TurnoverTest()
        {
            var gameIri = "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury";
            var turnover = playFactory.Play(gameIri, 328) as Turnover;
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/328", turnover.Iri);
            Assert.AreEqual("Wings: Skylar Diggins-Smith bad pass (Diana Taurasi steals)", turnover.Label);
            Assert.AreEqual(gameIri, turnover.GameIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Wings", turnover.TeamIri);
            Assert.AreEqual(328, turnover.EventNumber);
            Assert.AreEqual("2:35", turnover.Time);
            Assert.AreEqual(94, turnover.HomeScore);
            Assert.AreEqual(77, turnover.AwayScore);
            Assert.AreEqual(4, turnover.Period);
            Assert.AreEqual(2245, turnover.SecondsIntoGame);
            Assert.AreEqual(155, turnover.SecondsLeftInPeriod);
            Assert.AreEqual(5, turnover.SecondsSincePreviousEvent);
            Assert.AreEqual(10, turnover.SecondsUntilNextEvent);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/327", turnover.PreviousEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/329", turnover.NextEventIri);
            Assert.AreEqual("bad pass", turnover.TurnoverType);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Skylar_Diggins-Smith", turnover.TurnedOverByPlayerIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Diana_Taurasi", turnover.StolenByPlayerIri);
        }

        [TestMethod]
        public void TimeoutTest()
        {
            var gameIri = "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury";
            var timeout = playFactory.Play(gameIri, 177) as Timeout;
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/177", timeout.Iri);
            Assert.AreEqual("Mercury: Phoenix Full timeout", timeout.Label);
            Assert.AreEqual(gameIri, timeout.GameIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Mercury", timeout.TeamIri);
            Assert.AreEqual(177, timeout.EventNumber);
            Assert.AreEqual("13.8", timeout.Time);
            Assert.AreEqual(50, timeout.HomeScore);
            Assert.AreEqual(49, timeout.AwayScore);
            Assert.AreEqual(2, timeout.Period);
            Assert.AreEqual(1187, timeout.SecondsIntoGame);
            Assert.AreEqual(13, timeout.SecondsLeftInPeriod);
            Assert.AreEqual(19, timeout.SecondsSincePreviousEvent);
            Assert.AreEqual(5, timeout.SecondsUntilNextEvent);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/176", timeout.PreviousEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury/178", timeout.NextEventIri);
            Assert.AreEqual("Full", timeout.TimeoutDuration);
            Assert.AreEqual(Option<bool>.None, timeout.IsOfficial);
        }


        [TestMethod]
        public void ReboundTest()
        {
            var gameIri = "http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream";
            var rebound = playFactory.Play(gameIri, 7) as Rebound;
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream/7", rebound.Iri);
            Assert.AreEqual("Mystics: Ariel Atkins offensive rebound", rebound.Label);
            Assert.AreEqual(gameIri, rebound.GameIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Mystics", rebound.TeamIri);
            Assert.AreEqual(7, rebound.EventNumber);
            Assert.AreEqual("8:55", rebound.Time);
            Assert.AreEqual(0, rebound.HomeScore);
            Assert.AreEqual(0, rebound.AwayScore);
            Assert.AreEqual(1, rebound.Period);
            Assert.AreEqual(65, rebound.SecondsIntoGame);
            Assert.AreEqual(535, rebound.SecondsLeftInPeriod);
            Assert.AreEqual(0, rebound.SecondsSincePreviousEvent);
            Assert.AreEqual(3, rebound.SecondsUntilNextEvent);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream/6", rebound.PreviousEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream/8", rebound.NextEventIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/players/Ariel_Atkins", rebound.ReboundedByPlayerIri);
            Assert.AreEqual(true, rebound.IsOffensive);
        }
    }
}
