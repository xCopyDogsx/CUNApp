using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUNApp.Models.ViewModels
{
    public class EstudiantesVM
    {
        public long Est_ID { get; set; }
        public string Est_Nombre { get; set; }
        public string Est_Apellido { get; set; }
        public string Est_Email { get; set; }
        
        public string Acciones { get; set; }
    }
}