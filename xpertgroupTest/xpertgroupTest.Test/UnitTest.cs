using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xpertgroupTest.Model;

namespace xpertgroupTest.Test
{
    [TestClass]
    public class UnitTest
    {
        private Service.CubeService operacion; 
        private const string DATA_CORRECTA = "2\n4 5\nUPDATE 2 2 2 4\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3\n2 4\nUPDATE 2 2 2 1\nQUERY 1 1 1 1 1 1\nQUERY 1 1 1 2 2 2\nQUERY 2 2 2 2 2 2";
        private const string ERROR_TAMANO = "2\n222 5\nUPDATE 2 2 2 4\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3";
        private const string COORDENADAS_ERRONEAS = "1\n2 4\nUPDATE 5 2 2 4\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3";       
        private const string ERROR_ACTUALIZAR = "1\n2 4\nUPDATE 2 2 1\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3";
        
        [TestInitialize]
        public void Setup()
        {
            operacion = new Service.CubeService();
        }

        [TestMethod]
        public void DataCorrectTest()
        {

            RespuestaGeneral respuestaGeneral = operacion.Ejecutar(DATA_CORRECTA);
            Assert.IsNotNull(respuestaGeneral);
            Assert.IsTrue(respuestaGeneral.EstadoOperacion);
        }

        [TestMethod]
        public void ErrorTamano()
        {

            RespuestaGeneral respuestaGeneral = operacion.Ejecutar(ERROR_TAMANO);
            Assert.IsNotNull(respuestaGeneral);
            Assert.IsTrue(respuestaGeneral.EstadoOperacion);
        }

        [TestMethod]
        public void ErrorCoordenadas()
        {

            RespuestaGeneral respuestaGeneral = operacion.Ejecutar(COORDENADAS_ERRONEAS);
            Assert.IsNotNull(respuestaGeneral);
            Assert.IsTrue(respuestaGeneral.EstadoOperacion);
        }

        [TestMethod]
        public void Update()
        {

            RespuestaGeneral respuestaGeneral = operacion.Ejecutar(ERROR_ACTUALIZAR);
            Assert.IsNotNull(respuestaGeneral);
            Assert.IsTrue(respuestaGeneral.EstadoOperacion);
        }
    }
}
