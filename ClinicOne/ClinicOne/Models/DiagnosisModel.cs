using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicOne.Models
{
    public class DiagnosisModel
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string DiagnosisName { get; set; }
        public string Description { get; set; }
     
    }


    public class DiagnostCategoryModel
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }



    }
}