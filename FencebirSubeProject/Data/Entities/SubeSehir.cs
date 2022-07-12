using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class SubeSehir
    {
        public SubeSehir()
        {
            Sube = new HashSet<Sube>();
        }

        public int SubeSehirId { get; set; }
        public string SubeSehirAdi { get; set; }

        public virtual ICollection<Sube> Sube { get; set; }
    }
}
