﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicOne.Models
{
    public class PatientModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Age { get; set; }
        public string BloodType { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string Remarks { get; set; }

        public string WaitingListId { get; set; }


        public virtual List<ConsultationModel> Consultations { get; set; }
       

    }
}