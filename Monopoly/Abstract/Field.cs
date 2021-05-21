using System;

namespace Monopoly.Abstract
{
    internal abstract class Field
    {
        internal string Name { get; }

        internal Player Owner { get; private set; }

        internal virtual bool IsBought { get; private set; }

        internal int BuyPrice { get; }

        internal int RentalPrice { get; }

        internal virtual bool IsSpesialField => false;

        internal Field(string name, int buyPrice, int rentalPrice)
        {
            Name = name;
            BuyPrice = buyPrice;
            RentalPrice = rentalPrice;
        }

        internal virtual void BuyByPlayer(Player newOwner)
        {
            if (!IsSpesialField)
            {
                SetOwner(newOwner);
                Owner.SubMoney(BuyPrice);
                IsBought = true;
            }
        }

        protected void SetOwner(Player newOwner)
        {
            if (newOwner == null)
                return;

            Owner = newOwner;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Field otherField)
            {
                return
                    Name.Equals(otherField.Name)
                    && BuyPrice == otherField.BuyPrice
                    && RentalPrice == otherField.RentalPrice;
            }

            return false;
        }

        public override int GetHashCode() =>
             HashCode.Combine(Name, BuyPrice, RentalPrice);
    }
}
