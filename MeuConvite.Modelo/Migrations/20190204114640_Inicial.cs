using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeuConvite.Modelo.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuracao",
                columns: table => new
                {
                    Chave = table.Column<string>(maxLength: 256, nullable: false),
                    Valor = table.Column<string>(maxLength: 256, nullable: true),
                    Secao = table.Column<string>(maxLength: 256, nullable: true),
                    Descricao = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracao", x => x.Chave);
                });

            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    Celular = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Convite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(maxLength: 256, nullable: true),
                    Mensagem = table.Column<string>(type: "text", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Presente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(maxLength: 256, nullable: true),
                    EhNecessario = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Usuario = table.Column<string>(maxLength: 256, nullable: false),
                    Senha = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Usuario);
                });

            migrationBuilder.CreateTable(
                name: "Convidado",
                columns: table => new
                {
                    ConviteId = table.Column<int>(nullable: false),
                    ContatoId = table.Column<int>(nullable: false),
                    PresenteId = table.Column<int>(nullable: false),
                    Situacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convidado", x => new { x.ConviteId, x.ContatoId });
                    table.ForeignKey(
                        name: "FK_Convidado_Contato_ContatoId",
                        column: x => x.ContatoId,
                        principalTable: "Contato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Convidado_Convite_ConviteId",
                        column: x => x.ConviteId,
                        principalTable: "Convite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Convidado_Presente_PresenteId",
                        column: x => x.PresenteId,
                        principalTable: "Presente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Convidado_ContatoId",
                table: "Convidado",
                column: "ContatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Convidado_PresenteId",
                table: "Convidado",
                column: "PresenteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuracao");

            migrationBuilder.DropTable(
                name: "Convidado");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Contato");

            migrationBuilder.DropTable(
                name: "Convite");

            migrationBuilder.DropTable(
                name: "Presente");
        }
    }
}
