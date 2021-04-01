using Microsoft.EntityFrameworkCore.Migrations;

namespace DMSWebApplication.Migrations
{
    public partial class creation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_AspNetUsers_FileOwnerId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Folder_FileFolderFolderId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "Fichier");

            migrationBuilder.RenameIndex(
                name: "IX_Files_FileOwnerId",
                table: "Fichier",
                newName: "IX_Fichier_FileOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_FileFolderFolderId",
                table: "Fichier",
                newName: "IX_Fichier_FileFolderFolderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fichier",
                table: "Fichier",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fichier_AspNetUsers_FileOwnerId",
                table: "Fichier",
                column: "FileOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fichier_Folder_FileFolderFolderId",
                table: "Fichier",
                column: "FileFolderFolderId",
                principalTable: "Folder",
                principalColumn: "FolderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fichier_AspNetUsers_FileOwnerId",
                table: "Fichier");

            migrationBuilder.DropForeignKey(
                name: "FK_Fichier_Folder_FileFolderFolderId",
                table: "Fichier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fichier",
                table: "Fichier");

            migrationBuilder.RenameTable(
                name: "Fichier",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_Fichier_FileOwnerId",
                table: "Files",
                newName: "IX_Files_FileOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Fichier_FileFolderFolderId",
                table: "Files",
                newName: "IX_Files_FileFolderFolderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_AspNetUsers_FileOwnerId",
                table: "Files",
                column: "FileOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Folder_FileFolderFolderId",
                table: "Files",
                column: "FileFolderFolderId",
                principalTable: "Folder",
                principalColumn: "FolderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
