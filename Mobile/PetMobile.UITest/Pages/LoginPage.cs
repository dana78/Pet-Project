using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetMobile.UITest.Pages
{
    public class LoginPage : BasePage
    {
        private const string EmailEntry = "EmailEntry";
        private const string PasswordEntry = "PasswordEntry";
        private const string LoginButton = "LoginButton";

        public void Login()
        {
            app.EnterText(EmailEntry, "javier@mail.com");
            app.PressEnter();

            app.EnterText(PasswordEntry, "123123");
            app.PressEnter();

            app.Tap(LoginButton);

            app.Screenshot("Tapped on Ingresar");
            app.WaitForElement(x => x.Marked("OK"));
        }

    }
}
