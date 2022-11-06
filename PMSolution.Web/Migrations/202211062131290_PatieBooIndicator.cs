namespace PMSolution.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatieBooIndicator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "Ind", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "Ind");
        }
    }
}
