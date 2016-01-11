namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DistributionCenters",
                c => new
                    {
                        DistributionCenterId = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.Int(nullable: false),
                        Name = c.String(),
                        IsHead = c.Boolean(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.DistributionCenterId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                        DistributionCenterId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.DistributionCenters", t => t.DistributionCenterId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.DistributionCenterId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Distributions",
                c => new
                    {
                        DistributionId = c.Int(nullable: false, identity: true),
                        PackageId = c.Int(nullable: false),
                        On = c.DateTime(nullable: false),
                        Description = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistributionId)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        MedicineId = c.Int(nullable: false, identity: true),
                        MedicineName = c.String(),
                        Description = c.String(),
                        ShelfLife = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        IstempSensitve = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MedicineId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        PackageId = c.Int(nullable: false, identity: true),
                        MedicineId = c.Int(nullable: false),
                        BarcodeId = c.String(),
                        ExpiryDate = c.DateTime(nullable: false),
                        RegisteredOn = c.DateTime(nullable: false),
                        PackageStatusId = c.Int(),
                        CurrentLocation_DistributionCenterId = c.Int(),
                        RegisteredAtDC_DistributionCenterId = c.Int(),
                        RegisteredByUser_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.PackageId)
                .ForeignKey("dbo.DistributionCenters", t => t.CurrentLocation_DistributionCenterId)
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .ForeignKey("dbo.PackageStatus", t => t.PackageStatusId)
                .ForeignKey("dbo.DistributionCenters", t => t.RegisteredAtDC_DistributionCenterId)
                .ForeignKey("dbo.Users", t => t.RegisteredByUser_UserId)
                .Index(t => t.MedicineId)
                .Index(t => t.PackageStatusId)
                .Index(t => t.CurrentLocation_DistributionCenterId)
                .Index(t => t.RegisteredAtDC_DistributionCenterId)
                .Index(t => t.RegisteredByUser_UserId);
            
            CreateTable(
                "dbo.PackageStatus",
                c => new
                    {
                        PackageStatusId = c.Int(nullable: false, identity: true),
                        TransitState = c.String(),
                    })
                .PrimaryKey(t => t.PackageStatusId);
            
            CreateTable(
                "dbo.PackageTransactions",
                c => new
                    {
                        PackageTransactionsId = c.Int(nullable: false, identity: true),
                        PackageId = c.Int(nullable: false),
                        BarcodeId = c.String(),
                        FromLocId = c.Int(nullable: false),
                        ToLocId = c.Int(nullable: false),
                        SentBy = c.String(),
                        SentOn = c.DateTime(nullable: false),
                        ReceivedBy = c.String(),
                        ReceivedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PackageTransactionsId)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Packages", "RegisteredByUser_UserId", "dbo.Users");
            DropForeignKey("dbo.Packages", "RegisteredAtDC_DistributionCenterId", "dbo.DistributionCenters");
            DropForeignKey("dbo.PackageTransactions", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Packages", "PackageStatusId", "dbo.PackageStatus");
            DropForeignKey("dbo.Packages", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.Distributions", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Packages", "CurrentLocation_DistributionCenterId", "dbo.DistributionCenters");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "DistributionCenterId", "dbo.DistributionCenters");
            DropIndex("dbo.PackageTransactions", new[] { "PackageId" });
            DropIndex("dbo.Packages", new[] { "RegisteredByUser_UserId" });
            DropIndex("dbo.Packages", new[] { "RegisteredAtDC_DistributionCenterId" });
            DropIndex("dbo.Packages", new[] { "CurrentLocation_DistributionCenterId" });
            DropIndex("dbo.Packages", new[] { "PackageStatusId" });
            DropIndex("dbo.Packages", new[] { "MedicineId" });
            DropIndex("dbo.Distributions", new[] { "PackageId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "DistributionCenterId" });
            DropTable("dbo.PackageTransactions");
            DropTable("dbo.PackageStatus");
            DropTable("dbo.Packages");
            DropTable("dbo.Medicines");
            DropTable("dbo.Distributions");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.DistributionCenters");
        }
    }
}
