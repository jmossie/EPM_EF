using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EPM_EF.Models;

namespace EPM_EF.DAL
{
    public class EPMInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EPMContext>
    {
        protected override void Seed(EPMContext context)
        {
            var drawings = new List<Drawing>()
            {
                new Drawing {ID="10DE7AC7-02E5-44B4-87E6-CB4E2A883F48",DrawingNumber="7676898921",DrawingName="First Drawing", CreationDate=DateTime.Parse("2016-11-27") },
                new Drawing {ID="B85F3776-3576-48AF-9970-927A8538F5EE",DrawingNumber="4343242",DrawingName="Second Drawing", CreationDate=DateTime.Parse("2016-12-27") },
                new Drawing {ID="DEFC4C7E-07B2-47B3-84B2-CC4391A44A31",DrawingNumber="7545465-767",DrawingName="Third Drawing", CreationDate=DateTime.Parse("2016-10-27") },
                new Drawing {ID="0504F8A6-FFB0-4CF0-BDB0-40007D189C16",DrawingNumber="53435432-8586",DrawingName="Fourth Drawing", CreationDate=DateTime.Parse("2016-09-27") }
            };
            drawings.ForEach(d => context.Drawings.Add(d));
            context.SaveChanges();
            var drawingRevisions = new List<DrawingRevision>()
            {
                new DrawingRevision { ID="3E7FD3F6-F9F0-4AF7-AE89-7F966BEAC2F4",RevisionNumber="A",RevisionName="1st Drawing First Revision",DrawingID="10DE7AC7-02E5-44B4-87E6-CB4E2A883F48" ,CreationDate = DateTime.Parse("2016-11-27") },
                new DrawingRevision { ID="3922CFBF-9718-46B8-8104-DD080B7362DD",RevisionNumber="B",RevisionName="1st Drawing Second Revision",DrawingID="10DE7AC7-02E5-44B4-87E6-CB4E2A883F48" ,CreationDate = DateTime.Parse("2016-11-27") },
                new DrawingRevision { ID="5B16C2FA-D033-4938-A3AB-98BBFD4515F8",RevisionNumber="A",RevisionName="2st Drawing First Revision",DrawingID="B85F3776-3576-48AF-9970-927A8538F5EE" ,CreationDate = DateTime.Parse("2016-11-27") },
                new DrawingRevision { ID="220636FC-1897-42E0-8059-BCADAE9AF84E",RevisionNumber="B",RevisionName="2st Drawing Second Revision",DrawingID="B85F3776-3576-48AF-9970-927A8538F5EE" ,CreationDate = DateTime.Parse("2016-11-27") },
                new DrawingRevision { ID="89BBB081-8705-4766-9471-2F70A2A047A9",RevisionNumber="A",RevisionName="3st Drawing First Revision",DrawingID="DEFC4C7E-07B2-47B3-84B2-CC4391A44A31" ,CreationDate = DateTime.Parse("2016-11-27") },
                new DrawingRevision { ID="57D67B16-DC5D-4C03-9DC4-9AD527FDFC1E",RevisionNumber="B",RevisionName="3st Drawing Second Revision",DrawingID="DEFC4C7E-07B2-47B3-84B2-CC4391A44A31" ,CreationDate = DateTime.Parse("2016-11-27") },
                new DrawingRevision { ID="57BC1454-4F92-4932-803B-B95DE750702A",RevisionNumber="A",RevisionName="4st Drawing First Revision",DrawingID="0504F8A6-FFB0-4CF0-BDB0-40007D189C16" ,CreationDate = DateTime.Parse("2016-11-27") },
                new DrawingRevision { ID="DE29254C-5E29-4B49-AFA2-A6BC466CB617",RevisionNumber="B",RevisionName="4st Drawing Second Revision",DrawingID="0504F8A6-FFB0-4CF0-BDB0-40007D189C16" ,CreationDate = DateTime.Parse("2016-11-27") }
            };
            drawingRevisions.ForEach(dr => context.DrawingRevisions.Add(dr));
            context.SaveChanges();
        }

    }
}