using System;

namespace PetMobile.UITest.Pages
{
    public class AppointmentPage : BasePage
    {
        private const string AppointmentTitleEntry = "AppointmentTitleEntry";
        private const string AppointmentNextButton = "AppointmentNextButton";

        public void EnterAppointmentTitle(string title)
        {
            app.EnterText(AppointmentTitleEntry, title);
            app.PressEnter();
        }

        public void TapNextButton()
        {
            app.Tap(AppointmentNextButton);
        }

        public void AssertNoTitleMessageIsShown()
        {
            app.WaitForElement("Debes ingresar un título (motivo) a tu cita.");
        }
    }
}
