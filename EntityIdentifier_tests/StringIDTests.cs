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


    public class StringIDTests : EntityIDTests<PersonIDFactory>
    {
        private const string validID = "12345678";
        private const string firstFactoryGeneratedID = "1";

        [SetUp]
        public void SetUp()
        {
            idFactory = new PersonIDFactory();
            firstFactoryGeneratedIDAsString = firstFactoryGeneratedID;
            validIDAsString = validID;
            invalidIDAsString = null;

            personWithPresetID = new Person
            {
                GivenName = "Person whose ID",
                Surname = "Not tied to injected factory",
                ID = new PersonID(firstFactoryGeneratedID)
            };

            CommonSetUp();
        }

        [Test]
        public void ShouldReturnFalseForTryParse()
        {
            // arrange
            var idToParse = invalidIDAsString;

            // act
            var actual = PersonID.TryParse(idToParse, out PersonID entityID);

            // assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ShouldReturnTrueForTryParse()
        {
            // arrange
            var idToParse = validIDAsString;

            // act
            var actual = PersonID.TryParse(idToParse, out PersonID entityID);

            // assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ShouldReturnEntityIdentifierForTryParse()
        {
            // arrange
            var idToParse = validIDAsString;
            var expected = new PersonID(idToParse);

            // act
            var success = PersonID.TryParse(idToParse, out PersonID actual);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldReturnDefaultEntityIdentifierForTryParse()
        {
            // arrange
            var idToParse = invalidIDAsString;
            var expected = new PersonID();

            // act
            var success = PersonID.TryParse(idToParse, out PersonID actual);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}