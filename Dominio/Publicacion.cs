using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Publicacion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public TipoPublicacion IdTipoPublicacion { get; set; }
        public string Texto { get; set; }
        public string FotoPerfil { get; set; }
        public int? IdPublicacionCompartida { get; set; }
    }
}
