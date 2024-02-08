using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GestionBancariaAppNS;

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
        [DataRow(450, 0)]
        [DataRow(100, -1)]
        [DataRow(1145, -20)]
        [DataRow(200, -100)]
        public void ReintegroNOVALIDO(double saldoInicial, double reintegro)
        {
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
        [DataRow(300, 301)]
        [DataRow(738, 739)]
        [DataRow(800, 1000)]
        [DataRow(500, 650)]
        [DataRow(0, 1)]
        public void ReintegroINSUFICIENTE(double saldo, double cantidad)
        {
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
        [DataRow(1000, 1000, 0)]
        [DataRow(500, 1, 499)]
        [DataRow(400, 50, 350)]
        [DataRow(300, 25, 275)]
        public void ValidarReintegro(double saldoInicial, double reintegro, double saldoEsperado)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);
            // Método a probar
            miApp.RealizarReintegro(reintegro);
            Assert.AreEqual(saldoEsperado, miApp.ObtenerSaldo(), 0.001, "Se produjo un error al realizar el reintegro, saldo" + "incorrecto.");
        }

        [TestMethod]
        [DataRow(550, 0)]
        [DataRow(900, -1)]
        [DataRow(240, -500)]
        public void ingresoNOVALIDO(double saldo, double cantidad)
        {
     
            GestionBancariaApp miApp = new GestionBancariaApp(saldo);

            try
            {
                miApp.RealizarIngreso(cantidad);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
                return;
            }
            Assert.Fail("Error. Se debía haber producido la excepción: Cantidad no válida");

        }

        [TestMethod]
        [DataRow(600, 1, 601)]
        [DataRow(789, 10, 799)]
        [DataRow(1500, 200, 1700)]
        public void ingresoCORRECTO(double saldo, double cantidad, double saldoEsperado)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldo);

            miApp.RealizarIngreso(cantidad);

            Assert.AreEqual(saldoEsperado, miApp.ObtenerSaldo(), 0.001, "Ingreso realizado correctamente.");

        }

    }
}
