using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmpleadoArea",
                columns: table => new
                {
                    EmpleadoAreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoArea", x => x.EmpleadoAreaId);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoCargo",
                columns: table => new
                {
                    EmpleadoCargoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoCargo", x => x.EmpleadoCargoId);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoEstado",
                columns: table => new
                {
                    EmpleadoEstadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoEstado", x => x.EmpleadoEstadoId);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoTipo",
                columns: table => new
                {
                    EmpleadoTipoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoTipo", x => x.EmpleadoTipoId);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NSS = table.Column<int>(type: "int", nullable: false),
                    NumeroCedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimerNombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalarioOrdinario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroCuentaBanco = table.Column<int>(type: "int", nullable: false),
                    EmpleadoTipoId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoAreaId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoCargoId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoEstadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.EmpleadoId);
                    table.ForeignKey(
                        name: "FK_Empleado_EmpleadoArea_EmpleadoAreaId",
                        column: x => x.EmpleadoAreaId,
                        principalTable: "EmpleadoArea",
                        principalColumn: "EmpleadoAreaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleado_EmpleadoCargo_EmpleadoCargoId",
                        column: x => x.EmpleadoCargoId,
                        principalTable: "EmpleadoCargo",
                        principalColumn: "EmpleadoCargoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleado_EmpleadoEstado_EmpleadoEstadoId",
                        column: x => x.EmpleadoEstadoId,
                        principalTable: "EmpleadoEstado",
                        principalColumn: "EmpleadoEstadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleado_EmpleadoTipo_EmpleadoTipoId",
                        column: x => x.EmpleadoTipoId,
                        principalTable: "EmpleadoTipo",
                        principalColumn: "EmpleadoTipoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_EmpleadoAreaId",
                table: "Empleado",
                column: "EmpleadoAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_EmpleadoCargoId",
                table: "Empleado",
                column: "EmpleadoCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_EmpleadoEstadoId",
                table: "Empleado",
                column: "EmpleadoEstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_EmpleadoTipoId",
                table: "Empleado",
                column: "EmpleadoTipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "EmpleadoArea");

            migrationBuilder.DropTable(
                name: "EmpleadoCargo");

            migrationBuilder.DropTable(
                name: "EmpleadoEstado");

            migrationBuilder.DropTable(
                name: "EmpleadoTipo");
        }
    }
}
