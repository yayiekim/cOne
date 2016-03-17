using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicOne.Models
{
    public class RecordTypeModel
    {
        public Guid Id { get; set; }
        public string RecordTypeName {get;set;}
        public Guid RecordTypeCategoryId { get; set; }
    }

    public class RecordCategoryModel
    {

       public Guid Id { get; set; }
       public string Category { get; set; } 
                  
    
    }

}