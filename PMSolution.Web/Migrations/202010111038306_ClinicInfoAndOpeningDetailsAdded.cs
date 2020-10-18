namespace PMSolution.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClinicInfoAndOpeningDetailsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClinicDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        OpenTime = c.String(),
                        CloseTime = c.String(),
                        ClinicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clinics", t => t.ClinicId, cascadeDelete: true)
                .Index(t => t.ClinicId);
            
            CreateTable(
                "dbo.Clinics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        FaxNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClinicDays", "ClinicId", "dbo.Clinics");
            DropIndex("dbo.ClinicDays", new[] { "ClinicId" });
            DropTable("dbo.Clinics");
            DropTable("dbo.ClinicDays");
        }
    }
}
