using Monopoly.Abstract;

namespace Monopoly.Fields
{
    internal class Bank : SpesialField
    {
        internal Bank(string name, int rentalPrice, Player owner)
            : base(name, rentalPrice, owner)
        {
        }
    }
}
