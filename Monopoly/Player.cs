using System;

namespace Monopoly
{
    internal class Player
    {
        internal string Name { get; }
        public int Cash { get; private set; }

        internal Player(string name, int startMoney)
        {
            Name = name;
            Cash = startMoney;
        }

        internal void AddMoney(int moneyToAdd)
        {
            Cash += moneyToAdd;
        }

        internal void SubMoney(int moneyToSub)
        {
            Cash -= moneyToSub;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Player otherPlayer)
            {
                return 
                    Name.Equals(otherPlayer.Name)
                    && Cash == otherPlayer.Cash;
            }

            return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(Name, Cash);
    }
}
