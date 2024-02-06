using GestionBancariaAppNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System;
namespace GestionBancariaTest
{
    [TestClass]
    public class GestionBancariaTest
    {
        /*
       A1: cantidad <= 0 --> 1 CANTIDAD NO VÁLIDA
       A2: cantidad > saldo --> 2 SALDO INSUFICIENTE
       A3: cantidad < saldo --> 0 TODO CORRECTO
       */

        [TestMethod]
        public void ReintegroNOVALIDO()
        {
            double saldoInicial = 1000;
            double reintegro = -250;
            double saldoFinal = saldoInicial - reintegro;
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);
            try
            {
                miApp.RealizarReintegro(reintegro);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                // assert
                StringAssert.Contains(exception.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
                return;
            }
            Assert.Fail("Error. Se debía haber producido una excepción: Cantidad no válida");

        }
      
        [TestMethod]
        public void ReintegroINSUFICIENTE()
        {
            // preparación del caso de prueba
            double saldo = 300;
            double cantidad = 700;
            GestionBancariaApp miApp = new GestionBancariaApp(saldo);
            // Método a probar
            try
            {
                miApp.RealizarReintegro(cantidad);
            } 
            catch (ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, GestionBancariaApp.ERR_SALDO_INSUFICIENTE);
                return;
            }
            Assert.Fail("Error. Se debía haber producido la excepción: Saldo insuficiente.");
        }

        [TestMethod]
        [DataRow(1000, 250, 750)]
        [DataRow(800, 150, 650)]
        [DataRow(400, 50, 350)]
        public void ValidarReintegro(double saldoInicial, double reintegro, double saldoEsperado)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);
            // Método a probar
            miApp.RealizarReintegro(reintegro);
            Assert.AreEqual(saldoEsperado, miApp.ObtenerSaldo(), 0.001, "Se produjo un error al realizar el reintegro, saldo" + "incorrecto.");
        }

        [TestMethod]
        public void ingresoNOVALIDO()
        {
            double saldo = 550;
            double cantidad = 0;

            GestionBancariaApp miApp = new GestionBancariaApp(saldo);

            try
            {
                miApp.RealizarIngreso(cantidad);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
                return;
            }
            Assert.Fail("Error. Se debía haber producido la excepción: Cantidad no válida");

        }

        [TestMethod]
        public void ingresoCORRECTO()
        {
            double saldo = 400;
            double cantidad = 100;
            double saldoEsperado = saldo + cantidad;

            GestionBancariaApp miApp = new GestionBancariaApp(saldo);

            miApp.RealizarIngreso(cantidad);

            Assert.AreEqual(saldoEsperado, miApp.ObtenerSaldo(), 0.001, "Ingreso realizado correctamente.");

        }

    }
}
