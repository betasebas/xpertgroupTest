
using xpertgroupTest.Model;

namespace xpertgroupTest.Service
{
    public interface IOperacion
    {
        RespuestaGeneral EjecutarUpdate(OperacionCubo operacion, long[,,] matriz);

        RespuestaGeneral EjecutarQuery(OperacionCubo operacion, long[,,] matriz);
    }
}