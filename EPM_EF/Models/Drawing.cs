using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EPM_EF.Models;

namespace EPM_EF.Models
{
    public class Drawing
    {

        public string ID { get; set; }
        public string DrawingNumber { get; set; }
        public string DrawingName { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<DrawingRevision> DrawingRevisions { get; set; }
        public string LatestRev { get; set; }
        public string Notes { get; set; }

    }
}
