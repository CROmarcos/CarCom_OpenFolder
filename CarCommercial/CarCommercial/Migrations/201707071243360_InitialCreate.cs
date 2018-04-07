namespace CarCommercial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Automobils",
                c => new
                    {
                        AutomobilID = c.Int(nullable: false, identity: true),
                        Marka = c.String(),
                        Model = c.String(),
                        GodinaProizvodnje = c.Int(nullable: false),
                        Registracija = c.String(),
                        Boja = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AutomobilID);
            
            CreateTable(
                "dbo.Oglas",
                c => new
                    {
                        OglasID = c.Int(nullable: false, identity: true),
                        OpisOglasa = c.String(),
                        BrojTrazenihAuta = c.Int(nullable: false),
                        Cijena = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OglasID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Oglas");
            DropTable("dbo.Automobils");
        }
    }
}
