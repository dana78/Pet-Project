namespace PetMobile.UITest.Pages
{
    public class DashboardPage : BasePage
    {
        private const string NewAppointmentButton = "Nueva cita";

        public void GoToPageAppointment()
        {
            app.Tap(x => x.Text("Techie Escobar"));
            app.Tap(NewAppointmentButton);
        }
    }
}
