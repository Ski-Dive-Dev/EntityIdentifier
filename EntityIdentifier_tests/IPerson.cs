using SkiDiveDev.EntityIdentifier;

namespace SkiDiveDev.EntityIdentifier_tests
{
    public interface IPerson
    {
        int FavoriteNumber { get; set; }
        string GivenName { get; set; }
        IIdentifier ID { get; set; }
        string Surname { get; set; }

        string ToString();
    }
}