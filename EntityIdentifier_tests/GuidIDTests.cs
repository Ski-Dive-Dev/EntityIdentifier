using System;
using NUnit.Framework;
using SkiDiveDev.EntityIdentifier;
using SkiDiveDev.EntityIdentifier_tests;

namespace EntityIdentifier_tests.GuidIDTests
{
    /// <summary>
    /// A Class which represents a <see cref="Person"/> entity's ID as a <see cref="Guid"/>.
    /// </summary>
    class PersonID : GuidIdentifier
    {
        public PersonID() : base()
        { /* No additional construction required. */ }

        public PersonID(Guid id) : base(id)
        { /* No additional construction required. */ }

        public PersonID(GuidIdentifier id) : base(id.ID)
        { /* No additional construction required. */ }

        public PersonID(string value, bool strict = true) : base(Parse(value).ID)
        { /* No additional construction required. */ }


        public static bool TryParse(string value, out PersonID entityID)
        {
            if (GuidIdentifier.TryParse(value, out var id))
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
    /// A Class which returns a newly generated <see cref="PersonID"/>.
    /// </summary>
    public class PersonIDFactory : IIdentifierFactory
    {
        // The following method is what you would normally want to use, when not testing:
        //public IIdentifier CreateID() => new PersonID(Guid.NewGuid());

        private static int _lastIDGenerated = 0;

        public IIdentifier CreateID()
        {
            _lastIDGenerated++;

            const string guidPrefix = "00000000-0000-0000-0000-";
            var manufacturedGuid = guidPrefix + _lastIDGenerated.ToString("D12");

            var guid = Guid.Parse(manufacturedGuid);

            return new PersonID(guid);
        }
    }


    public class GuidIDTests
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
                ID = new PersonID(Guid.Parse("00000000-0000-0000-0000-000000000001"))
            };

            entityIDTests = new EntityIDTests<PersonIDFactory>(idFactory, personWithPresetID);
            entityIDTests.SetUp();
        }


        [Test]
        public void ShouldCreateID()
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
            entityIDTests.ShouldReturnIDAsString(personWithPresetID, "00000000-0000-0000-0000-000000000001");
        }

        [Test]
        public void ShouldCreateEntityIDFromString()
        {
            // arrange
            var guid = Guid.NewGuid();
            var expected = new PersonID(guid);

            var idToParse = guid.ToString();

            // act
            var actual = new PersonID(idToParse);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GuidIDEqualityTests()
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