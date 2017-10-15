namespace PetMobile.UITest.Pages
{
    public class PetFormPage : BasePage
    {
        private const string NameTxt = "PetName";
        private const string BreedTxt = "PetBreed";
        private const string SendButton = "SendPetButton";

        public void FillName(string name = "Norman")
        {
            app.EnterText(NameTxt, name);
            app.PressEnter();                        
        }

        public void FillBreed(string breed = "Puddle")
        {
            app.EnterText(BreedTxt, breed);
            app.PressEnter();
        }

        public void ClickSend()
        {
            app.Tap(SendButton);
        }
    }
}
