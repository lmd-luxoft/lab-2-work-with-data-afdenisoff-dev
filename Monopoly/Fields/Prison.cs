using Monopoly.Abstract;

namespace Monopoly.Fields
{
    internal class Prison : SpesialField
    {
        internal Prison(string name, int rentalPrice, Player owner)
            : base(name, rentalPrice, owner)
        {
        }
    }
}
