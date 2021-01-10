using System;
using NUnit.Framework;
using SkiDiveDev.EntityIdentifier;
using SkiDiveDev.EntityIdentifier_tests;

namespace EntityIdentifier_tests.StringIDTests
{
    /// <summary>
    /// A Class which represents a <see cref="Person"/> entity's ID as a <see langref="string"/>.
    /// </summary>
    class PersonID : StringIdentifier
    {
        public PersonID() : base()
        { /* No additional construction required. */ }

        public PersonID(string id) : base(id)
        { /* No additional construction required. */ }

        public PersonID(StringIdentifier id) : base(id.ID)
        { /* No additional construction required. */ }


        public static bool TryParse(string value, out PersonID entityID)
        {
            if (StringIdentifier.TryParse(value, out var id))
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
            return new PersonID(_lastIDGenerated.ToString());
        }
    }


    public class StringIDTests
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
                ID = new PersonID("1")
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
        public void ShouldSortEntitiesDescendingbyID()
        {
            entityIDTests.ShouldSortEntitiesDescendingbyID();
        }
    }
}