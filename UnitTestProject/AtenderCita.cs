using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace UnitTestProject
{
    [TestClass]
    public class AtenderCita
    {
        IWebDriver driver = new ChromeDriver();

        [TestMethod]
        public void FlujoBasico()
        {
            try
            {
                driver.Navigate().GoToUrl("http://petprojectwebapp.azurewebsites.net/User/Login");

                IWebElement input1 = driver.FindElement(By.Id("Email"));
                input1.Clear();
                input1.SendKeys("ramon@mail.com");
                IWebElement input2 = driver.FindElement(By.Id("Password"));
                input2.Clear();
                input2.SendKeys("123456");
            

               IWebElement btnIniciaSesion = driver.FindElement(By.Id("btnInciarSesion"));
               btnIniciaSesion.Click();
               Thread.Sleep(2000);

            //    IWebElement opcion = driver.FindElement(By.XPath("//*[@id=\"frmMenu:j_idt18\"]/ul/li[2]/a"));
            //    opcion.Click();
            //    Thread.Sleep(2000);


            //    IWebElement btnNuevo = driver.FindElement(By.Id("btnNuevo"));
            //    btnNuevo.Click();

            //    IWebElement input3 = driver.FindElement(By.Id("txtNombre"));
            //    input3.Clear();
            //    input3.SendKeys("Categoria Prueba");
            //    Thread.Sleep(2000);

            //    IWebElement btnGuardar = driver.FindElement(By.Id("btnGuardar"));
            //    btnGuardar.Click();
            //    Thread.Sleep(2000);

            //    string mensajeObtenido = driver.FindElement(By.Id("messages")).Text;
            //    Assert.AreEqual(mensajeEsperado, mensajeObtenido);
            //    driver.Close();
            }
            catch (Exception e)
            {
                Assert.Fail();

            }
        }
    }
}
