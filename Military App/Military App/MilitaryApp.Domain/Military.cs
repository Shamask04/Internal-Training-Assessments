using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryApp.Domain
{
    public class ViewMilitary
    {
        public string Name { get; set; }
        public King King { get; set; }

    }
    public class MilitaryBattle
    {
        public int MilitaryId { get; set; }
        public int BattleId { get; set; }
        public Military Military { get; set; }
        public Battle Battle { get; set; }
    }
    public class Military
    {
        public Military()
        {
            Quotes = new List<Quote>();
            MilitaryBattles = new List<MilitaryBattle>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        public int? KingId { get; set; }
        public King King { get; set; }
        public List<MilitaryBattle> MilitaryBattles { get; set; }
        public Horse Horse { get; set; }
    }

    public class Battle
    {
        public Battle()
        {
            MilitaryBattles = new List<MilitaryBattle>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<MilitaryBattle> MilitaryBattles { get; set; }
    }

    public class King
    {
        public int Id { get; set; }
        public string KingName { get; set; }
    }

    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Military Military { get; set; }
        public int MilitaryId { get; set; }
    }

    public class Horse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MilitaryId { get; set; }
    }

}
