using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations.SmartBox
{
    public partial class UpdateBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatteryPower",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "BoxState",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "IsOpenedBox",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "IsOpenedDoor",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "Light",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "Wetness",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "SignalLevel",
                table: "Locations");

            migrationBuilder.AddColumn<string>(
                name: "CloudKey",
                table: "SmartBoxes",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DriverId",
                table: "SmartBoxes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "SmartBoxes",
                nullable: true);

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
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<string>(maxLength: 45, nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    BoxCount = table.Column<int>(nullable: false),
                    BoxState = table.Column<int>(nullable: false),
                    IsBusy = table.Column<bool>(nullable: false),
                    AccountId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 45, nullable: true),
                    Message = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_SmartBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    PayStatus = table.Column<int>(nullable: false),
                    PaidAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    HRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
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
                name: "UserHasAccesses",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHasAccesses", x => new { x.BoxId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserHasAccesses_SmartBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id");
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
                name: "DriverHasBoxes",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverHasBoxes", x => new { x.BoxId, x.DriverId });
                    table.ForeignKey(
                        name: "FK_DriverHasBoxes_SmartBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DriverHasBoxes_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InceptionAdress = table.Column<string>(nullable: true),
                    InceptionLatitude = table.Column<double>(nullable: false),
                    InceptionLongitude = table.Column<double>(nullable: false),
                    DestinationAddress = table.Column<string>(nullable: true),
                    DestinationLatitude = table.Column<double>(nullable: false),
                    DestinationLongitude = table.Column<double>(nullable: false),
                    Length = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ShipmentTime = table.Column<string>(nullable: true),
                    LoadMethod = table.Column<string>(nullable: true),
                    CargoType = table.Column<string>(nullable: true),
                    CargoClass = table.Column<string>(nullable: true),
                    Distance = table.Column<double>(nullable: false),
                    Insurance = table.Column<double>(nullable: false),
                    PaymentId = table.Column<Guid>(nullable: false),
                    OrderStageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStages_OrderStageId",
                        column: x => x.OrderStageId,
                        principalTable: "OrderStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
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
                name: "OrderHasBoxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BoxId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    IsBusy = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHasBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHasBoxes_SmartBoxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "SmartBoxes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderHasBoxes_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderStageLogs",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    OrderStageId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStageLogs", x => new { x.OrderId, x.OrderStageId });
                    table.ForeignKey(
                        name: "FK_OrderStageLogs_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderStageLogs_OrderStages_OrderStageId",
                        column: x => x.OrderStageId,
                        principalTable: "OrderStages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    WayPoints = table.Column<string>(maxLength: 255, nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    DriverId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    AbortedBy = table.Column<string>(nullable: true),
                    AbortedAt = table.Column<DateTime>(nullable: false),
                    DoneAt = table.Column<DateTime>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    TaskType = table.Column<string>(maxLength: 45, nullable: true),
                    TaskPriority = table.Column<string>(maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserHasOrders",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHasOrders", x => new { x.OrderId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserHasOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
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
                name: "IX_DriverHasBoxes_DriverId",
                table: "DriverHasBoxes",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_BoxId",
                table: "Events",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_ModbusVariables_VariableId",
                table: "ModbusVariables",
                column: "VariableId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHasBoxes_BoxId",
                table: "OrderHasBoxes",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHasBoxes_OrderId",
                table: "OrderHasBoxes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStageId",
                table: "Orders",
                column: "OrderStageId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStageLogs_OrderStageId",
                table: "OrderStageLogs",
                column: "OrderStageId");

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
                name: "IX_Tasks_DriverId",
                table: "Tasks",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OrderId",
                table: "Tasks",
                column: "OrderId");

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
                name: "DriverHasBoxes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "ModbusVariables");

            migrationBuilder.DropTable(
                name: "OrderHasBoxes");

            migrationBuilder.DropTable(
                name: "OrderStageLogs");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "SensorDatas");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "UserHasAccesses");

            migrationBuilder.DropTable(
                name: "UserHasOrders");

            migrationBuilder.DropTable(
                name: "VariableNotifies");

            migrationBuilder.DropTable(
                name: "AlarmTypes");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Variables");

            migrationBuilder.DropTable(
                name: "SensorTypes");

            migrationBuilder.DropTable(
                name: "OrderStages");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "VariableGroups");

            migrationBuilder.DropColumn(
                name: "CloudKey",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "SmartBoxes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "SmartBoxes");

            migrationBuilder.AddColumn<double>(
                name: "BatteryPower",
                table: "SmartBoxes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "BoxState",
                table: "SmartBoxes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SmartBoxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOpenedBox",
                table: "SmartBoxes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOpenedDoor",
                table: "SmartBoxes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Light",
                table: "SmartBoxes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "SmartBoxes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "SmartBoxes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Wetness",
                table: "SmartBoxes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SignalLevel",
                table: "Locations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
