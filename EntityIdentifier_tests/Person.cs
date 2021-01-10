using System;
using SkiDiveDev.EntityIdentifier;

namespace SkiDiveDev.EntityIdentifier_tests
{
    public class Person : IPerson
    {
        public IIdentifier ID { get; set; }

        public string GivenName { get; set; }
        public string Surname { get; set; }
        public int FavoriteNumber { get; set; }


        public override string ToString()
        {
            return $"id: {ID} / {GivenName} {Surname}; FavNum: {FavoriteNumber}";
        }
    }
}
