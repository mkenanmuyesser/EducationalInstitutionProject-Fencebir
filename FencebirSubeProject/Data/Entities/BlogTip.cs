using System;
using System.Collections.Generic;

namespace FencebirSubeProject.Entities
{
    public partial class BlogTip
    {
        public BlogTip()
        {
            Blog = new HashSet<Blog>();
        }

        public int BlogTipId { get; set; }
        public string BlogTipAdi { get; set; }
        public int Sira { get; set; }
        public bool AktifMi { get; set; }

        public virtual ICollection<Blog> Blog { get; set; }
    }
}
