using PLINQ.Models;

namespace PLINQ.Services
{
    public static class CityService
    {
        public static City[] cities = new[] {
            new City { Id = 1,  Name = "Turku"  , Country = "Finland" },
            new City { Id = 2,  Name = "Paris"  , Country = "France" },
            new City { Id = 3,  Name = "Oslo"    ,  Country = "Norway" } ,
            new City { Id = 4,  Name = "Helsinki"     , Country = "Finland" },

            new City { Id = 5,  Name = "Turku"  , Country = "Finland" },
            new City { Id = 6,  Name = "Paris"  , Country = "France" },
            new City { Id = 7,  Name = "Oslo"    ,  Country = "Norway" } ,
            new City { Id = 8,  Name = "Helsinki"     , Country = "Finland" } ,

            new City { Id = 9,  Name = "Turku"  , Country = "Finland" },
            new City { Id = 10,  Name = "Paris"  , Country = "France" },
            new City { Id = 11,  Name = "Oslo"    ,  Country = "Norway" } ,
            new City { Id = 12,  Name = "Helsinki"     , Country = "Finland"},

            new City { Id = 13,  Name = "Turku"  , Country = "Finland" },
            new City { Id = 14,  Name = "Paris"  , Country = "France" },
            new City { Id = 15,  Name = "Oslo"    ,  Country = "Norway" } ,
            new City { Id = 16,  Name = "Helsinki"     , Country = "Finland"},

            new City { Id = 17,  Name = "Turku"  , Country = "Finland" },
            new City { Id = 18,  Name = "Paris"  , Country = "France" },
            new City { Id = 19,  Name = "Oslo"    ,  Country = "Norway" } ,
            new City { Id = 20,  Name = "Helsinki"     , Country = "Finland"}
        };

        public static City[] GetCities() => cities;
    }
}