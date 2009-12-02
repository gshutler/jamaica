using System;
using System.Data;
using Migrator.Framework;

namespace Jamaica.TableFootball.Core.Migrations
{
    [Migration(20091202084621)]
    public class MatchResultDate : Migration
    {
        public override void Up()
        {
            Database.AddColumn("MatchResult", 
                new Column("Date", DbType.DateTime, ColumnProperty.NotNull, "'2000-01-01'"));
        }

        public override void Down()
        {
            Database.RemoveColumn("MatchResult", "Date");
        }
    }
}