using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Armors",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Type = table.Column<string>(maxLength: 128, nullable: false),
                    BaseAc = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    ValueInGp = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    StealthDisadvantage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DndCharacters",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Level = table.Column<int>(nullable: true),
                    GoldPieces = table.Column<int>(nullable: false),
                    PlatinumPieces = table.Column<int>(nullable: false),
                    SilverPieces = table.Column<int>(nullable: false),
                    CopperPieces = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DndCharacters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MagicalItems",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Spell = table.Column<string>(maxLength: 128, nullable: false),
                    MaxCharges = table.Column<int>(nullable: false),
                    CurrentCharges = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagicalItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtherEquipments",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    ValueInGp = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherEquipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    AttackType = table.Column<string>(maxLength: 128, nullable: false),
                    WeaponType = table.Column<string>(maxLength: 128, nullable: false),
                    WeaponSize = table.Column<string>(maxLength: 128, nullable: false),
                    ToHit = table.Column<string>(maxLength: 128, nullable: false),
                    Damage = table.Column<string>(maxLength: 128, nullable: false),
                    Range = table.Column<int>(nullable: false),
                    ValueInGp = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Silvered = table.Column<bool>(nullable: false),
                    Finesse = table.Column<bool>(nullable: false),
                    TwoHanded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharactersArmors",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: true),
                    ArmorId = table.Column<string>(maxLength: 36, nullable: false),
                    DndCharacterId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharactersArmors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharactersArmors_Armors_ArmorId",
                        column: x => x.ArmorId,
                        principalTable: "Armors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharactersArmors_DndCharacters_DndCharacterId",
                        column: x => x.DndCharacterId,
                        principalTable: "DndCharacters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharactersMagicalItems",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: true),
                    MagicalItemId = table.Column<string>(maxLength: 36, nullable: false),
                    DndCharacterId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharactersMagicalItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharactersMagicalItems_DndCharacters_DndCharacterId",
                        column: x => x.DndCharacterId,
                        principalTable: "DndCharacters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharactersMagicalItems_MagicalItems_MagicalItemId",
                        column: x => x.MagicalItemId,
                        principalTable: "MagicalItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharactersEquipments",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: true),
                    EquipmentId = table.Column<string>(maxLength: 36, nullable: false),
                    DndCharacterId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharactersEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharactersEquipments_DndCharacters_DndCharacterId",
                        column: x => x.DndCharacterId,
                        principalTable: "DndCharacters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharactersEquipments_OtherEquipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "OtherEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharactersWeapons",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: true),
                    WeaponId = table.Column<string>(maxLength: 36, nullable: false),
                    DndCharacterId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharactersWeapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharactersWeapons_DndCharacters_DndCharacterId",
                        column: x => x.DndCharacterId,
                        principalTable: "DndCharacters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharactersWeapons_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharactersArmors_ArmorId",
                table: "CharactersArmors",
                column: "ArmorId");

            migrationBuilder.CreateIndex(
                name: "IX_CharactersArmors_DndCharacterId",
                table: "CharactersArmors",
                column: "DndCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharactersEquipments_DndCharacterId",
                table: "CharactersEquipments",
                column: "DndCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharactersEquipments_EquipmentId",
                table: "CharactersEquipments",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CharactersMagicalItems_DndCharacterId",
                table: "CharactersMagicalItems",
                column: "DndCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharactersMagicalItems_MagicalItemId",
                table: "CharactersMagicalItems",
                column: "MagicalItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CharactersWeapons_DndCharacterId",
                table: "CharactersWeapons",
                column: "DndCharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharactersWeapons_WeaponId",
                table: "CharactersWeapons",
                column: "WeaponId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CharactersArmors");

            migrationBuilder.DropTable(
                name: "CharactersEquipments");

            migrationBuilder.DropTable(
                name: "CharactersMagicalItems");

            migrationBuilder.DropTable(
                name: "CharactersWeapons");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Armors");

            migrationBuilder.DropTable(
                name: "OtherEquipments");

            migrationBuilder.DropTable(
                name: "MagicalItems");

            migrationBuilder.DropTable(
                name: "DndCharacters");

            migrationBuilder.DropTable(
                name: "Weapons");
        }
    }
}
