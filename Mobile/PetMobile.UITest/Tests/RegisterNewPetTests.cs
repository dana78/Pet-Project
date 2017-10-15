using System;
using NUnit.Framework;
using PetMobile.UITest.Pages;
using Xamarin.UITest;

namespace PetMobile.UITest.Tests
{
    [TestFixture(Platform.Android)]
    public class RegisterNewPetTests
    {
        IApp app;
        Platform platform;

        LoginPage loginPage;
        DashboardPage dashboardPage;
        PetFormPage petFormPage;

        public RegisterNewPetTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            loginPage = new LoginPage();
            dashboardPage = new DashboardPage();
            petFormPage = new PetFormPage();

            loginPage.Login();
        }

        [Test]
        public void BasicFlow()
        {
            //  Click new pet form
            dashboardPage.SelectToolbarItem();

            //  Fill all fields
            petFormPage.FillName("Terry");
            petFormPage.FillBreed();
            petFormPage.ClickSend();

            app.WaitForElement(x => x.Id("message"));
            var result = app.Query("¡Excelente! Tu mascota ha sido registrada exitosamente.");
            app.Screenshot("Registro exitoso");
            Assert.NotZero(result.Length);
        }

        [Test]
        public void AlternativeFlow1()
        {
            dashboardPage.SelectToolbarItem();

            petFormPage.FillBreed();
            petFormPage.ClickSend();

            app.WaitForElement(x => x.Id("message"));
            var result = app.Query("Debes ponerle un nombre a tu mascota");
            app.Screenshot("Mensaje de error de registro");
            Assert.NotZero(result.Length);
        }

        [Test]
        public void AlternativeFlow2()
        {
            dashboardPage.SelectToolbarItem();
            app.WaitForElement("FormTitle");
            app.Back();
            app.WaitForElement("Inicio");
            app.Screenshot("Regreso a dashboard");
        }

    }
}
