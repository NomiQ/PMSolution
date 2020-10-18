namespace PMSolution.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingAppointmentsFromPatientClassTest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropIndex("dbo.Appointments", new[] { "PatientId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Appointments", "PatientId");
            AddForeignKey("dbo.Appointments", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
        }
    }
}
