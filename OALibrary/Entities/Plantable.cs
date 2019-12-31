using System;
using System.Collections.Generic;

namespace OALibrary.Entities
{
    public partial class Plantable
    {
        public Plantable()
        {
            Plandistribute = new HashSet<Plandistribute>();
        }

        public int Id { get; set; }
        public string Teacherid { get; set; }
        public string Filepath { get; set; }
        public string Filename { get; set; }
        public DateTime Committime { get; set; }
        public uint State { get; set; }
        public string Issue { get; set; }

        public virtual Teacherinfomation Teacher { get; set; }
        public virtual ICollection<Plandistribute> Plandistribute { get; set; }
    }
}
