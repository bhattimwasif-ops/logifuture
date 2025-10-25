namespace WalletApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        WalletId = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.String(nullable: false, maxLength: 50),
                        CreatedAt = c.DateTime(nullable: false),
                        ReferenceId = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wallets", t => t.WalletId, cascadeDelete: true)
                .Index(t => t.WalletId)
                .Index(t => t.ReferenceId);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserId, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "WalletId", "dbo.Wallets");
            DropIndex("dbo.Wallets", new[] { "UserId" });
            DropIndex("dbo.Transactions", new[] { "ReferenceId" });
            DropIndex("dbo.Transactions", new[] { "WalletId" });
            DropTable("dbo.Wallets");
            DropTable("dbo.Transactions");
        }
    }
}
