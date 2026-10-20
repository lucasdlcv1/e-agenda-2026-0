using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using eAgenda.Infraestrutura.Compartilhado.Orm;

#nullable disable

namespace eAgenda.Infraestrutura.Compartilhado.Orm.Migrations;

[DbContext(typeof(EAgendaDbContext))]
[Migration("20261020200000_Add_TBCompromissos")]
public partial class Add_TBCompromissos : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TBCompromissos",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Assunto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                DataOcorrencia = table.Column<DateOnly>(type: "date", nullable: false),
                HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                HoraTermino = table.Column<TimeOnly>(type: "time", nullable: false),
                Tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                LinkOuLocal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                ContatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TBCompromissos", x => x.Id);
                table.ForeignKey(
                    name: "FK_TBCompromissos_TBContato_ContatoId",
                    column: x => x.ContatoId,
                    principalTable: "TBContato",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateIndex(
            name: "IX_TBCompromissos_ContatoId",
            table: "TBCompromissos",
            column: "ContatoId");

        migrationBuilder.CreateIndex(
            name: "IX_TBCompromissos_DataOcorrencia_HoraInicio",
            table: "TBCompromissos",
            columns: new[] { "DataOcorrencia", "HoraInicio" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "TBCompromissos");
    }
}
