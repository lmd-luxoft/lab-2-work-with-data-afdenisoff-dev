using System.Collections.Generic;
using System.Linq;
using Monopoly.Abstract;
using Monopoly.Constants;
using Monopoly.Fields;

namespace Monopoly
{
    partial class Monopoly
    {
        internal List<Player> Players { get; } = new List<Player>();

        internal List<Field> Fields { get; } = new List<Field>();
        
        public Monopoly(string[] playerNames)
        {
            for (int i = 0; i < playerNames.Count(); i++)
            {
                Players.Add(new Player(playerNames[i], FieldsPrice.StartupPlayerCapital));
            }

            SetFields();

            SetSpesialFields();
        }

        internal bool Buy(Player player, Field field)
        {
            if (field.IsBought)
                return false;

             field.BuyByPlayer(player);

             return true;
        }

        internal bool Renta(Player player, Field field)
        {
            if (!field.IsBought)
                return false;

            player.SubMoney(field.RentalPrice);
            field.Owner.AddMoney(field.RentalPrice);

            return true;
        }

        private void SetFields()
        {
            Fields.Add(new Auto("Ford", FieldsPrice.FieldAutoBuyPrice, FieldsPrice.FieldAutoRentalPrice));
            Fields.Add(new Food("MCDonald", FieldsPrice.FieldFoodBuyPrice, FieldsPrice.FieldFoodRentalPrice));
            Fields.Add(new Clother("Lamoda", FieldsPrice.FieldClotherBuyPrice, FieldsPrice.FieldClotherRentalPrice));
            Fields.Add(new Travel("Air Baltic", FieldsPrice.FieldTravelBuyPrice, FieldsPrice.FieldTravelRentalPrice));
            Fields.Add(new Travel("Nordavia", FieldsPrice.FieldTravelBuyPrice, FieldsPrice.FieldTravelRentalPrice));
            Fields.Add(new Food("KFC", FieldsPrice.FieldFoodBuyPrice, FieldsPrice.FieldFoodRentalPrice));
            Fields.Add(new Auto("TESLA", FieldsPrice.FieldAutoBuyPrice, FieldsPrice.FieldAutoRentalPrice));
        }

        private void SetSpesialFields()
        {
            var bankir = new Player(SpesialNames.PlayerBankir, 0);
            var warder = new Player(SpesialNames.PlayerWarder, 0);

            Players.Add(bankir);
            Players.Add(warder);

            Fields.Add(new Bank(SpesialNames.FieldBank, FieldsPrice.SpesialFieldBankRentalPrice, bankir));
            Fields.Add(new Prison(SpesialNames.FieldPrison, FieldsPrice.SpesialFieldPrisonRentalPrice, warder));
        }
    }
}
