using System;
using System.Data;
using Migrator.Framework;

namespace Jamaica.TableFootball.Core.Migrations
{
    [Migration(20091201232607)]
    public class MatchResult : Migration
    {
        public override void Up()
        {
            Database.AddTable("MatchResult",
                new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("Victor_Id", DbType.Int32, ColumnProperty.NotNull),
                new Column("Opponent_Id", DbType.Int32, ColumnProperty.NotNull));
        }

        public override void Down()
        {
            Database.RemoveTable("MatchResult");
        }
    }
}