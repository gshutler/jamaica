using System;
using System.Data;
using Migrator.Framework;

namespace OR.Throwaway.Migrations
{
    [Migration(200910251204)]
    public class Users : Migration
    {
        public override void Up()
        {
            Database.AddTable("User",
                new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("Username", DbType.String, 255, ColumnProperty.NotNull),
                new Column("PasswordSalt", DbType.String, 255, ColumnProperty.NotNull),
                new Column("PasswordHash", DbType.String, 255, ColumnProperty.NotNull));
        }

        public override void Down()
        {
            Database.RemoveTable("User");
        }
    }
}