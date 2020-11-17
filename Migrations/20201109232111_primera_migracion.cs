using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrimerParcial.Migrations
{
    public partial class primera_migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    nombresCliente = table.Column<string>(nullable: true),
                    apellidosCliente = table.Column<string>(nullable: true),
                    telefonoCliente = table.Column<string>(nullable: true),
                    correoCliente = table.Column<string>(nullable: true),
                    direccionCliente = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clienteId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    nombresTecnico = table.Column<string>(nullable: true),
                    apellidosTecnico = table.Column<string>(nullable: true),
                    telefonoTecnico = table.Column<string>(nullable: true),
                    correoTecnico = table.Column<string>(nullable: true),
                    oficioTecnico = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tecnicoId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdenServicios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fechaRegistro = table.Column<DateTime>(nullable: false),
                    ClienteId = table.Column<Guid>(nullable: true),
                    tipo = table.Column<int>(nullable: false),
                    precio = table.Column<double>(nullable: false),
                    producto = table.Column<string>(nullable: false),
                    descripcion_servicio = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ordenServicioId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenServicios_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrdenServicioId = table.Column<Guid>(nullable: true),
                    fechaVisita = table.Column<DateTime>(nullable: false),
                    direccionCita = table.Column<string>(nullable: false),
                    descripcionCita = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("citaId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citas_OrdenServicios_OrdenServicioId",
                        column: x => x.OrdenServicioId,
                        principalTable: "OrdenServicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CitaTecnicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CitaId = table.Column<Guid>(nullable: true),
                    TecnicoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitaTecnicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CitaTecnicos_Citas_CitaId",
                        column: x => x.CitaId,
                        principalTable: "Citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CitaTecnicos_Tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Tecnicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResumenCitas",
                columns: table => new
                {
                    DetalleTrabajo = table.Column<string>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    CitaId = table.Column<Guid>(nullable: true),
                    fechaFormulario = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("detalleTrabajo", x => x.DetalleTrabajo);
                    table.ForeignKey(
                        name: "FK_ResumenCitas_Citas_CitaId",
                        column: x => x.CitaId,
                        principalTable: "Citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_OrdenServicioId",
                table: "Citas",
                column: "OrdenServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_CitaTecnicos_CitaId",
                table: "CitaTecnicos",
                column: "CitaId");

            migrationBuilder.CreateIndex(
                name: "IX_CitaTecnicos_TecnicoId",
                table: "CitaTecnicos",
                column: "TecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenServicios_ClienteId",
                table: "OrdenServicios",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumenCitas_CitaId",
                table: "ResumenCitas",
                column: "CitaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitaTecnicos");

            migrationBuilder.DropTable(
                name: "ResumenCitas");

            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "OrdenServicios");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
