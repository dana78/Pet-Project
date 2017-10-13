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

            loginPage.Login();
        }

        [Test]
        public void BasicFlow()
        {
            //Clic en nueva mascota

        }
    }
}
