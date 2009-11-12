using System.Data;
using Migrator.Framework;

namespace OR.Throwaway.Migrations
{
    [Migration(200910251304)]
    public class Tags : Migration
    {
        public override void Up()
        {
            Database.AddTable("Tag", 
                new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("Name", DbType.String, 255, ColumnProperty.NotNull)
                );
        }

        public override void Down()
        {
            Database.RemoveTable("Tag");
        }
    }
}