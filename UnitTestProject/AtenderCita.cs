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
        public void FlujoNormal()
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

                IWebElement btnIniciaSesion = driver.FindElement(By.Id("btnIniciarSesion"));
                btnIniciaSesion.Click();
                Thread.Sleep(2000);

                IWebElement opcion = driver.FindElement(By.Id("Atender"));
                opcion.Click();
                Thread.Sleep(2000);

                IWebElement opcion2 = driver.FindElement(By.Id("btnRegistrarAtencion"));
                opcion2.Click();
                Thread.Sleep(2000);


                IWebElement input3 = driver.FindElement(By.Id("ClinicalSign"));
                input3.Clear();
                input3.SendKeys("Dolor de Pata");
                Thread.Sleep(500);

                IWebElement input4 = driver.FindElement(By.Id("Anamnesis"));
                input4.Clear();
                input4.SendKeys("¿Qué síntomas presenta?");
                Thread.Sleep(500);

                IWebElement input5 = driver.FindElement(By.Id("Diagnostic"));
                input5.Clear();
                input5.SendKeys("Pata Rota");
                Thread.Sleep(500);

                IWebElement input6 = driver.FindElement(By.Id("Treatment"));
                input6.Clear();
                input6.SendKeys("Operación, reposo y medicación");
                Thread.Sleep(500);

                IWebElement input7 = driver.FindElement(By.Id("Observations"));
                input7.Clear();
                input7.SendKeys("No Presenta");
                Thread.Sleep(500);

                IWebElement opcion3 = driver.FindElement(By.Id("btnGuardar"));
                opcion3.Click();
                Thread.Sleep(2000);



            }
            catch (Exception e)
            {
                Assert.Fail();

            }
        }

        [TestMethod]
        public void FlujoAlternativo01()
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

                IWebElement btnIniciaSesion = driver.FindElement(By.Id("btnIniciarSesion"));
                btnIniciaSesion.Click();
                Thread.Sleep(2000);

                IWebElement opcion = driver.FindElement(By.Id("Atender"));
                opcion.Click();
                Thread.Sleep(2000);

                IWebElement opcion2 = driver.FindElement(By.Id("btnRegistrarAtencion"));
                opcion2.Click();
                Thread.Sleep(2000);


                IWebElement input3 = driver.FindElement(By.Id("ClinicalSign"));
                input3.Clear();
                input3.SendKeys("Dolor de Pata");
                Thread.Sleep(500);

                IWebElement input4 = driver.FindElement(By.Id("Anamnesis"));
                input4.Clear();
                input4.SendKeys("¿Qué síntomas presenta?");
                Thread.Sleep(500);

                //IWebElement input5 = driver.FindElement(By.Id("Diagnostic"));
                //input5.Clear();
                //input5.SendKeys("Pata Rota");
                //Thread.Sleep(500);

                IWebElement input6 = driver.FindElement(By.Id("Treatment"));
                input6.Clear();
                input6.SendKeys("Operación, reposo y medicación");
                Thread.Sleep(500);

                IWebElement input7 = driver.FindElement(By.Id("Observations"));
                input7.Clear();
                input7.SendKeys("No Presenta");
                Thread.Sleep(500);

                IWebElement opcion3 = driver.FindElement(By.Id("btnGuardar"));
                opcion3.Click();
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                Assert.Fail();

            }
        }

        [TestMethod]
        public void FlujoAlternativo02()
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

                IWebElement btnIniciaSesion = driver.FindElement(By.Id("btnIniciarSesion"));
                btnIniciaSesion.Click();
                Thread.Sleep(2000);

                IWebElement opcion = driver.FindElement(By.Id("Atender"));
                opcion.Click();
                Thread.Sleep(2000);

                IWebElement opcion2 = driver.FindElement(By.Id("btnRegistrarAtencion"));
                opcion2.Click();
                Thread.Sleep(2000);


                IWebElement input3 = driver.FindElement(By.Id("ClinicalSign"));
                input3.Clear();
                input3.SendKeys("Dolor de Pata");
                Thread.Sleep(500);

                IWebElement input4 = driver.FindElement(By.Id("Anamnesis"));
                input4.Clear();
                input4.SendKeys("¿Qué síntomas presenta?");
                Thread.Sleep(500);

                IWebElement input5 = driver.FindElement(By.Id("Diagnostic"));
                input5.Clear();
                input5.SendKeys("Pata Rota");
                Thread.Sleep(500);

                IWebElement input6 = driver.FindElement(By.Id("Treatment"));
                input6.Clear();
                input6.SendKeys("Operación, reposo y medicación");
                Thread.Sleep(500);

                IWebElement input7 = driver.FindElement(By.Id("Observations"));
                input7.Clear();
                input7.SendKeys("No Presenta");
                Thread.Sleep(500);

                IWebElement opcion3 = driver.FindElement(By.Id("Regresar"));
                opcion3.Click();
                Thread.Sleep(2000);

            }
            catch (Exception e)
            {
                Assert.Fail();

            }
        }
    }
}
