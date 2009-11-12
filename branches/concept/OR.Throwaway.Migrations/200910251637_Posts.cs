using System;
using System.Data;
using Migrator.Framework;

namespace OR.Throwaway.Migrations
{
    [Migration(200910251637)]
    public class Posts : Migration
    {
        public override void Up()
        {
            Database.AddTable("Post",
                new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("Title", DbType.String, 255, ColumnProperty.NotNull),
                new Column("Description", DbType.String, 4000, ColumnProperty.NotNull),
                new Column("Author_id", DbType.Int32, ColumnProperty.ForeignKey | ColumnProperty.NotNull)
            );
            Database.AddForeignKey("FK_User_Post_Author", "Post", "Author_id", "User", "Id");
        }

        public override void Down()
        {
            Database.RemoveForeignKey("Post", "FK_User_Post_Author");
            Database.RemoveTable("Post");
        }
    }
}