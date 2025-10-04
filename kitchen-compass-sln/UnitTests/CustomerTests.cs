using CustomerBackend;
using NUnit;
using Newtonsoft;

namespace UnitTests
{
    public class BackendTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_MenuItemWithNoModifications_When_AddModificationCalled_Then_ModificationIsAddedToItemInstance()
        {
            //Given
            MenuItem item = new MenuItem("id1");

            //When
            item.AddModification("No Mushrooms");

            //Then
            Assert.That(item.Modifications.Count == 1);
            Assert.That(item.Modifications["No Mushrooms"] == false);
        }
    }
}


