﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutogearWeb.EFModels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class autogearEntities : DbContext
    {
        public autogearEntities()
            : base("name=autogearEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Instructor_Leaves> Instructor_Leaves { get; set; }
        public virtual DbSet<Instructor_Student> Instructor_Student { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Package_Details> Package_Details { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Student_Address> Student_Address { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Suburb> Suburbs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PostCode> PostCodes { get; set; }
        public virtual DbSet<RTA> RTAs { get; set; }
        public virtual DbSet<Student_License> Student_License { get; set; }
    }
}