

namespace Modelos
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; }
        public string Cedula { get; set; }
        public Departamento refDepartamento { get; set; }
        public decimal sueldo { get; set; }
        public string fechaContrato { get; set; }
    }
}
