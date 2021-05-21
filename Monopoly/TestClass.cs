// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Abstract;
using Monopoly.Constants;
using Monopoly.Fields;
using NUnit.Framework;
using static Monopoly.Monopoly;

namespace Monopoly
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void GetPlayersListReturnCorrectList()
        {
            string[] players = new string[]{ "Peter","Ekaterina","Alexander" };

            var expectedPlayers = new List<Player>();
            expectedPlayers.Add(new Player("Peter", FieldsPrice.StartupPlayerCapital));
            expectedPlayers.Add(new Player("Ekaterina", FieldsPrice.StartupPlayerCapital));
            expectedPlayers.Add(new Player("Alexander", FieldsPrice.StartupPlayerCapital));
            expectedPlayers.Add(new Player(SpesialNames.PlayerBankir, 0));
            expectedPlayers.Add(new Player(SpesialNames.PlayerWarder, 0));

            Monopoly monopoly = new Monopoly(players);

            var actualPlayers = monopoly.Players;

            CollectionAssert.AreEquivalent(expectedPlayers, actualPlayers);
        }

        [Test]
        public void GetFieldsListReturnCorrectList()
        {
            var expectedCompanies = new List<Field>();
            expectedCompanies.Add(new Auto("Ford", FieldsPrice.FieldAutoBuyPrice, FieldsPrice.FieldAutoRentalPrice));
            expectedCompanies.Add(new Food("MCDonald", FieldsPrice.FieldFoodBuyPrice, FieldsPrice.FieldFoodRentalPrice));
            expectedCompanies.Add(new Clother("Lamoda", FieldsPrice.FieldClotherBuyPrice, FieldsPrice.FieldClotherRentalPrice));
            expectedCompanies.Add(new Travel("Air Baltic", FieldsPrice.FieldTravelBuyPrice, FieldsPrice.FieldTravelRentalPrice));
            expectedCompanies.Add(new Travel("Nordavia", FieldsPrice.FieldTravelBuyPrice, FieldsPrice.FieldTravelRentalPrice));
            expectedCompanies.Add(new Food("KFC", FieldsPrice.FieldFoodBuyPrice, FieldsPrice.FieldFoodRentalPrice));
            expectedCompanies.Add(new Auto("TESLA", FieldsPrice.FieldAutoBuyPrice, FieldsPrice.FieldAutoRentalPrice));

            expectedCompanies.Add(new Bank(SpesialNames.FieldBank, FieldsPrice.SpesialFieldBankRentalPrice, new Player(SpesialNames.PlayerBankir, 0)));
            expectedCompanies.Add(new Prison(SpesialNames.FieldPrison, FieldsPrice.SpesialFieldPrisonRentalPrice, new Player(SpesialNames.PlayerWarder, 0)));

            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);

            var actualCompanies = monopoly.Fields;

            CollectionAssert.AreEquivalent(expectedCompanies, actualCompanies);
        }

        [Test]
        public void PlayerBuyNoOwnedCompanies()
        {
            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);

            var field = monopoly.Fields.First(f => f.Name == "Ford");
            var player = monopoly.Players.First(p => p.Name == "Peter");
            monopoly.Buy(player, field);
            
            var actualPlayer = monopoly.Players.First(p => p.Name == "Peter");
            var expectedPlayer = new Player("Peter", 5500);
            Assert.AreEqual(expectedPlayer, actualPlayer);

            var actualField = monopoly.Fields.First(f => f.Name == "Ford");
            Assert.AreEqual(actualPlayer, actualField.Owner);
        }

        [Test]
        public void RentaShouldBeCorrectTransferMoney()
        {
            string[] players = new string[] { "Peter", "Ekaterina", "Alexander" };
            Monopoly monopoly = new Monopoly(players);

            var field = monopoly.Fields.First(f => f.Name == "Ford");
            var buyPlayer = monopoly.Players.First(p => p.Name == "Peter");
            var rentalPlayer = monopoly.Players.First(p => p.Name == "Ekaterina");

            monopoly.Buy(buyPlayer, field);

            field = monopoly.Fields.First(f => f.Name == "Ford");
            monopoly.Renta(rentalPlayer, field);

            var player1 = monopoly.Players.First(p => p.Name == "Peter");
            Assert.AreEqual(5750, player1.Cash);

            var player2 = monopoly.Players.First(p => p.Name == "Ekaterina");
            Assert.AreEqual(5750, player2.Cash);
        }
    }
}
