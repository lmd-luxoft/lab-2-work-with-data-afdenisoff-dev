namespace Monopoly.Abstract
{
    internal abstract class SpesialField : Field
    {
        internal override bool IsSpesialField => true;

        internal override bool IsBought => true;

        internal SpesialField(string name, int rentalPrice, Player owner)
            : base(name, 0, rentalPrice)
        {
            SetOwner(owner);
        }
    }
}
