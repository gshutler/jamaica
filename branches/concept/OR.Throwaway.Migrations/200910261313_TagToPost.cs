using System;
using System.Data;
using Migrator.Framework;

namespace OR.Throwaway.Migrations
{
    [Migration(200910261313)]
    public class TagToPost : Migration
    {
        public override void Up()
        {
            Database.AddTable("TagToPost",
                new Column("Tag_Id", DbType.Int32, ColumnProperty.ForeignKey | ColumnProperty.NotNull),
                new Column("Post_Id", DbType.Int32, ColumnProperty.ForeignKey | ColumnProperty.NotNull)
            );
            Database.AddForeignKey("FK_Tag_TagToPost", "TagToPost", "Tag_Id", "Tag", "Id");
            Database.AddForeignKey("FK_Post_TagToPost", "TagToPost", "Post_Id", "Post", "Id");
        }

        public override void Down()
        {
            Database.RemoveForeignKey("TagToPost", "FK_Post_TagToPost");
            Database.RemoveForeignKey("TagToPost", "FK_Tag_TagToPost");
            Database.RemoveTable("TagToPost");
        }
    }
}