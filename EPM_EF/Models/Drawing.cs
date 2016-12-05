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
        [Display(Name = "Drawing Number")]
        public string DrawingNumber { get; set; }
        [Display(Name = "Drawing Name")]
        public string DrawingName { get; set; }
        [Display(Name = "Date Created")]
        public DateTime CreationDate { get; set; }
        public virtual ICollection<DrawingRevision> DrawingRevisions { get; set; }
        [Display(Name = "Latest Revision")]
        public string LatestRev { get; set; }
        public string Notes { get; set; }

    }
}
