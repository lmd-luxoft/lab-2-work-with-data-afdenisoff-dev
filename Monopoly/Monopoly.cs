using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class Monopoly
    {
        private readonly List<Player> players = new List<Player>();

        private readonly List<IField> fields = new List<IField>();
        
        public Monopoly(string[] p, int v)
        {
            for (int i = 0; i < v; i++)
            {
                players.Add(new Player(p[i], 6000));
            }
            fields.Add(new Auto("Ford", 500, 250));
            fields.Add(new Food("MCDonald", 250, 250));
            fields.Add(new Clother("Lamoda", 100, 100));
            fields.Add(new Travel("Air Baltic", 700, 300));
            fields.Add(new Travel("Nordavia", 700, 300));
            fields.Add(new Prison("Prison", 0, 1000));
            fields.Add(new Food("MCDonald", 700, 250));
            fields.Add(new Auto("TESLA", 500, 250));
            fields.Add(new Bank("Bank", 0, 700));
        }

        internal List<Player> GetPlayersList()
        {
            return players;
        }

        internal List<IField> GetFieldsList()
        {
            return fields;
        }

        internal IField GetFieldByName(string v)
        {
            return (from p in fields where p.Name == v select p).FirstOrDefault();
        }

        internal bool Buy(int playerId, Tuple<string, FieldType, int, bool> k)
        {
            if (k.Item3 != 0)
                return false;

            var player = GetPlayerInfo(playerId);
            
            int cash = 0;

            switch(k.Item2)
            {
                case FieldType.AUTO:
                    cash = player.Cash - 500;
                    players[playerId - 1] = new Player(player.Name, cash);
                    break;
                case FieldType.FOOD:
                    cash = player.Cash - 250;
                    players[playerId - 1] = new Tuple<string, int>(player.Item1, cash);
                    break;
                case FieldType.TRAVEL:
                    cash = player.Cash - 700;
                    players[playerId - 1] = new Tuple<string, int>(player.Item1, cash);
                    break;
                case FieldType.CLOTHER:
                    cash = player.Cash - 100;
                    players[playerId - 1] = new Tuple<string, int>(player.Item1, cash);
                    break;
                default:
                    return false;
            }

            int i = players.Select((item, index) => new { name = item.Item1, index = index })
                .Where(n => n.name == player.Item1)
                .Select(p => p.index).FirstOrDefault();

            fields[i] = new Tuple<string, FieldType, int, bool>(k.Item1, k.Item2, playerId, k.Item4);
             return true;
        }

        internal Player GetPlayerInfo(int playerId)
        {
            return players[playerId - 1];
        }

        internal bool Renta(int playerId, Field field)
        {
            var player = GetPlayerInfo(playerId);

            Tuple<string, int> o = null;

            if (field.PlayerId == 0)
                return false;

            switch (field.Type)
            {
                case FieldType.AUTO:
                    o =  GetPlayerInfo(field.PlayerId);
                    player = new Tuple<string, int>(player.Item1, player.Item2 - 250);
                    o = new Tuple<string, int>(o.Item1,o.Item2 + 250);
                    break;
                case FieldType.FOOD:
                    o = GetPlayerInfo(field.PlayerId);
                    player = new Tuple<string, int>(player.Item1, player.Item2 - 250);
                    o = new Tuple<string, int>(o.Item1, o.Item2 + 250);
                    break;
                case FieldType.TRAVEL:
                    o = GetPlayerInfo(field.PlayerId);
                    player = new Tuple<string, int>(player.Item1, player.Item2 - 300);
                    o = new Tuple<string, int>(o.Item1, o.Item2 + 300);
                    break;
                case FieldType.CLOTHER:
                    o = GetPlayerInfo(field.PlayerId);
                    player = new Tuple<string, int>(player.Item1, player.Item2 - 100);
                    o = new Tuple<string, int>(o.Item1, o.Item2 + 100);

                    break;
                case FieldType.PRISON:
                    player = new Tuple<string, int>(player.Item1, player.Item2 - 1000);
                    break;
                case FieldType.BANK:
                    player = new Tuple<string, int>(player.Item1, player.Item2 - 700);
                    break;
                default:
                    return false;
            }
            players[playerId - 1] = player;

            if(o != null)
                players[field.PlayerId - 1] = o;
            return true;
        }

        internal enum FieldType
        {
            AUTO,
            FOOD,
            CLOTHER,
            TRAVEL,
            PRISON,
            BANK
        }

        internal interface IField
        {          
            string Name { get; }

            int PlayerId { get; }

            bool IsBought { get; }

            int BuyPrice { get; }

            int RentalPrice { get; }

            void BuyByPlayer(int newPlayerId);
        }

        internal class Player
        {
            public string Name { get; }
            public int Cash { get; private set; }

            public Player(string name, int startMoney)
            {
                Name = name;
                Cash = startMoney;
            }

            public void AddMoney(int moneyToAdd)
            {
                Cash += moneyToAdd;
            }

            public void SubMoney(int moneyToSub)
            {
                Cash -= moneyToSub;
            }

        }

        internal class Field
        {
            public FieldType Type { get; }

            public string Name { get; }

            public int PlayerId { get; private set; }

            public bool IsBought { get; private set; }

            public int Price { get; }

            internal Field(FieldType fieldType, string name, int price)
            {
                Type = fieldType;
                Name = name;
                Price = price;
            }

            public void ChangePlayer(int newPlayerId)
            {
                PlayerId = newPlayerId;
            }
        }

        internal class Auto : IField
        {
            public string Name { get; }

            public int PlayerId { get; private set; }

            public bool IsBought { get; private set; }

            public int BuyPrice { get; }

            public int RentalPrice { get; }

            internal Auto(string name, int buyPrice, int rentalPrice)
            {
                Name = name;
                BuyPrice = buyPrice;
                RentalPrice = rentalPrice;
            }

            public void BuyByPlayer(int newPlayerId)
            {
                PlayerId = newPlayerId;
            }
        }

        internal class Food : IField
        {
            public string Name { get; }

            public int PlayerId { get; private set; }

            public bool IsBought { get; private set; }

            public int BuyPrice { get; }

            public int RentalPrice { get; }

            internal Food(string name, int buyPrice, int rentalPrice)
            {
                Name = name;
                BuyPrice = buyPrice;
                RentalPrice = rentalPrice;
            }

            public void BuyByPlayer(int newPlayerId)
            {
                PlayerId = newPlayerId;
            }
        }

        internal class Clother : IField
        {
            public string Name { get; }

            public int PlayerId { get; private set; }

            public bool IsBought { get; private set; }

            public int BuyPrice { get; }

            public int RentalPrice { get; }

            internal Clother(string name, int buyPrice, int rentalPrice)
            {
                Name = name;
                BuyPrice = buyPrice;
                RentalPrice = rentalPrice;
            }

            public void BuyByPlayer(int newPlayerId)
            {
                PlayerId = newPlayerId;
            }
        }

        internal class Travel : IField
        {
            public string Name { get; }

            public int PlayerId { get; private set; }

            public bool IsBought { get; private set; }

            public int BuyPrice { get; }

            public int RentalPrice { get; }

            internal Travel(string name, int buyPrice, int rentalPrice)
            {
                Name = name;
                BuyPrice = buyPrice;
                RentalPrice = rentalPrice;
            }

            public void BuyByPlayer(int newPlayerId)
            {
                PlayerId = newPlayerId;
            }
        }

        internal class Prison : IField
        {
            public string Name { get; }

            public int PlayerId { get; private set; }

            public bool IsBought { get; private set; }

            public int BuyPrice { get; }

            public int RentalPrice { get; }

            internal Prison(string name, int buyPrice, int rentalPrice)
            {
                Name = name;
                BuyPrice = buyPrice;
                RentalPrice = rentalPrice;
            }

            public void BuyByPlayer(int newPlayerId)
            {
                PlayerId = newPlayerId;
            }
        }

        internal class Bank : IField
        {
            public string Name { get; }

            public int PlayerId { get; private set; }

            public bool IsBought { get; private set; }

            public int BuyPrice { get; }

            public int RentalPrice { get; }

            internal Bank(string name, int buyPrice, int rentalPrice)
            {
                Name = name;
                BuyPrice = buyPrice;
                RentalPrice = rentalPrice;
            }

            public void BuyByPlayer(int newPlayerId)
            {
                PlayerId = newPlayerId;
            }
        }
    }
}
