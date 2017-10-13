using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetMobile.ViewModels;

namespace PetMobile.UnitTests
{
    [TestClass]
    public class PetRegisterTests
    {
        PetEditViewModel vm;

        [TestInitialize]
        public void TestInitialize()
        {
            vm = new PetEditViewModel(null, null, null);
        }

        [TestMethod]
        public void OKWhenAllFieldsAreNotEmptyAndCorrectlyFormatted()
        {
            vm.Pet.Firstname = "Puchy";
            vm.Pet.Lastname = "Escobar";
            vm.Pet.Breed = "Poodle";
            vm.Pet.Color = "Negro";

            var valid = vm.ValidateModel(out string message);
            Assert.AreEqual(true, valid);
        }

        [TestMethod]
        public void OKWhenNullOrEmptyColor()
        {
            vm.Pet.Firstname = "Puchy";
            vm.Pet.Lastname = "Escobar";
            vm.Pet.Breed = "Poodle";
            vm.Pet.Color = string.Empty;

            var valid = vm.ValidateModel(out string message);
            Assert.AreEqual(true, valid);
        }

        [TestMethod]
        public void OKWhenNullOrEmptyLastname()
        {
            vm.Pet.Firstname = "Puchy";
            vm.Pet.Lastname = string.Empty;
            vm.Pet.Breed = "Poodle";
            vm.Pet.Color = "Negro";

            var valid = vm.ValidateModel(out string message);
            Assert.AreEqual(true, valid);
        }

        [TestMethod]
        public void OKWhenNullOrEmptyColorAndLastname()
        {
            vm.Pet.Firstname = "Puchy";
            vm.Pet.Lastname = string.Empty;
            vm.Pet.Breed = "Poodle";
            vm.Pet.Color = string.Empty;

            var valid = vm.ValidateModel(out string message);
            Assert.AreEqual(true, valid);
        }

        [TestMethod]
        public void InvalidWhenNullOrEmptyFirstname()
        {
            vm.Pet.Firstname = string.Empty;
            vm.Pet.Lastname = "Escobar";
            vm.Pet.Breed = "Poodle";
            vm.Pet.Color = "Negro";

            var valid = vm.ValidateModel(out string message);
            Assert.AreEqual(false, valid);
        }

        [TestMethod]
        public void InvalidWhenNullOrEmptyBreed()
        {
            vm.Pet.Firstname = "Puchy";
            vm.Pet.Lastname = "Escobar";
            vm.Pet.Breed = string.Empty;
            vm.Pet.Color = "123";

            var valid = vm.ValidateModel(out string message);
            Assert.AreEqual(false, valid);
        }

        [TestMethod]
        public void InvalidWhenNonAlphabeticColor()
        {
            vm.Pet.Firstname = "Puchy";
            vm.Pet.Lastname = "Escobar";
            vm.Pet.Breed = "Poodle";
            vm.Pet.Color = "123";

            var valid = vm.ValidateModel(out string message);
            Assert.AreEqual(false, valid);
        }

        [TestMethod]
        public void InvalidWhenNonAlphabeticBreed()
        {
            vm.Pet.Firstname = "Puchy";
            vm.Pet.Lastname = "Escobar";
            vm.Pet.Breed = "123";
            vm.Pet.Color = "Negro";

            var valid = vm.ValidateModel(out string message);
            Assert.AreEqual(false, valid);
        }
    }

}
