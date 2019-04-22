using System;
using System.Collections.Generic;
using InCube.Core.Functional;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PbpRdfApi;

namespace PbpRdfApiTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void ReadWingsAtMercuryGame()
        {
            var iri = "http://stellman-greene.com/pbprdf/games/2018-08-21_Wings_at_Mercury";
            var gameFactory = new GameFactory(TestData.TripleStore);
            var game = gameFactory.Game(iri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Wings", game.AwayTeamIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Mercury", game.HomeTeamIri);
            Assert.AreEqual(DateTimeOffset.Parse("08/21/2018 19:30:00 -05:00").ToUnixTimeSeconds(),
                game.GameTime.Value.ToUnixTimeSeconds());
            Assert.AreEqual(Option<string>.None, game.GameLocation);

            Assert.AreEqual(10, game.HomeTeamRoster.Count);
            Assert.AreEqual("Diana Taurasi", game.HomeTeamRoster["http://stellman-greene.com/pbprdf/players/Diana_Taurasi"]);
            Assert.AreEqual("DeWanna Bonner", game.HomeTeamRoster["http://stellman-greene.com/pbprdf/players/DeWanna_Bonner"]);

            Assert.AreEqual(8, game.AwayTeamRoster.Count);
            Assert.AreEqual("Liz Cambage", game.AwayTeamRoster["http://stellman-greene.com/pbprdf/players/Liz_Cambage"]);
            Assert.AreEqual("Glory Johnson", game.AwayTeamRoster["http://stellman-greene.com/pbprdf/players/Glory_Johnson"]);
        }

        [TestMethod]
        public void ReadMysticsAtDreamGame() 
        {
            var iri = "http://stellman-greene.com/pbprdf/games/2018-09-04_Mystics_at_Dream";
            var gameFactory = new GameFactory(TestData.TripleStore);
            var game = gameFactory.Game(iri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Mystics", game.AwayTeamIri);
            Assert.AreEqual("http://stellman-greene.com/pbprdf/teams/Dream", game.HomeTeamIri);
            Assert.AreEqual(DateTimeOffset.Parse("09/04/2018 18:00:00 -06:00").ToUnixTimeSeconds(),
                game.GameTime.Value.ToUnixTimeSeconds());
            Assert.AreEqual("McCamish Pavilion", game.GameLocation);

            Assert.AreEqual(9, game.HomeTeamRoster.Count);
            Assert.AreEqual("Tiffany Hayes", game.HomeTeamRoster["http://stellman-greene.com/pbprdf/players/Tiffany_Hayes"]);
            Assert.AreEqual("Renee Montgomery", game.HomeTeamRoster["http://stellman-greene.com/pbprdf/players/Renee_Montgomery"]);

            Assert.AreEqual(8, game.AwayTeamRoster.Count);
            Assert.AreEqual("Kristi Toliver", game.AwayTeamRoster["http://stellman-greene.com/pbprdf/players/Kristi_Toliver"]);
            Assert.AreEqual("Elena Delle Donne", game.AwayTeamRoster["http://stellman-greene.com/pbprdf/players/Elena_Delle_Donne"]);
        }

        [TestMethod]
        public void GameNotFound()
        {
            var iri = "http://stellman-greene.com/pbprdf/games/not_a_real_game";
            var gameFactory = new GameFactory(TestData.TripleStore);

            Assert.ThrowsException<NotFoundException>(() => gameFactory.Game(iri));
        }
    }
}
