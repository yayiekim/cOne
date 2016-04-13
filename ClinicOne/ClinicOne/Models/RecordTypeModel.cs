using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicOne.Models
{
    public class RecordTypeModel
    {
        public Guid Id { get; set; }
        public string RecordTypeName { get; set; }
        public Guid RecordTypeCategoryId { get; set; }
        public int ValueTypeId { get; set; }
        public string RecordTypeCategoryName { get; set; }
        public string ValueTypeName { get; set; }
    }

    public class RecordCategoryModel
    {

        public Guid Id { get; set; }
        public string Category { get; set; }
        public int CategoryClassId { get; set; }
        public string CategoryClassName { get; set; }


    }

    public class ValueTypeModel
    {

        public int Id { get; set; }
        public string ValueTypeName { get; set; }


    }
    public class RecordTypeClassModel
    {

        public int Id { get; set; }
        public string ClassName { get; set; }


    }

}