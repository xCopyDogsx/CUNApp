//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CUNApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Curso
    {
        public long Cur_ID { get; set; }
        public long Mat_ID { get; set; }
        public long Est_ID { get; set; }
        public long Doc_ID { get; set; }
    
        public virtual Docente Docente { get; set; }
        public virtual Estudiante Estudiante { get; set; }
        public virtual Materia Materia { get; set; }
    }
}
