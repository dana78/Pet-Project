using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RegistrarVeterinario
{
    [TestClass]
    public class RegistrarVeterinario
    {
        IWebDriver driver = new ChromeDriver();

        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void registrarVeterinario_FlujoBasico()
        {
            try
            {
                string mensajeEsperado = "Usuario creado";
                driver.Navigate().GoToUrl("http://petprojectwebapp.azurewebsites.net/User/Register");

                IWebElement input1 = driver.FindElement(By.Name("txtNombre"));
                input1.Clear();
                input1.SendKeys("Ramon");

                IWebElement input2 = driver.FindElement(By.Name("txtApellidos"));
                input2.Clear();
                input2.SendKeys("Ramon");

                IWebElement input3 = driver.FindElement(By.Name("txtNombre"));
                input3.Clear();
                input3.SendKeys("Diaz Sevilla");

                IWebElement input4 = driver.FindElement(By.("txtNombre"));
                input4.Clear();
                input4.SendKeys("Ramon");
            }
        }

    }
}
