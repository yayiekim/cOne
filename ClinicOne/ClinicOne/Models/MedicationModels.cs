using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicOne.Models
{
    public class MedicationModel
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string MedicationName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Dosage { get; set; }
              
    }

    public class MedicationCategoryModel
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }



    }



}