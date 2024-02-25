using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRMWebApiApp.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityFlagTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveFlags",
                columns: table => new
                {
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "BondRisk",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MacaulayDuration = table.Column<double>(type: "float", nullable: true),
                    Volatility30Day = table.Column<double>(type: "float", nullable: true),
                    Volatility90Day = table.Column<double>(type: "float", nullable: true),
                    Convexity = table.Column<double>(type: "float", nullable: true),
                    AverageVolume30Day = table.Column<double>(type: "float", nullable: true),
                    Spread = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BondRisk__CA195970B44F9D05", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "CallSchedule",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CallDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CallPrice = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CallSche__CA1959707AB96237", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "DividendHistory",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeclaredDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RecordDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PayDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Frequency = table.Column<int>(type: "int", nullable: true),
                    DividendType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dividend__CA1959706EB74C10", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "EquityRisk",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AverageVolume20Day = table.Column<double>(type: "float", nullable: true),
                    Beta = table.Column<double>(type: "float", nullable: true),
                    ShortInterest = table.Column<double>(type: "float", nullable: true),
                    YTDReturn = table.Column<double>(type: "float", nullable: true),
                    PriceVolatility90Day = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EquityRi__CA195970673DECF0", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "PricingDetails",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AskPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Volume = table.Column<int>(type: "int", nullable: true),
                    HighPrice = table.Column<double>(type: "float", nullable: true),
                    LowPrice = table.Column<double>(type: "float", nullable: true),
                    OpenPrice = table.Column<double>(type: "float", nullable: true),
                    ClosePrice = table.Column<double>(type: "float", nullable: true),
                    BidPrice = table.Column<double>(type: "float", nullable: true),
                    LastPrice = table.Column<double>(type: "float", nullable: true),
                    PERatio = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PricingD__CA19597015ECF495", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "PutSchedule",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PutDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PutPrice = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PutSched__CA1959700698C3CC", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "RegulatoryDetails",
                columns: table => new
                {
                    PFId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PFAssetClass = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PFCountry = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PFCreditRating = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PFCurrency = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PFInstrument = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PFLiquidityProfile = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PFMaturity = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PFNAICSCode = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PFRegion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PFSector = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PFSubAssetClass = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Regulato__5944FB171D4EB8EE", x => x.PFId);
                });

            migrationBuilder.CreateTable(
                name: "SecurityDetailsBonds",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstCouponDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CouponCap = table.Column<double>(type: "float", nullable: true),
                    CouponFloor = table.Column<double>(type: "float", nullable: true),
                    CouponFrequency = table.Column<int>(type: "int", nullable: true),
                    CouponRate = table.Column<double>(type: "float", nullable: true),
                    CouponType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    IsCallable = table.Column<bool>(type: "bit", nullable: true),
                    IsFixToFloat = table.Column<bool>(type: "bit", nullable: true),
                    IsPutable = table.Column<bool>(type: "bit", nullable: true),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LastResetDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MaturityDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PenultimateCouponDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MaximumCallNotificationDays = table.Column<int>(type: "int", nullable: true),
                    MaximumPutNotificationDays = table.Column<int>(type: "int", nullable: true),
                    ResetFrequency = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Security__CA1959704061764D", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "SecurityDetailsEquity",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isADR = table.Column<bool>(type: "bit", nullable: true),
                    ADRUnderlyingTicker = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ADRUnderlyingCurrency = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    SharesPerADR = table.Column<double>(type: "float", nullable: true),
                    IPODate = table.Column<DateOnly>(type: "date", nullable: true),
                    PriceCurrency = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    SettleDays = table.Column<int>(type: "int", nullable: true),
                    SharesOutstanding = table.Column<double>(type: "float", nullable: true),
                    VotingRightsPerShare = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Security__CA1959702C48CD26", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "SecurityIdentifier",
                columns: table => new
                {
                    SIdentifier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUSIP = table.Column<string>(type: "char(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: true),
                    ISIN = table.Column<string>(type: "char(15)", unicode: false, fixedLength: true, maxLength: 15, nullable: true),
                    SEDOL = table.Column<string>(type: "char(7)", unicode: false, fixedLength: true, maxLength: 7, nullable: true),
                    BloombergTicker = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    BloombergUniqueId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    BloombergUniqueName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValue: "NA"),
                    BloombergGlobalId = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: true, defaultValue: "NA"),
                    BloombergTickerAndExchange = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Security__1E4E6088653FBF10", x => x.SIdentifier);
                });

            migrationBuilder.CreateTable(
                name: "SecuritySummary",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecurityType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    SecurityName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SecurityDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    HasPosition = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Security__CA19597060D594BF", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceData",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: true),
                    IssueCountry = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Exchange = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Issuer = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IssueCurrency = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    TradingCurrency = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    BloombergIndustrySubGroup = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    BloombergIndustryGroup = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    BloombergSector = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CountryOfIncorporation = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RiskCurrency = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_SID_ReferenceData",
                        column: x => x.SID,
                        principalTable: "SecuritySummary",
                        principalColumn: "SID");
                });

            migrationBuilder.CreateTable(
                name: "SecuritySummaryBonds",
                columns: table => new
                {
                    SecId = table.Column<int>(type: "int", nullable: true),
                    InvestmentType = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    TradingFactor = table.Column<double>(type: "float", nullable: true),
                    PricingFactor = table.Column<double>(type: "float", nullable: true),
                    AssetType = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_SecId_SecuritySummaryBonds",
                        column: x => x.SecId,
                        principalTable: "SecuritySummary",
                        principalColumn: "SID");
                });

            migrationBuilder.CreateTable(
                name: "SecuritySummaryEquity",
                columns: table => new
                {
                    SecId = table.Column<int>(type: "int", nullable: true),
                    RoundLotSize = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_SecId_SecuritySummaryEquity",
                        column: x => x.SecId,
                        principalTable: "SecuritySummary",
                        principalColumn: "SID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceData_SID",
                table: "ReferenceData",
                column: "SID");

            migrationBuilder.CreateIndex(
                name: "IX_SecuritySummaryBonds_SecId",
                table: "SecuritySummaryBonds",
                column: "SecId");

            migrationBuilder.CreateIndex(
                name: "IX_SecuritySummaryEquity_SecId",
                table: "SecuritySummaryEquity",
                column: "SecId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveFlags");

            migrationBuilder.DropTable(
                name: "BondRisk");

            migrationBuilder.DropTable(
                name: "CallSchedule");

            migrationBuilder.DropTable(
                name: "DividendHistory");

            migrationBuilder.DropTable(
                name: "EquityRisk");

            migrationBuilder.DropTable(
                name: "PricingDetails");

            migrationBuilder.DropTable(
                name: "PutSchedule");

            migrationBuilder.DropTable(
                name: "ReferenceData");

            migrationBuilder.DropTable(
                name: "RegulatoryDetails");

            migrationBuilder.DropTable(
                name: "SecurityDetailsBonds");

            migrationBuilder.DropTable(
                name: "SecurityDetailsEquity");

            migrationBuilder.DropTable(
                name: "SecurityIdentifier");

            migrationBuilder.DropTable(
                name: "SecuritySummaryBonds");

            migrationBuilder.DropTable(
                name: "SecuritySummaryEquity");

            migrationBuilder.DropTable(
                name: "SecuritySummary");
        }
    }
}
