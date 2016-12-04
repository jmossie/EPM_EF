using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPM_EF.Models
{
    public class DrawingRevision
    {
        public string ID { get; set; }
        public string RevisionName { get; set; }
        public string RevisionNumber { get; set; }
        public ReleaseStatus ReleaseStatus { get; set; }
        public string DrawingID { get; set; }
        public DateTime CreationDate { get; set; }
        public string Notes { get; set; }
    }
}

