namespace crudwithstoreprocedure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City4",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        StateId = c.Int(nullable: false),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.CustomerA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.State4",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.AddressA",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerA", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.StateA",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerA", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CityA",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        City = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerA", t => t.Id)
                .Index(t => t.Id);
            
            CreateStoredProcedure(
                "dbo.CustomerViewModel_Insert",
                p => new
                    {
                        Name = p.String(),
                        Email = p.String(),
                        address = p.String(),
                        State = p.Int(),
                        City = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[CustomerA]([Name], [Email])
                      VALUES (@Name, @Email)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[CustomerA]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      INSERT [dbo].[AddressA]([Id], [address])
                      VALUES (@Id, @address)
                      
                      INSERT [dbo].[StateA]([Id], [State])
                      VALUES (@Id, @State)
                      
                      INSERT [dbo].[CityA]([Id], [City])
                      VALUES (@Id, @City)
                      
                      SELECT t0.[Id]
                      FROM [dbo].[CustomerA] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.CustomerViewModel_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        Email = p.String(),
                        address = p.String(),
                        State = p.Int(),
                        City = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[CustomerA]
                      SET [Name] = @Name, [Email] = @Email
                      WHERE ([Id] = @Id)
                      
                      UPDATE [dbo].[AddressA]
                      SET [address] = @address
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      UPDATE [dbo].[StateA]
                      SET [State] = @State
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      UPDATE [dbo].[CityA]
                      SET [City] = @City
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0"
            );
            
            CreateStoredProcedure(
                "dbo.CustomerViewModel_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[AddressA]
                      WHERE ([Id] = @Id)
                      
                      DELETE [dbo].[StateA]
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      DELETE [dbo].[CityA]
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      DELETE [dbo].[CustomerA]
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.CustomerViewModel_Delete");
            DropStoredProcedure("dbo.CustomerViewModel_Update");
            DropStoredProcedure("dbo.CustomerViewModel_Insert");
            DropForeignKey("dbo.CityA", "Id", "dbo.CustomerA");
            DropForeignKey("dbo.StateA", "Id", "dbo.CustomerA");
            DropForeignKey("dbo.AddressA", "Id", "dbo.CustomerA");
            DropIndex("dbo.CityA", new[] { "Id" });
            DropIndex("dbo.StateA", new[] { "Id" });
            DropIndex("dbo.AddressA", new[] { "Id" });
            DropTable("dbo.CityA");
            DropTable("dbo.StateA");
            DropTable("dbo.AddressA");
            DropTable("dbo.State4");
            DropTable("dbo.CustomerA");
            DropTable("dbo.City4");
        }
    }
}
