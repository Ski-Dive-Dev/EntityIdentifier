using NUnit.Framework;
using SkiDiveDev.EntityIdentifier;
using SkiDiveDev.EntityIdentifier_tests;


namespace EntityIdentifier_tests.UlongIDTests
{
    /// <summary>
    /// A Class which represents a <see cref="Person"/> entity's ID as an <see langref="ulong"/>.
    /// </summary>
    class PersonID : UlongIdentifier
    {
        public PersonID() : base()
        { /* No additional construction required. */ }

        public PersonID(ulong id) : base(id)
        { /* No additional construction required. */ }

        public PersonID(UlongIdentifier id) : base(id.ID)
        { /* No additional construction required. */ }

        public PersonID(string value) : base(Parse(value).ID)
        { /* No additional construction required. */ }


        public static bool TryParse(string value, out PersonID entityID)
        {
            if (UlongIdentifier.TryParse(value, out var id))
            {
                entityID = new PersonID(id);
                return true;
            }
            else
            {
                entityID = new PersonID();
                return false;
            }
        }
    }


    /// <summary>
    /// A Class which returns the next available <see cref="PersonID"/>.
    /// </summary>
    public class PersonIDFactory : IIdentifierFactory
    {
        private static ulong _lastIDGenerated = 0;

        public IIdentifier CreateID()
        {
            _lastIDGenerated++;
            return new PersonID(_lastIDGenerated);
        }
    }


    public class ULongIDTests : EntityIDTests<PersonIDFactory>
    {
        private const int validID = 12345678;
        private const int firstFactoryGeneratedID = 1;

        [SetUp]
        public void SetUp()
        {
            idFactory = new PersonIDFactory();
            firstFactoryGeneratedIDAsString = firstFactoryGeneratedID.ToString();
            validIDAsString = validID.ToString();
            invalidIDAsString = "x123";

            personWithPresetID = new Person
            {
                GivenName = "Person whose ID",
                Surname = "Not tied to injected factory",
                ID = new PersonID(firstFactoryGeneratedID)
            };

            CommonSetUp();
        }


        [Test]
        public void ShouldCreateEntityIDFromString()
        {
            // arrange
            var expected = new PersonID(validID);

            var idToParse = validIDAsString;

            // act
            var actual = new PersonID(idToParse);

            // assert
            Assert.AreEqual(expected, actual);
        }

    }
}