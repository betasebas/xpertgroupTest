using System;
using System.Collections.Generic;
using xpertgroupTest.Model;

namespace xpertgroupTest.Service
{
    public class Validacion
    {

        public void ValidarDatosEntrada(string data, ref RespuestaGeneral respuestaGeneral, out List<ArregloCubo> listCubo)
        {
            string[] datos = data.Split('\n');

            if (Convert.ToInt32(datos[0]) <= 0 || Convert.ToInt32(datos[0]) > 50)
            {
                respuestaGeneral.EstadoOperacion = false;
                respuestaGeneral.Mensaje = "La cantidad de casos de prueba debe ser entre 1 y 50.";
            }

            listCubo = CrearOjetoCubo(datos, ref respuestaGeneral);
        }

        private List<ArregloCubo> CrearOjetoCubo(string[] informacion, ref RespuestaGeneral respuestaGeneral)
        {
            List<ArregloCubo> listCubo = new List<ArregloCubo>();
            ArregloCubo cubo = new ArregloCubo();
            for (int i = 1; i < informacion.Length; i++)
            {
                string[] datos = informacion[i].Split(' ');
                if (datos.Length == 2)
                {
                    if (!ValidarTamanoMatrizOperaciones(datos, ref respuestaGeneral))
                        return new List<ArregloCubo>();

                    cubo = new ArregloCubo
                    {
                        TamanoMatriz = Convert.ToInt32(informacion[i].Split(' ')[0]),
                        CantidadOperaciones = Convert.ToInt32(informacion[i].Split(' ')[1]),
                        OperacionesCubo = new List<OperacionCubo>()
                    };
                    listCubo.Add(cubo);
                    continue;
                }
                cubo.OperacionesCubo.Add(new OperacionCubo
                {
                    TipoOperacion = datos[0],
                    InformacionOperacion = ObtenerInformacionOperacion(datos)
                });
            }
            return listCubo;
        }

        private bool ValidarTamanoMatrizOperaciones(string[] infoMatriz, ref RespuestaGeneral respuestaGeneral)
        {
            if (Convert.ToInt32(infoMatriz[0]) < 1 || Convert.ToInt32(infoMatriz[0]) > 100)
            {
                respuestaGeneral.EstadoOperacion = false;
                respuestaGeneral.Mensaje = "El tamaño de la matriz debe estar entre 1 y 100.";
                return false;
            }
                

            if (Convert.ToInt32(infoMatriz[1]) < 1 || Convert.ToInt32(infoMatriz[1]) > 1000)
            {
                respuestaGeneral.EstadoOperacion = false;
                respuestaGeneral.Mensaje = "La cantidad de operaciones debe estar entre 1 y 1000.";
                return false;
            }
           
            return true;
        }

        private long[] ObtenerInformacionOperacion(string[] datos)
        {
            long[] coordenadas = new long[datos.Length - 1];
            for (int i = 1; i < datos.Length; i++)
            {
                coordenadas[i - 1] = Convert.ToInt64(datos[i]);
            }
            return coordenadas;
        }

        public void ValidarCoordenadas(long[] informacionOperacion, ArregloCubo cubo, string tipoOperacion, ref RespuestaGeneral respuestaGeneral)
        {
            if (!respuestaGeneral.EstadoOperacion)
                return;
            int length = tipoOperacion.Equals(TipoOperacion.UPDATE.ToString()) ? informacionOperacion.Length - 1 : informacionOperacion.Length;
            for (int i = 0; i < length; i++)
            {
                if (informacionOperacion[i] > cubo.TamanoMatriz || informacionOperacion[i] <= 0)
                {
                    respuestaGeneral.EstadoOperacion = false;
                    respuestaGeneral.Mensaje = "Las coordenadas deben de ser menor o igual que el tamaño de la matriz y mayores que cero(0).";
                    return;
                }
            }
        }

        public RespuestaGeneral ValidarOperacionUpdate(long[] infoOperacion, ref RespuestaGeneral respuestaGeneral)
        {
            if (infoOperacion.Length != 4)
                return new RespuestaGeneral { EstadoOperacion = false, Mensaje = "La operación 'UPDATE' solo permite cuatro (4) datos de entrada." };
            if (infoOperacion[3] <= Math.Pow(10, -9) || infoOperacion[3] >= Math.Pow(10, 9))
                return new RespuestaGeneral { EstadoOperacion = false, Mensaje = "Los valores válidos para actualizar una posición del cubo oscila entre 10^9 y 10^-9." };

            return respuestaGeneral;
        }

        public RespuestaGeneral ValidarOperacionQuery(long[] infoOperacion, ref RespuestaGeneral respuesta)
        {
            if (infoOperacion.Length != 6)
                return new RespuestaGeneral { EstadoOperacion = false, Mensaje = "La operación 'QUERY' solo permite seis (6) datos de entrada." };

            return respuesta;
        }
    }
}