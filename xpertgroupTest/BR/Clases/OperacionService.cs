using xpertgroupTest.Model;

namespace xpertgroupTest.Service
{
    public class Operacion : IOperacion
    {
        protected readonly Validacion validacionDatos;
        public Operacion()
        {
            validacionDatos = new Validacion();
        }
        protected RespuestaGeneral respuestaGeneral = new RespuestaGeneral { EstadoOperacion = true };
        public RespuestaGeneral EjecutarUpdate(OperacionCubo operacion, long[,,] matriz)
        {
            respuestaGeneral = validacionDatos.ValidarOperacionUpdate(operacion.InformacionOperacion, ref respuestaGeneral);
            if (respuestaGeneral.EstadoOperacion)
                ActualizarValor(operacion.InformacionOperacion, matriz);
            return respuestaGeneral;
        }

        private void ActualizarValor(long[] infoOperacion, long[,,] matriz) =>
            matriz[infoOperacion[0] - 1, infoOperacion[1] - 1, infoOperacion[2] - 1] = infoOperacion[3];
   

        public RespuestaGeneral EjecutarQuery(OperacionCubo operacion, long[,,] matriz)
        {
            respuestaGeneral = validacionDatos.ValidarOperacionQuery(operacion.InformacionOperacion, ref respuestaGeneral);
            if (!respuestaGeneral.EstadoOperacion)
                return respuestaGeneral;
            AcumularValores(operacion.InformacionOperacion, matriz);
            return respuestaGeneral;
        }

        private RespuestaGeneral AcumularValores(long[] infoOperacion, long[,,] matriz)
        {
            long sumatoria = 0;
            int length = matriz.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int k = 0; k < length; k++)
                    {
                        if (infoOperacion[0] - 1 <= i && infoOperacion[1] - 1 <= j && infoOperacion[2] - 1 <= k
                            && i <= infoOperacion[3] - 1 && j <= infoOperacion[4] - 1 && k <= infoOperacion[5] - 1)
                            sumatoria += matriz[i, j, k];
                    }
                }
            }
            respuestaGeneral.Mensaje += sumatoria + "\n";
            return respuestaGeneral;
        }

    }
}