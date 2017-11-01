namespace crudwithstoreprocedure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StateA", "State", c => c.String());
            AlterColumn("dbo.CityA", "City", c => c.String());
            AlterStoredProcedure(
                "dbo.CustomerViewModel_Insert",
                p => new
                    {
                        Name = p.String(),
                        Email = p.String(),
                        address = p.String(),
                        State = p.String(),
                        City = p.String(),
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
            
            AlterStoredProcedure(
                "dbo.CustomerViewModel_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        Email = p.String(),
                        address = p.String(),
                        State = p.String(),
                        City = p.String(),
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
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CityA", "City", c => c.Int(nullable: false));
            AlterColumn("dbo.StateA", "State", c => c.Int(nullable: false));
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
