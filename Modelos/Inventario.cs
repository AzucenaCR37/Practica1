using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Inventario
    {
        public int id { get; set; }
        public String nombreCorto { get; set; }
        public String descripcion { get; set; }
        public String serie { get; set; }
        public String color { get; set; }
        public DateTime fechaAdquision { get; set; }
        public String tipoAdquision { get; set; }
        public String observaciones { get; set; }
        public int areas_id { get; set; }

        public String nombreArea { get; set; }
    }
}
