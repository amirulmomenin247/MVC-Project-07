namespace Final_Eve_07.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdditionalFeatures",
                c => new
                    {
                        AdditionalFeatureId = c.Int(nullable: false, identity: true),
                        Sensors = c.String(nullable: false, maxLength: 200),
                        Battery = c.String(nullable: false, maxLength: 100),
                        VideoSupport = c.String(nullable: false, maxLength: 50),
                        SpeciFicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdditionalFeatureId)
                .ForeignKey("dbo.SpeciFications", t => t.SpeciFicationId, cascadeDelete: true)
                .Index(t => t.SpeciFicationId);
            
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        PlatformId = c.Int(nullable: false, identity: true),
                        Chipset = c.String(nullable: false, maxLength: 50),
                        CPU = c.String(nullable: false, maxLength: 50),
                        GPU = c.String(nullable: false, maxLength: 50),
                        AdditionalFeatureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlatformId)
                .ForeignKey("dbo.AdditionalFeatures", t => t.AdditionalFeatureId, cascadeDelete: true)
                .Index(t => t.AdditionalFeatureId);
            
            CreateTable(
                "dbo.SpeciFications",
                c => new
                    {
                        SpeciFicationId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 50),
                        Camera = c.String(nullable: false),
                        Display = c.String(nullable: false, maxLength: 50),
                        RAM = c.String(nullable: false, maxLength: 20),
                        PhoneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SpeciFicationId)
                .ForeignKey("dbo.Phones", t => t.PhoneId, cascadeDelete: true)
                .Index(t => t.PhoneId);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        PhoneId = c.Int(nullable: false, identity: true),
                        PhoneName = c.String(nullable: false, maxLength: 100),
                        LaunchDate = c.DateTime(nullable: false),
                        OperatingSystem = c.String(nullable: false, maxLength: 30),
                        PhonePrice = c.Decimal(nullable: false, storeType: "money"),
                        Available = c.Boolean(nullable: false),
                        Picture = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.PhoneId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdditionalFeatures", "SpeciFicationId", "dbo.SpeciFications");
            DropForeignKey("dbo.SpeciFications", "PhoneId", "dbo.Phones");
            DropForeignKey("dbo.Platforms", "AdditionalFeatureId", "dbo.AdditionalFeatures");
            DropIndex("dbo.SpeciFications", new[] { "PhoneId" });
            DropIndex("dbo.Platforms", new[] { "AdditionalFeatureId" });
            DropIndex("dbo.AdditionalFeatures", new[] { "SpeciFicationId" });
            DropTable("dbo.Phones");
            DropTable("dbo.SpeciFications");
            DropTable("dbo.Platforms");
            DropTable("dbo.AdditionalFeatures");
        }
    }
}
