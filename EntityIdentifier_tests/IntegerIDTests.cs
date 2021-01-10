using NUnit.Framework;
using SkiDiveDev.EntityIdentifier;
using SkiDiveDev.EntityIdentifier_tests;


namespace EntityIdentifier_tests.IntegerIDTests
{
    /// <summary>
    /// A Class which represents a <see cref="Person"/> entity's ID as an <see langref="int"/>.
    /// </summary>
    class PersonID : IntIdentifier
    {
        public PersonID() : base()
        { /* No additional construction required. */ }

        public PersonID(int id) : base(id)
        { /* No additional construction required. */ }

        public PersonID(IntIdentifier id) : base(id.ID)
        { /* No additional construction required. */ }

        public PersonID(string value) : base(Parse(value).ID)
        { /* No additional construction required. */ }


        public static bool TryParse(string value, out PersonID entityID)
        {
            if (IntIdentifier.TryParse(value, out var id))
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
        private static int _lastIDGenerated = 0;

        public IIdentifier CreateID()
        {
            _lastIDGenerated++;
            return new PersonID(_lastIDGenerated);
        }
    }


    public class IntIDTests
    {
        readonly PersonIDFactory idFactory = new PersonIDFactory();
        EntityIDTests<PersonIDFactory> entityIDTests;

        Person personWithPresetID;

        [SetUp]
        public void SetUp()
        {
            personWithPresetID = new Person
            {
                GivenName = "Person whose ID",
                Surname = "Not tied to injected factory",
                ID = new PersonID(1) // We can do this, because we know the first Factory-created ID is 1.
            };

            entityIDTests = new EntityIDTests<PersonIDFactory>(idFactory, personWithPresetID);
            entityIDTests.SetUp();
        }

        [Test]
        public void ShouldAssignIDWithoutChange()
        {
            entityIDTests.ShouldAssignIDWithoutChange();
        }

        [Test]
        public void TestIDIsNotAssigned()
        {
            entityIDTests.TestIDIsNotAssigned();
        }

        [Test]
        public void TestIDIsAssigned()
        {
            entityIDTests.TestIDIsAssigned();
        }

        [Test]
        public void ShouldReturnIDAsString()
        {
            entityIDTests.ShouldReturnIDAsString(personWithPresetID, "1");
        }

        [Test]
        public void ShouldCreateEntityIDFromString()
        {
            // arrange
            var expected = new PersonID(12345678);

            var idToParse = "12345678";

            // act
            var actual = new PersonID(idToParse);

            // assert
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void IDEqualityTests()
        {
            entityIDTests.IDEqualityTests();
        }

        [Test]
        public void ShouldSortEntitiesAscendingByID()
        {
            entityIDTests.ShouldSortEntitiesAscendingByID();
        }

        [Test]
        public void ShouldSortEntitiesDescendingByID()
        {
            entityIDTests.ShouldSortEntitiesDescendingbyID();
        }
    }
}