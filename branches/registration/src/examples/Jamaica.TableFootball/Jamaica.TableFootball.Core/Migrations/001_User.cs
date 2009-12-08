using System;
using System.Data;
using Migrator.Framework;

namespace Jamaica.TableFootball.Core.Migrations
{
    [Migration(20091125074925)]
    public class User : Migration
    {
        public override void Up()
        {
            Database.AddTable("User",
                new Column("Name", DbType.String, 100, ColumnProperty.NotNull),
                new Column("Salt", DbType.AnsiStringFixedLength, 128, ColumnProperty.NotNull),
                new Column("Hash", DbType.AnsiStringFixedLength, 128, ColumnProperty.NotNull));
        }

        public override void Down()
        {
            Database.RemoveTable("User");
        }
    }
}