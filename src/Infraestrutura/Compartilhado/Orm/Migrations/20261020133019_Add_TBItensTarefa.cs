using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgenda.Infraestrutura.Compartilhado.Orm.migrations
{
    /// <inheritdoc />
    public partial class Add_TBItensTarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemTarefa_TBTarefas_TarefaId",
                table: "ItemTarefa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemTarefa",
                table: "ItemTarefa");

            migrationBuilder.RenameTable(
                name: "ItemTarefa",
                newName: "TBItensTarefa");

            migrationBuilder.RenameIndex(
                name: "IX_ItemTarefa_TarefaId",
                table: "TBItensTarefa",
                newName: "IX_TBItensTarefa_TarefaId");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "TBItensTarefa",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBItensTarefa",
                table: "TBItensTarefa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBItensTarefa_TBTarefas_TarefaId",
                table: "TBItensTarefa",
                column: "TarefaId",
                principalTable: "TBTarefas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBItensTarefa_TBTarefas_TarefaId",
                table: "TBItensTarefa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBItensTarefa",
                table: "TBItensTarefa");

            migrationBuilder.RenameTable(
                name: "TBItensTarefa",
                newName: "ItemTarefa");

            migrationBuilder.RenameIndex(
                name: "IX_TBItensTarefa_TarefaId",
                table: "ItemTarefa",
                newName: "IX_ItemTarefa_TarefaId");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "ItemTarefa",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemTarefa",
                table: "ItemTarefa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTarefa_TBTarefas_TarefaId",
                table: "ItemTarefa",
                column: "TarefaId",
                principalTable: "TBTarefas",
                principalColumn: "Id");
        }
    }
}
