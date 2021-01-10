using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SkiDiveDev.EntityIdentifier;
using SkiDiveDev.EntityIdentifier_tests;

namespace EntityIdentifier_tests
{
    /// <summary>
    /// This class tests different types of IDs for a <see cref="Person"/> class.  The <see cref="Person.ID"/>
    /// property is of type <see cref="IIdentifier"/>, whose underlying type is governed by the injected
    /// <see cref="IIdentifierFactory"/>.
    /// </summary>
    /// <typeparam name="PersonIDFactory">The unit tests require the ability to generate IDs of the type given
    /// by the <see cref="PersonIDFactory"/>.  However, in normal use, the given type would actually be
    /// <see cref="IIdentifier"/>.</typeparam>
    public class EntityIDTests<PersonIDFactory> where PersonIDFactory : IIdentifierFactory
    {
        protected PersonIDFactory idFactory;

        protected string firstFactoryGeneratedIDAsString;
        protected string validIDAsString;
        protected string invalidIDAsString;

        Person p1;
        Person p2;
        Person p3;
        protected IPerson personWithPresetID;


        public void CommonSetUp()
        {
            p1 = new Person()
            {
                ID = idFactory.CreateID(),
                GivenName = "Derrick",
                Surname = "Lee",
                FavoriteNumber = 46
            };
            p2 = new Person()
            {
                ID = idFactory.CreateID(),
                GivenName = "Gordan",
                Surname = "Lee",
                FavoriteNumber = 9
            };
            p3 = new Person()
            {
                ID = idFactory.CreateID(),
                GivenName = "Julian",
                Surname = "Lee",
                FavoriteNumber = 8
            };
        }

        [Test]
        public void ShouldAssignIDWithoutChange()
        {
            // arrange
            personWithPresetID.ID = p1.ID;

            // act
            var idAsExpected = p1.ID.Equals(personWithPresetID.ID);

            // assert
            Assert.IsTrue(idAsExpected);
        }

        [Test]
        public void TestIDIsAssigned()
        {
            var person = new Person
            {
                ID = idFactory.CreateID()
            };

            Assert.IsTrue(person.ID.IDIsAssigned);
        }

        [Test]
        public void TestIDIsNotAssigned()
        {
            var person = new Person();

            Assert.IsFalse(person.ID?.IDIsAssigned ?? false);  // ID can be null!
        }


        [Test]
        public void ShouldReturnIDAsString()
        {
            // arrange
            var expected = firstFactoryGeneratedIDAsString;

            // act
            var actual = personWithPresetID.ID.ToString();

            // assert
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void IDEqualityTests()
        {
            // act
            var p1_p1 = p1.ID.Equals(p1.ID);

            var p1_p2 = p1.ID.Equals(p2.ID);

            var p1_p3 = p1.ID.Equals(p3.ID);

            // assert
            Assert.IsTrue(p1_p1);

            Assert.IsFalse(p1_p2);

            Assert.IsFalse(p1_p3);
        }

        [Test]
        public void ShouldSortEntitiesAscendingByID()
        {
            // arrange
            var expectedList = new List<Person> { p1, p2, p3 };
            var people = new List<Person> { p2, p3, p1 };

            // act
            var ascendingList = people.OrderBy(p => p.ID);

            // assert
            CollectionAssert.AreEqual(expectedList, ascendingList);
        }


        [Test]
        public void ShouldSortEntitiesDescendingbyID()
        {
            // arrange
            var expectedList = new List<Person> { p3, p2, p1 };
            var people = new List<Person> { p2, p3, p1 };

            // act
            var ascendingList = people.OrderByDescending(p => p.ID);

            // assert
            CollectionAssert.AreEqual(expectedList, ascendingList);
        }
    }
}
