using System;
using System.Data;
using Migrator.Framework;

namespace Jamaica.TableFootball.Core.Migrations
{
    [Migration(20091201231526)]
    public class Participant : Migration
    {
        public override void Up()
        {
            Database.AddTable("Participant",
                new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("User_Id", DbType.String, ColumnProperty.NotNull),
                new Column("Score", DbType.Int32, ColumnProperty.NotNull));
        }

        public override void Down()
        {
            Database.RemoveTable("Participant");
        }
    }
}