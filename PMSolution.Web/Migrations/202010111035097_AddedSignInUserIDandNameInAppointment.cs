namespace PMSolution.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSignInUserIDandNameInAppointment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "StaffId", "dbo.Staffs");
            DropIndex("dbo.Appointments", new[] { "StaffId" });
            RenameColumn(table: "dbo.Appointments", name: "StaffId", newName: "Staff_Id");
            AddColumn("dbo.Appointments", "UserId", c => c.String());
            AddColumn("dbo.Appointments", "UserEmail", c => c.String());
            AlterColumn("dbo.Appointments", "Staff_Id", c => c.Int());
            CreateIndex("dbo.Appointments", "Staff_Id");
            AddForeignKey("dbo.Appointments", "Staff_Id", "dbo.Staffs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "Staff_Id", "dbo.Staffs");
            DropIndex("dbo.Appointments", new[] { "Staff_Id" });
            AlterColumn("dbo.Appointments", "Staff_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Appointments", "UserEmail");
            DropColumn("dbo.Appointments", "UserId");
            RenameColumn(table: "dbo.Appointments", name: "Staff_Id", newName: "StaffId");
            CreateIndex("dbo.Appointments", "StaffId");
            AddForeignKey("dbo.Appointments", "StaffId", "dbo.Staffs", "Id", cascadeDelete: true);
        }
    }
}
