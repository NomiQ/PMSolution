namespace PMSolution.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAppointmentsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        StartTime = c.String(),
                        EndTime = c.String(),
                        PatientId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.StaffId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropIndex("dbo.Appointments", new[] { "StaffId" });
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropTable("dbo.Appointments");
        }
    }
}
