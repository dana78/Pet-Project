using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using PetMobile.UITest.Pages;

namespace PetMobile.UITest.Tests
{
    [TestFixture(Platform.Android)]
    public class RegisterNewAppointmentTests
    {
        IApp app;
        Platform platform;

        LoginPage loginPage;
        DashboardPage dashboardPage;
        AppointmentPage appointmentPage;

        public RegisterNewAppointmentTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

            loginPage = new LoginPage();
            dashboardPage = new DashboardPage();
            appointmentPage = new AppointmentPage();

            loginPage.Login();
        }

        [Test]
        public void BasicFlow()
        {
            dashboardPage.GoToPageAppointment();

            appointmentPage.EnterAppointmentTitle("Pata rota");
            appointmentPage.TapNextButton();
            app.Screenshot("Cita luego de dar clic en Siguiente");

            app.WaitForElement(x => x.Text("Selección de veterinario"));
            app.Tap(x => x.Text("Ramon Díaz"));
            app.WaitForElement(x => x.Id("button1"));
            app.Tap(x => x.Id("button1"));
            app.Screenshot("Luego de dar tap a SI");
            app.WaitForElement(x => x.Id("alertTitle"));

            app.Tap(x => x.Id("button2"));
            app.WaitForElement(x => x.Marked("OK"));
        }        

        [Test]
        public void AlternativeFlow1()
        {
            dashboardPage.GoToPageAppointment();
            appointmentPage.TapNextButton();

            app.Screenshot("Despues de dar Tap a Siguiente");
            appointmentPage.AssertNoTitleMessageIsShown();
        }

    }
}

