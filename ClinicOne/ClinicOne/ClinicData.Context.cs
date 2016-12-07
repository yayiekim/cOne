﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClinicOne
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ClinicOneEntities : DbContext
    {
        public ClinicOneEntities()
            : base("name=ClinicOneEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }
        public virtual DbSet<Consultation> Consultations { get; set; }
        public virtual DbSet<ConsultationsDiagnosi> ConsultationsDiagnosis { get; set; }
        public virtual DbSet<ConsultationsOtherService> ConsultationsOtherServices { get; set; }
        public virtual DbSet<ContactNumber> ContactNumbers { get; set; }
        public virtual DbSet<Diagnosi> Diagnosis { get; set; }
        public virtual DbSet<DiagnosisCategory> DiagnosisCategories { get; set; }
        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<PatientInsurance> PatientInsurances { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientsRecord> PatientsRecords { get; set; }
        public virtual DbSet<RecordTypesCategory> RecordTypesCategories { get; set; }
        public virtual DbSet<RecordTypesCategoryClass> RecordTypesCategoryClasses { get; set; }
        public virtual DbSet<SecurityLog> SecurityLogs { get; set; }
        public virtual DbSet<ValueType> ValueTypes { get; set; }
        public virtual DbSet<Waiting> Waitings { get; set; }
        public virtual DbSet<LabResult> LabResults { get; set; }
        public virtual DbSet<RecordType> RecordTypes { get; set; }
        public virtual DbSet<DrugsCategory> DrugsCategories { get; set; }
        public virtual DbSet<PrescribedMedication> PrescribedMedications { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
    }
}
