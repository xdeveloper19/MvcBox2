using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlarmTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensorTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartBoxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CloudKey = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartBoxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VariableGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 45, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Function = table.Column<string>(maxLength: 45, nullable: true),
                    VariableGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariableGroups_VariableGroups_VariableGroupId",
                        column: x => x.VariableGroupId,
                        principalTable: "VariableGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Acknowledge = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    AcknowledgedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ReleasedAt = table.Column<DateTime>(nullable: false),
                    AlarmTypeId = table.Column<Guid>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alarms_AlarmTypes_AlarmTypeId",
                        column: x => x.AlarmTypeId,
                        principalTable: "AlarmTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Alarms_SmartBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false),
                    EventTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_SmartBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Altitude = table.Column<double>(nullable: true),
                    CurrentDate = table.Column<DateTime>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_SmartBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_SmartBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 45, nullable: true),
                    Address = table.Column<string>(maxLength: 64, nullable: true),
                    Bus = table.Column<string>(maxLength: 64, nullable: true),
                    SensorTypeId = table.Column<Guid>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_SmartBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sensors_SensorTypes_SensorTypeId",
                        column: x => x.SensorTypeId,
                        principalTable: "SensorTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserHasAccesses",
                columns: table => new
                {
                    OwnerId = table.Column<Guid>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHasAccesses", x => new { x.BoxId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_UserHasAccesses_SmartBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Variables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Value = table.Column<string>(maxLength: 512, nullable: true),
                    DefaultValue = table.Column<string>(maxLength: 512, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Modifiable = table.Column<bool>(nullable: false),
                    VariableGroupId = table.Column<Guid>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variables_SmartBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Variables_VariableGroups_VariableGroupId",
                        column: x => x.VariableGroupId,
                        principalTable: "VariableGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SensorDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    SensorName = table.Column<string>(nullable: true),
                    SensorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorDatas_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModbusVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Function = table.Column<int>(nullable: false),
                    Sign = table.Column<int>(nullable: false),
                    Address = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 45, nullable: true),
                    Size = table.Column<int>(nullable: false),
                    Trackable = table.Column<bool>(nullable: false),
                    PollingTime = table.Column<int>(nullable: false),
                    VariableId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModbusVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModbusVariables_Variables_VariableId",
                        column: x => x.VariableId,
                        principalTable: "Variables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VariableNotifies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VariableId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableNotifies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariableNotifies_Variables_VariableId",
                        column: x => x.VariableId,
                        principalTable: "Variables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_AlarmTypeId",
                table: "Alarms",
                column: "AlarmTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_BoxId",
                table: "Alarms",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_BoxId",
                table: "Events",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_BoxId",
                table: "Locations",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Media_BoxId",
                table: "Media",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_ModbusVariables_VariableId",
                table: "ModbusVariables",
                column: "VariableId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorDatas_SensorId",
                table: "SensorDatas",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_BoxId",
                table: "Sensors",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_SensorTypeId",
                table: "Sensors",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VariableGroups_VariableGroupId",
                table: "VariableGroups",
                column: "VariableGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VariableNotifies_VariableId",
                table: "VariableNotifies",
                column: "VariableId");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_BoxId",
                table: "Variables",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_VariableGroupId",
                table: "Variables",
                column: "VariableGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarms");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "ModbusVariables");

            migrationBuilder.DropTable(
                name: "SensorDatas");

            migrationBuilder.DropTable(
                name: "UserHasAccesses");

            migrationBuilder.DropTable(
                name: "VariableNotifies");

            migrationBuilder.DropTable(
                name: "AlarmTypes");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Variables");

            migrationBuilder.DropTable(
                name: "SensorTypes");

            migrationBuilder.DropTable(
                name: "SmartBoxes");

            migrationBuilder.DropTable(
                name: "VariableGroups");
        }
    }
}
