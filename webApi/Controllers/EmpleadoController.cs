using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoData _empleado;
        //inyectamos la dependencia
        public EmpleadoController( EmpleadoData empleadodata)
        {
            _empleado = empleadodata;
        }

        //listamos el empleado

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            // retornamos la lista
            List<Empleado> lista = await _empleado.listar();
            return StatusCode(StatusCodes.Status200OK,lista);

        }

        //creamos el empleado
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Empleado objeto)
        {
            string respuesta = await _empleado.Crear(objeto);

            return StatusCode(StatusCodes.Status200OK, new {isSucces=respuesta});

        }


        //editamos el empleado
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Empleado objeto)
        {
            string respuesta = await _empleado.Editar(objeto);

            return StatusCode(StatusCodes.Status200OK, new { isSucces = respuesta });

        }

        //eliminamos el empleado
        [HttpDelete ("{id}")]
        public async Task<IActionResult> ELiminar(int id)
        {
            string respuesta = await _empleado.Eliminar(id);

            return StatusCode(StatusCodes.Status200OK, new { isSucces = respuesta });

        }


    }
}
