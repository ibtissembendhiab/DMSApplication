using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folder_Group_GroupId",
                table: "Folder");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Folder",
                newName: "FolderGroupGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Folder_GroupId",
                table: "Folder",
                newName: "IX_Folder_FolderGroupGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folder_Group_FolderGroupGroupId",
                table: "Folder",
                column: "FolderGroupGroupId",
                principalTable: "Group",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folder_Group_FolderGroupGroupId",
                table: "Folder");

            migrationBuilder.RenameColumn(
                name: "FolderGroupGroupId",
                table: "Folder",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Folder_FolderGroupGroupId",
                table: "Folder",
                newName: "IX_Folder_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folder_Group_GroupId",
                table: "Folder",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
