namespace PMSolution.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDayOfWeekToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClinicDays", "Day", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClinicDays", "Day", c => c.Int(nullable: false));
        }
    }
}
