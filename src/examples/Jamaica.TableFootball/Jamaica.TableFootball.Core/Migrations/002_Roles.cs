using System;
using System.Data;
using Migrator.Framework;

namespace Jamaica.TableFootball.Core.Migrations
{
    [Migration(20091129160953)]
    public class Roles : Migration
    {
        public override void Up()
        {
            Database.AddTable("Role",
                new Column("Name", DbType.String, 50, ColumnProperty.NotNull));
        }

        public override void Down()
        {
            Database.RemoveTable("Role");
        }
    }
}