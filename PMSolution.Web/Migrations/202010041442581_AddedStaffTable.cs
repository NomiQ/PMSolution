namespace PMSolution.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStaffTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        CNIC = c.String(),
                        DateOfBirth = c.DateTime(nullable: false, storeType: "date"),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Staffs");
        }
    }
}
