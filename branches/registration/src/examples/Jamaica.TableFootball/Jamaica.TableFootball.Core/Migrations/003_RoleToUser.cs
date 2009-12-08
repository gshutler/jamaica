using System;
using System.Data;
using Migrator.Framework;

namespace Jamaica.TableFootball.Core.Migrations
{
    [Migration(20091129161224)]
    public class RoleToUser : Migration
    {
        public override void Up()
        {
            Database.AddTable("RoleToUser",
                new Column("User_Id", DbType.String, 100, ColumnProperty.NotNull),
                new Column("Role_Id", DbType.String, 50, ColumnProperty.NotNull));
        }

        public override void Down()
        {
            Database.RemoveTable("RoleToUser");
        }
    }
}