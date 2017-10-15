using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetMobile.ViewModels;

namespace PetMobile.UnitTests
{
    [TestClass]
    public class AppointmentTests
    {
        AppointmentViewModel vm;

        [TestInitialize]
        public void TestInitialize()
        {
            vm = new AppointmentViewModel();
        }

        [TestMethod]
        public void ValidWhenNotEmptyTitle()
        {
            var notEmptyTitle = "Pata rota";
            vm.Appointment.Title = notEmptyTitle;

            var isValid = vm.ValidateTitle();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void InvalidWhenSpacesOnlyTitle()
        {
            var spacesOnlyTitle = "   ";
            vm.Appointment.Title = spacesOnlyTitle;

            var isValid = vm.ValidateTitle();

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void InvalidWhenNullTitle()
        {
            string nullTitle = null;
            vm.Appointment.Title = nullTitle;

            var isValid = vm.ValidateTitle();

            Assert.AreEqual(false, isValid);
        }
    }

}
