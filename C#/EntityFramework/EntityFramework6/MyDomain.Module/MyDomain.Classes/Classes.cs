using System.Collections.Generic;
using MyDomain.Classes.Enums;

namespace MyDomain.Classes
{
    public class Ninja
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Served { get; set; }
        public Clan Clan { get; set; }
        public int ClanId { get; set; }
        public List<Equipment> EquipmentOwned { get; set; }
    }

    public class Clan
    {
        public int Id { get; set; }
        public string ClanName { get; set; }
        public List<Ninja> Ninjas { get; set; }
    }

    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DomainTypes.EquipmentType Type { get; set; }
        public Ninja Ninja { get; set; }
    }
}