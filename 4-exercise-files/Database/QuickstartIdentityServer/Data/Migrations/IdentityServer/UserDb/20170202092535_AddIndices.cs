using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickstartIdentityServer.Data.Migrations.IdentityServer.UserDb
{
    public partial class AddIndices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProviderName",
                table: "UserExternalProviders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserExternalProviders_ProviderName",
                table: "UserExternalProviders",
                column: "ProviderName");

            migrationBuilder.CreateIndex(
                name: "IX_UserExternalProviders_ProviderSubjectId",
                table: "UserExternalProviders",
                column: "ProviderSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExternalProviders_ProviderName_ProviderSubjectId",
                table: "UserExternalProviders",
                columns: new[] { "ProviderName", "ProviderSubjectId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserExternalProviders_ProviderName",
                table: "UserExternalProviders");

            migrationBuilder.DropIndex(
                name: "IX_UserExternalProviders_ProviderSubjectId",
                table: "UserExternalProviders");

            migrationBuilder.DropIndex(
                name: "IX_UserExternalProviders_ProviderName_ProviderSubjectId",
                table: "UserExternalProviders");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderName",
                table: "UserExternalProviders",
                nullable: true);
        }
    }
}
