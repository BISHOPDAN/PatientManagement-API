﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Patients",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Patients");
        }
    }
}
