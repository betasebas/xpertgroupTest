
using System;
using System.Collections.Generic;
using xpertgroupTest.Model;

namespace xpertgroupTest.Service
{
    public class CubeService : Operacion
    {
        public CubeService()
        {
        }

        public RespuestaGeneral Ejecutar(string datos)
        {
            try
            {
                validacionDatos.ValidarDatosEntrada(datos, ref respuestaGeneral, out List<ArregloCubo> listCubo);
                foreach (ArregloCubo cubo in listCubo)
                {
                    if (!respuestaGeneral.EstadoOperacion)
                        return respuestaGeneral;
                    respuestaGeneral = ProcesarCubo(cubo, respuestaGeneral);
                }
            }
            catch (Exception ex)
            {
                return new RespuestaGeneral { EstadoOperacion = false, Mensaje = ex.Message, Informacion = ex };
            }
            return respuestaGeneral;
        }

        private RespuestaGeneral ProcesarCubo(ArregloCubo cubo, RespuestaGeneral respuestaGeneral)
        {
            long[,,] matriz = new long[cubo.TamanoMatriz, cubo.TamanoMatriz, cubo.TamanoMatriz];
            return ResolverOperacionesMatriz(matriz, cubo, respuestaGeneral);
        }

        private RespuestaGeneral ResolverOperacionesMatriz(long[,,] matriz, ArregloCubo cubo, RespuestaGeneral respuestaGeneral)
        {
            if (!respuestaGeneral.EstadoOperacion)
                return respuestaGeneral;
            return ResolverOperaciones(cubo, matriz);
        }

        private RespuestaGeneral ResolverOperaciones(ArregloCubo cubo, long[,,] matriz)
        {
            foreach (OperacionCubo operacionCubo in cubo.OperacionesCubo)
            {
                string tipoOperacion = operacionCubo.TipoOperacion.ToUpper();
                if (!respuestaGeneral.EstadoOperacion)
                    return respuestaGeneral;
                validacionDatos.ValidarCoordenadas(operacionCubo.InformacionOperacion, cubo, tipoOperacion, ref respuestaGeneral);

                if (tipoOperacion.Equals(TipoOperacion.UPDATE.ToString()))
                    EjecutarUpdate(operacionCubo, matriz);

                if (tipoOperacion.Equals(TipoOperacion.QUERY.ToString()))
                    EjecutarQuery(operacionCubo, matriz);
            }
            return respuestaGeneral;
        }
    }
}