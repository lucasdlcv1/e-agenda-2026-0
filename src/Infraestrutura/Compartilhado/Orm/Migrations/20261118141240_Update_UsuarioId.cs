using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgenda.Infraestrutura.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Update_UsuarioId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TBContato_Email",
                table: "TBContato");

            migrationBuilder.DropIndex(
                name: "IX_TBContato_Telefone",
                table: "TBContato");

            migrationBuilder.DropIndex(
                name: "IX_TBCategorias_Titulo",
                table: "TBCategorias");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "TBTarefas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "TBItensTarefa",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "TBDespesas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "TBContato",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "TBCompromissos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "TBCategorias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TBTarefas_UsuarioId",
                table: "TBTarefas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TBDespesas_UsuarioId",
                table: "TBDespesas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TBContato_UsuarioId_Email",
                table: "TBContato",
                columns: new[] { "UsuarioId", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBContato_UsuarioId_Telefone",
                table: "TBContato",
                columns: new[] { "UsuarioId", "Telefone" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBCompromissos_UsuarioId",
                table: "TBCompromissos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TBCategorias_UsuarioId_Titulo",
                table: "TBCategorias",
                columns: new[] { "UsuarioId", "Titulo" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TBCategorias_AspNetUsers_UsuarioId",
                table: "TBCategorias",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBCompromissos_AspNetUsers_UsuarioId",
                table: "TBCompromissos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBContato_AspNetUsers_UsuarioId",
                table: "TBContato",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBDespesas_AspNetUsers_UsuarioId",
                table: "TBDespesas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBTarefas_AspNetUsers_UsuarioId",
                table: "TBTarefas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBCategorias_AspNetUsers_UsuarioId",
                table: "TBCategorias");

            migrationBuilder.DropForeignKey(
                name: "FK_TBCompromissos_AspNetUsers_UsuarioId",
                table: "TBCompromissos");

            migrationBuilder.DropForeignKey(
                name: "FK_TBContato_AspNetUsers_UsuarioId",
                table: "TBContato");

            migrationBuilder.DropForeignKey(
                name: "FK_TBDespesas_AspNetUsers_UsuarioId",
                table: "TBDespesas");

            migrationBuilder.DropForeignKey(
                name: "FK_TBTarefas_AspNetUsers_UsuarioId",
                table: "TBTarefas");

            migrationBuilder.DropIndex(
                name: "IX_TBTarefas_UsuarioId",
                table: "TBTarefas");

            migrationBuilder.DropIndex(
                name: "IX_TBDespesas_UsuarioId",
                table: "TBDespesas");

            migrationBuilder.DropIndex(
                name: "IX_TBContato_UsuarioId_Email",
                table: "TBContato");

            migrationBuilder.DropIndex(
                name: "IX_TBContato_UsuarioId_Telefone",
                table: "TBContato");

            migrationBuilder.DropIndex(
                name: "IX_TBCompromissos_UsuarioId",
                table: "TBCompromissos");

            migrationBuilder.DropIndex(
                name: "IX_TBCategorias_UsuarioId_Titulo",
                table: "TBCategorias");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TBTarefas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TBItensTarefa");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TBDespesas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TBContato");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TBCompromissos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TBCategorias");

            migrationBuilder.CreateIndex(
                name: "IX_TBContato_Email",
                table: "TBContato",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBContato_Telefone",
                table: "TBContato",
                column: "Telefone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBCategorias_Titulo",
                table: "TBCategorias",
                column: "Titulo",
                unique: true);
        }
    }
}
