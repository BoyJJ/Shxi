using System;
using System.Collections.Generic;

namespace OALibrary.Entities
{
    public partial class Awardpunish
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public int PunishContent { get; set; }
        public string AwardContent { get; set; }
        public int State { get; set; }
        public string Issue { get; set; }
        public string Sid { get; set; }

        public virtual Student S { get; set; }
    }
}
