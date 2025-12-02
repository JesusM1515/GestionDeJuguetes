using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;

namespace Pruebas
{
    [TestFixture]
    public class TestQA
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private const string urlBase = "http://localhost:5043";

        private static ExtentReports extent;
        private ExtentTest test;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            string pathReporte = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ReporteQA", "Reporte.html");

            Directory.CreateDirectory(Path.GetDirectoryName(pathReporte));

            var sparkReporter = new ExtentSparkReporter(pathReporte);

            sparkReporter.Config.DocumentTitle = "Reporte de Pruebas Automatizadas";
            sparkReporter.Config.ReportName = "Reporte QA Juguetes";
            sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;

            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);

            extent.AddSystemInfo("Sistema Operativo", Environment.OSVersion.ToString());
            extent.AddSystemInfo("Navegador", "Edge");
            extent.AddSystemInfo("Selenium Version", "4.x");
        }

        [SetUp]
        public void Setup()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            var options = new EdgeOptions();
            driver = new EdgeDriver(options);

            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Login_UsuarioCorrecto_RedireccionaHome()
        {
            driver.Navigate().GoToUrl(urlBase);
            wait.Until(d => d.FindElement(By.Id("Correo")).Displayed);

            driver.FindElement(By.Id("Correo")).SendKeys("ADMIN@juguetes.com");
            driver.FindElement(By.Id("Password")).SendKeys("admin000");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            wait.Until(d => d.Url.Contains("/Home"));
            Assert.IsTrue(driver.Url.Contains("/Home"));
        }

        [Test]
        public void Login_Incorrecto_MuestraError()
        {
            driver.Navigate().GoToUrl($"{urlBase}/Login");
            driver.FindElement(By.Id("Correo")).SendKeys("mal@correo.com");
            driver.FindElement(By.Id("Password")).SendKeys("PasswordIncorrecto");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            wait.Until(d => d.FindElement(By.CssSelector(".alert-danger")));
            Assert.Pass("Error mostrado correctamente");
        }

        [Test]
        public void CrearJuguete_Valido_AgregaCorrectamente()
        {
            driver.Navigate().GoToUrl($"{urlBase}/EJuguetes/Create");
            wait.Until(d => d.FindElement(By.Id("Nombre")).Displayed);

            driver.FindElement(By.Id("ID_Juguete")).SendKeys("Prueba");
            driver.FindElement(By.Id("Nombre")).SendKeys("OsoMolon");
            driver.FindElement(By.Id("Categoria")).SendKeys("Recreativo");
            driver.FindElement(By.Id("Tipo")).SendKeys("Peluche");
            driver.FindElement(By.Id("Precio")).SendKeys("10000");
            driver.FindElement(By.Id("Stock")).SendKeys("10");
            driver.FindElement(By.Id("Edad")).SendKeys("7");
            driver.FindElement(By.Id("Descripcion")).SendKeys("OsoMolon el mejor");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            wait.Until(d => d.Url.Contains("/EJuguetes"));
            Assert.IsTrue(driver.Url.Contains("/EJuguetes"));
        }

        [Test]
        public void CrearJuguete_Invalido_MuestraError()
        {
            driver.Navigate().GoToUrl($"{urlBase}/EJuguetes/Create");
            wait.Until(d => d.FindElement(By.Id("Nombre")).Displayed);

            driver.FindElement(By.Id("ID_Juguete")).SendKeys("");
            driver.FindElement(By.Id("Nombre")).SendKeys("");
            driver.FindElement(By.Id("Categoria")).SendKeys("");
            driver.FindElement(By.Id("Tipo")).SendKeys("");
            driver.FindElement(By.Id("Precio")).SendKeys("");
            driver.FindElement(By.Id("Stock")).SendKeys("");
            driver.FindElement(By.Id("Edad")).SendKeys("");
            driver.FindElement(By.Id("Descripcion")).SendKeys("");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            wait.Until(d => d.FindElement(By.CssSelector(".validation-summary-errors, .text-danger")));
            Assert.Pass("Se mostro el mensaje de error correctamente");
        }

        [Test]
        public void EditarJuguete_Valido_GuardaCorrectamente()
        {
            driver.Navigate().GoToUrl($"{urlBase}/EJuguetes/Edit/3");
            wait.Until(d => d.FindElement(By.Id("Nombre")).Displayed);

            driver.FindElement(By.Id("Nombre")).Clear();
            driver.FindElement(By.Id("Categoria")).Clear();
            driver.FindElement(By.Id("Tipo")).Clear();
            driver.FindElement(By.Id("Precio")).Clear();
            driver.FindElement(By.Id("Stock")).Clear();
            driver.FindElement(By.Id("Edad")).Clear();
            driver.FindElement(By.Id("Descripcion")).Clear();

            driver.FindElement(By.Id("Nombre")).SendKeys("JugueteEditado");
            driver.FindElement(By.Id("Categoria")).SendKeys("CategoriaEditada");
            driver.FindElement(By.Id("Tipo")).SendKeys("TipoNuevo");
            driver.FindElement(By.Id("Precio")).SendKeys("500");
            driver.FindElement(By.Id("Stock")).SendKeys("20");
            driver.FindElement(By.Id("Edad")).SendKeys("8");
            driver.FindElement(By.Id("Descripcion")).SendKeys("Descripcion actualizada");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            wait.Until(d => d.Url.Contains("/EJuguetes"));
            Assert.IsTrue(driver.Url.Contains("/EJuguetes"));
        }

        [Test]
        public void EditarJuguete_Invalido_MuestraErrores()
        {
            driver.Navigate().GoToUrl($"{urlBase}/EJuguetes/Edit/3");
            wait.Until(d => d.FindElement(By.Id("Nombre")).Displayed);

            driver.FindElement(By.Id("Nombre")).Clear();
            driver.FindElement(By.Id("Categoria")).Clear();
            driver.FindElement(By.Id("Tipo")).Clear();
            driver.FindElement(By.Id("Precio")).Clear();
            driver.FindElement(By.Id("Stock")).Clear();
            driver.FindElement(By.Id("Edad")).Clear();
            driver.FindElement(By.Id("Descripcion")).Clear();

            driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            wait.Until(d => d.FindElement(By.CssSelector(".text-danger, .validation-summary-errors")));
            Assert.Pass("Los mensajes de error se mostraron correctamente");
        }

        [Test]
        public void EliminarJuguete_Valido_EliminaCorrectamente()
        {
            driver.Navigate().GoToUrl($"{urlBase}/EJuguetes/Delete/10");
            wait.Until(d => d.FindElement(By.CssSelector("input[value='Delete']")).Displayed);

            driver.FindElement(By.CssSelector("input[value='Delete']")).Click();

            wait.Until(d => d.Url.Contains("/EJuguetes"));
            Assert.IsTrue(driver.Url.Contains("/EJuguetes"));
        }

        [Test]
        public void EliminarJuguete_Invalido_NoExiste()
        {
            driver.Navigate().GoToUrl($"{urlBase}/EJuguetes/Delete/9999");
            try
            {
                var boton = driver.FindElement(By.CssSelector("input[value='Delete']"));
                Assert.Fail("Se encontro un boton Delete en un juguete que no deberia existir");
            }
            catch (NoSuchElementException)
            {
                Assert.Pass("Correcto: no existe el juguete y no se mostro la opcion de eliminar");
            }
        }

        [Test]
        public void VerListaJuguetes_Valido_CargaCorrectamente()
        {
            driver.Navigate().GoToUrl($"{urlBase}/EJuguetes");
            wait.Until(d => d.FindElement(By.CssSelector("table")));

            var filas = driver.FindElements(By.CssSelector("table tbody tr"));
            Assert.IsTrue(filas.Count > 0, "La lista de juguetes se mostro correctamente");
        }

        [Test]
        public void BuscarJuguete_Valido_MuestraResultados()
        {
            driver.Navigate().GoToUrl($"{urlBase}/EJuguetes");
            wait.Until(d => d.FindElement(By.Name("searchString")));

            driver.FindElement(By.Name("searchString")).SendKeys("4");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            wait.Until(d => d.FindElement(By.CssSelector("table")));
            var texto = driver.PageSource;

            Assert.IsTrue(texto.Contains("4"), "El resultado debería contener el ID buscado");
        }

        [Test]
        public void BuscarJuguete_Invalido_NoDebeMostrarResultados()
        {
            driver.Navigate().GoToUrl($"{urlBase}/EJuguetes");
            wait.Until(d => d.FindElement(By.Name("searchString")));

            driver.FindElement(By.Name("searchString")).SendKeys("NO_EXISTE_999");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            wait.Until(d => d.FindElement(By.CssSelector("table")));
            var filas = driver.FindElements(By.CssSelector("table tbody tr"));

            Assert.AreEqual(0, filas.Count, "No deberían mostrarse elementos para un ID inexistente");
        }

        [TearDown]
        public void TearDown()
        {
            string screenshotPath = TomarScreenshot(TestContext.CurrentContext.Test.Name);

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = TestContext.CurrentContext.Result.StackTrace;
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                test.Fail("Test Fallido: " + errorMessage);
                test.Log(Status.Fail, "Detalle del error: " + stacktrace);

                test.AddScreenCaptureFromPath(screenshotPath);
            }
            else if (status == TestStatus.Passed)
            {
                test.Pass("Test Exitoso");

                test.AddScreenCaptureFromPath(screenshotPath);
            }

            driver.Quit();
            driver.Dispose();
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            extent.Flush();
        }

        private string TomarScreenshot(string nombrePrueba)
        {
            try
            {
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string carpeta = Path.Combine(escritorio, "Screenshots_Pruebas");
                Directory.CreateDirectory(carpeta);

                string archivo = Path.Combine(
                    carpeta,
                    $"{nombrePrueba}_{DateTime.Now:yyyyMMdd_HHmmss}.png"
                );

                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(archivo);

                TestContext.AddTestAttachment(archivo);

                return archivo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar screenshot: " + ex.Message);
                return "";
            }
        }
    }
}