using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EPM_EF.Models
{
    public class DrawingRevision
    {
        public string ID { get; set; }
        [Display(Name = "Drawing Revision Name")]
        public string RevisionName { get; set; }
        [Display(Name = "Drawing Revision Number")]
        public string RevisionNumber { get; set; }
        [Display(Name = "Release Status")]
        public ReleaseStatus releaseStatus { get; set; }
        public string DrawingID { get; set; }
        [Display(Name = "Date Created")]
        public DateTime CreationDate { get; set; }
        public string Notes { get; set; }
    }
}

