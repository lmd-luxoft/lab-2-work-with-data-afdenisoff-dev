using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class Monopoly
    {
        private List<Player> players = new List<Player>();

        public List<Tuple<string, Monopoly.FieldType, int, bool>> fields = new List<Tuple<string, FieldType, int, bool>>();
        
        public Monopoly(string[] p, int v)
        {
            for (int i = 0; i < v; i++)
            {
                players.Add(new Tuple<string,int>(p[i], 6000));     
            }
            fields.Add(new Tuple<string, Monopoly.FieldType, int, bool>("Ford", Monopoly.FieldType.AUTO, 0, false));
            fields.Add(new Tuple<string, Monopoly.FieldType, int, bool>("MCDonald", Monopoly.FieldType.FOOD, 0, false));
            fields.Add(new Tuple<string, Monopoly.FieldType, int, bool>("Lamoda", Monopoly.FieldType.CLOTHER, 0, false));
            fields.Add(new Tuple<string, Monopoly.FieldType, int, bool>("Air Baltic", Monopoly.FieldType.TRAVEL, 0, false));
            fields.Add(new Tuple<string, Monopoly.FieldType, int, bool>("Nordavia", Monopoly.FieldType.TRAVEL, 0, false));
            fields.Add(new Tuple<string, Monopoly.FieldType, int, bool>("Prison", Monopoly.FieldType.PRISON, 0, false));
            fields.Add(new Tuple<string, Monopoly.FieldType, int, bool>("MCDonald", Monopoly.FieldType.FOOD, 0, false));
            fields.Add(new Tuple<string, Monopoly.FieldType, int, bool>("TESLA", Monopoly.FieldType.AUTO, 0, false));
        }

        internal List<Player> GetPlayersList()
        {
            return players;
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

        internal List<Tuple<string, Monopoly.FieldType, int, bool>> GetFieldsList()
        {
            return fields;
        }

        internal Tuple<string, FieldType, int, bool> GetFieldByName(string v)
        {
            return (from p in fields where p.Item1 == v select p).FirstOrDefault();
        }

        internal bool Buy(int playerId, Tuple<string, FieldType, int, bool> k)
        {
            var player = GetPlayerInfo(playerId);
            
            int cash = 0;

            switch(k.Item2)
            {
                case FieldType.AUTO:
                    if (k.Item3 != 0)
                        return false;
                    cash = player.Item2 - 500;
                    players[playerId - 1] = new Tuple<string, int>(player.Item1, cash);
                    break;
                case FieldType.FOOD:
                    if (k.Item3 != 0)
                        return false;
                    cash = player.Item2 - 250;
                    players[playerId - 1] = new Tuple<string, int>(player.Item1, cash);
                    break;
                case FieldType.TRAVEL:
                    if (k.Item3 != 0)
                        return false;
                    cash = player.Item2 - 700;
                    players[playerId - 1] = new Tuple<string, int>(player.Item1, cash);
                    break;
                case FieldType.CLOTHER:
                    if (k.Item3 != 0)
                        return false;
                    cash = player.Item2 - 100;
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
                    o = new Tuple<string, int>(o.Item1, o.Item2 + 1000);

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

        internal class Field
        {
            public FieldType Type { get; }

            public string Name { get; }

            public int PlayerId { get; private set; }

            public bool IsBuy { get; private set; }

            internal Field(FieldType fieldType, string name)
            {
                Type = fieldType;
                Name = name;
            }

            public void ChangePlayer(int newPlayerId)
            {
                PlayerId = newPlayerId;
            }
        }

        internal class Player
        {
            public string Name { get; }
            public int Money { get; private set; }

            public Player(string name, int startMoney)
            {
                Name = name;
                Money = startMoney;
            }

            public void AddMoney(int moneyToAdd)
            {
                Money += moneyToAdd;
            }

            public void SubMoney(int moneyToSub)
            {
                Money -= moneyToSub;
            }

        }
    }
}
