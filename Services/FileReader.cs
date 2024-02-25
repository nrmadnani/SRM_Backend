using System.Globalization;
using CsvHelper;
using CsvHelper.Excel.EPPlus;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.ObjectPool;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using SRMWebApiApp.Data;
using SRMWebApiApp.Models;

namespace SRMWebApiApp.Services {
    public class FileReader : IFileReadService {
        private readonly IVP_3308_v3Context _context; 


        public FileReader(IVP_3308_v3Context context) {
            _context = context;
        }

        public async Task<CallSchedule> UploadData(FileStream file) {
// using (FileStream fileStream = new FileStream(@"C:\Users\ysbavishi\Documents\Case Study\Security Master\SRM_Backend\File\Data for securities.xlsx", FileMode.Open)){
    ExcelPackage excel = new ExcelPackage(file);
    ExcelWorksheet worksheet = excel.Workbook.Worksheets[0];
    // int colCount = worksheet.Dimension.End.Column;
    // int rowCount = worksheet.Dimension.End.Row;
    // Console.WriteLine(colCount.ToString());
    // Console.WriteLine(rowCount.ToString());
    // Console.WriteLine(worksheet.Cells[15,2].Value.ToString());
    // DateTime a = (DateTime) worksheet.Cells[2,57].Value;
    // Console.WriteLine(a.ToString());
    // var lastRow = worksheet.Cells.Where(cell => !string.IsNullOrEmpty(cell.Value?.ToString() ?? string.Empty)).LastOrDefault().End.Row;
    // Console.WriteLine(lastRow);
    /*
    for (int row = 1; row <= rowCount; row++)
        {
            for (int col = 1; col <= colCount; col++)
            {
                Console.WriteLine(" Row:" + row + " column:" + col + " Value:" + worksheet.Cells[row, col].Value.ToString().Trim());
            }
        }
    */

    // Trying to put call schedule
    ExcelWorksheet bondSheet = excel.Workbook.Worksheets[1];
    int equityColCount = worksheet.Dimension.End.Column;
    int equityRowRount = worksheet.Cells.Where(cell => !string.IsNullOrEmpty(cell.Value?.ToString() ?? string.Empty)).LastOrDefault().End.Row;
    int bondColCount = bondSheet.Dimension.End.Column;
    int bondRowCount = bondSheet.Cells.Where(cell => !string.IsNullOrEmpty(cell.Value?.ToString() ?? string.Empty)).LastOrDefault().End.Row;

    // Creating object

    // Inserting for SecuritySummary
    var resetSSId = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('SecuritySummary', RESEED, 0)");
    var resetSSEId = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('SecuritySummaryEquity', RESEED, 0)");
    var resetSSBId = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('SecuritySummaryBonds', RESEED, 0)");
    var resetSSINFO = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('SecurityDetailsEquity', RESEED, 0)");
    var resetSEID = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('SecurityIdentifier', RESEED, 0)");
    var resetER = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('EquityRisk', RESEED, 0)");
    var resetRegu = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('RegulatoryDetails', RESEED, 0)");
    var resetRef = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('ReferenceData', RESEED, 0)");
    var resetPrice =_context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('PricingDetails', RESEED, 0)"); 
    var resetDiv =_context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('DividendHistory', RESEED, 0)"); 
    var resetSSBondDe = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('SecurityDetailsBonds', RESEED, 0)");
    var resetBondRisk = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('BondRisk', RESEED, 0)");
    var resetCallSchedule = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('CallSchedule', RESEED, 0)");
    var resetPutSchedule = _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('PutSchedule', RESEED, 0)");

    for (int row = 2; row <= equityRowRount; row++)
        {
                var accessObject = new SecuritySummary();
                accessObject.SecurityName = worksheet.Cells[row, 3].Value.ToString() ?? "NA";
                accessObject.SecurityDescription = worksheet.Cells[row, 2].Value.ToString() ?? "NA";
                accessObject.SecurityType = "Equity";
                accessObject.HasPosition = bool.Parse(worksheet.Cells[row,4].Value.ToString());
                accessObject.IsActive = bool.Parse(worksheet.Cells[row, 5].Value.ToString());
                _context.Add(accessObject);
                await _context.SaveChangesAsync();
                int lotSize = Convert.ToInt32(worksheet.Cells[row,6].Value);
                var db = new SecuritySummaryEquity();
                db.SecId = row -1;
                db.RoundLotSize = lotSize;
                _context.Add(db);
                await _context.SaveChangesAsync();
                var secIdObj = new SecurityIdentifier();
                secIdObj.BloombergUniqueName = worksheet.Cells[row,7].Value.ToString() ?? null; 
                secIdObj.CUSIP = worksheet.Cells[row,8].Value.ToString() ?? null; 
                secIdObj.ISIN = worksheet.Cells[row,9].Value.ToString() ?? null; 
                secIdObj.SEDOL = worksheet.Cells[row,10].Value.ToString() ?? null; 
                secIdObj.BloombergTicker = worksheet.Cells[row,11].Value.ToString() ?? null; 
                secIdObj.BloombergUniqueId = worksheet.Cells[row,12].Value.ToString() ?? null; 
                secIdObj.BloombergGlobalId = worksheet.Cells[row,13].Value.ToString() ?? null; 
                secIdObj.BloombergTickerAndExchange = worksheet.Cells[row,14].Value.ToString() ?? null; 
                _context.Add(secIdObj);
                await _context.SaveChangesAsync();
                var secDetailsEquity = new SecurityDetailsEquity();
                string temp = worksheet.Cells[row,15].Value.ToString() ?? null;
                secDetailsEquity.isADR =  Boolean.Parse(temp ?? "false"); 
                secDetailsEquity.ADRUnderlyingTicker = (string?) worksheet.Cells[row,16].Value;
                secDetailsEquity.ADRUnderlyingCurrency = (string?) worksheet.Cells[row,17].Value;
                secDetailsEquity.SharesPerADR = (double?) worksheet.Cells[row, 18].Value;
                var tempo = (DateTime?) worksheet.Cells[row,19].Value ?? null;
                if (tempo is null) {
                    secDetailsEquity.IPODate = null;
                } else if(tempo.HasValue){
                    var templ = (DateTime) worksheet.Cells[row,19].Value;
                    secDetailsEquity.IPODate = (DateOnly) DateOnly.FromDateTime(templ);
                }
                secDetailsEquity.PriceCurrency = worksheet.Cells[row, 20].Value.ToString() ?? null;
                secDetailsEquity.SettleDays = Convert.ToInt32(worksheet.Cells[row, 21].Value.ToString() ?? null);
                temp = (string?)worksheet.Cells[row,21].Value;
                // secDetailsEquity.SharesOutstanding = Double.Parse(temp ?? null);
                secDetailsEquity.SharesOutstanding = (double?) worksheet.Cells[row,22].Value ?? null;
                // secDetailsEquity.VotingRightsPerShare = Convert.ToDouble(worksheet.Cells[row, 23].Value.ToString() ?? null);
                secDetailsEquity.VotingRightsPerShare = (double?) worksheet.Cells[row,23].Value ?? null; 
                _context.Add(secDetailsEquity);
                await _context.SaveChangesAsync();

                var equityRisk = new EquityRisk();
                equityRisk.AverageVolume20Day = (double?) worksheet.Cells[row,24].Value ?? null;
                equityRisk.Beta = (double?) worksheet.Cells[row,25].Value ?? null;
                equityRisk.ShortInterest = (double?) worksheet.Cells[row,26].Value ?? null;
                equityRisk.YTDReturn = (double?) worksheet.Cells[row,27].Value ?? null;
                equityRisk.PriceVolatility90Day = (double?) worksheet.Cells[row,28].Value ?? null;
                _context.Add(equityRisk);
                await _context.SaveChangesAsync();


                var regulatoryDetail = new RegulatoryDetail();
                regulatoryDetail.PFAssetClass = (string?) worksheet.Cells[row,29].Value ?? null;
                regulatoryDetail.PFCountry = (string?) worksheet.Cells[row,30].Value ?? null;
                regulatoryDetail.PFCreditRating = (string?) worksheet.Cells[row,31].Value ?? null;
                regulatoryDetail.PFCurrency = (string?) worksheet.Cells[row,32].Value ?? null;
                regulatoryDetail.PFInstrument = (string?) worksheet.Cells[row,33].Value ?? null;
                regulatoryDetail.PFLiquidityProfile = (string?) worksheet.Cells[row,34].Value ?? null;
                regulatoryDetail.PFMaturity = (string?) worksheet.Cells[row,35].Value ?? null;
                regulatoryDetail.PFNAICSCode = (string?) worksheet.Cells[row,36].Value ?? null;
                regulatoryDetail.PFRegion = (string?) worksheet.Cells[row,37].Value ?? null;
                regulatoryDetail.PFSector = (string?) worksheet.Cells[row,38].Value ?? null;
                regulatoryDetail.PFSubAssetClass = (string?) worksheet.Cells[row,39].Value ?? null;
                _context.Add(regulatoryDetail);
                await _context.SaveChangesAsync();
                //var securitySummaryEquity = _context.SecuritySummaryEquities.($"INSERT INTO SecuritySummaryEquity(SecId, RoundLotSize) values ({row-1}, {lotSize})",row, lotSize).AsEnumerable().First();

                var referenceData = new ReferenceDatum();
                referenceData.IssueCountry = (string?) worksheet.Cells[row, 40].Value ?? null;
                referenceData.Exchange = (string?) worksheet.Cells[row, 41].Value ?? null;
                referenceData.Issuer = (string?) worksheet.Cells[row, 42].Value ?? null;
                referenceData.IssueCurrency = (string?) worksheet.Cells[row, 43].Value ?? null;
                referenceData.TradingCurrency = (string?) worksheet.Cells[row, 44].Value ?? null;
                referenceData.BloombergIndustrySubGroup = (string?) worksheet.Cells[row, 45].Value ?? null;
                referenceData.BloombergIndustryGroup = (string?) worksheet.Cells[row, 46].Value ?? null;
                referenceData.BloombergSector = (string?) worksheet.Cells[row, 47].Value ?? null;
                referenceData.CountryOfIncorporation = (string?) worksheet.Cells[row, 48].Value ?? null;
                referenceData.RiskCurrency = (string?) worksheet.Cells[row, 49].Value ?? null;
                _context.Add(referenceData);
                await _context.SaveChangesAsync();


                var priceObj = new PricingDetail();
                priceObj.OpenPrice = (double?) worksheet.Cells[row,50].Value ?? null;
                priceObj.ClosePrice = (double?) worksheet.Cells[row,51].Value ?? null;
                var store = (double?) worksheet.Cells[row,52].Value ?? null;
                if (store == null) {
                    priceObj.Volume = null;
                } else {
                    int obj = Convert.ToInt32(store);
                    priceObj.Volume = obj;
                }
                // priceObj.Volume = (int?) worksheet.Cells[row,52].Value ?? null;
                priceObj.LastPrice = (double?) worksheet.Cells[row,53].Value ?? null;
                var storeDeci = (double ?) worksheet.Cells[row, 54].Value;
                if (storeDeci == null) {
                    priceObj.AskPrice = null;
                } else {
                    decimal x = (decimal) storeDeci;
                }
                // priceObj.AskPrice = (decimal?) worksheet.Cells[row,54].Value; 
                priceObj.BidPrice = (double?) worksheet.Cells[row,55].Value ?? null;
                priceObj.PERatio = (double?) worksheet.Cells[row,56].Value ?? null;
                _context.Add(priceObj);
                await _context.SaveChangesAsync();

                var dividendHistory = new DividendHistory();
                var dateStore = (DateTime?) worksheet.Cells[row,57].Value ?? null;
                if (dateStore is null) {
                    dividendHistory.DeclaredDate = null;
                } else if(dateStore.HasValue){
                    var templ = (DateTime) worksheet.Cells[row,57].Value;
                    dividendHistory.DeclaredDate = (DateOnly) DateOnly.FromDateTime(templ);
                }
                dateStore = (DateTime?) worksheet.Cells[row,58].Value ?? null;
                if (dateStore is null) {
                    dividendHistory.ExDate = null;
                } else if(dateStore.HasValue){
                    var templ = (DateTime) worksheet.Cells[row,58].Value;
                    dividendHistory.ExDate = (DateOnly) DateOnly.FromDateTime(templ);
                }
                dateStore = (DateTime?) worksheet.Cells[row,59].Value ?? null;
                if (dateStore is null) {
                    dividendHistory.RecordDate = null;
                } else if(dateStore.HasValue){
                    var templ = (DateTime) worksheet.Cells[row,59].Value;
                    dividendHistory.RecordDate = (DateOnly) DateOnly.FromDateTime(templ);
                }
                dateStore = (DateTime?) worksheet.Cells[row,60].Value ?? null;
                if (dateStore is null) {
                    dividendHistory.RecordDate = null;
                } else if(dateStore.HasValue){
                    var templ = (DateTime) worksheet.Cells[row,60].Value;
                    dividendHistory.RecordDate = (DateOnly) DateOnly.FromDateTime(templ);
                }
                dividendHistory.Amount = (double?) worksheet.Cells[row,61].Value ?? null;
                dividendHistory.Frequency = (int?) worksheet.Cells[row,62].Value ?? null;
                dividendHistory.DividendType = (string?) worksheet.Cells[row, 63].Value ?? null;
                _context.Add(dividendHistory);
                await _context.SaveChangesAsync();

            }
        // Time For Bonds 

        for (int row = 2; row <= bondRowCount; row++) {
            var bondSummary = new SecuritySummary();
            bondSummary.SecurityDescription = (string) bondSheet.Cells[row, 2].Value ?? null;
            bondSummary.SecurityName = (string) bondSheet.Cells[row, 3].Value ?? null;
            bondSummary.SecurityType = "Bond";
            bondSummary.HasPosition = (bool?) bondSheet.Cells[row, 30].Value ?? null;
            bondSummary.IsActive = null;
            _context.Add(bondSummary);
            await _context.SaveChangesAsync();

            var bondSpecificSummary = new SecuritySummaryBond();
            bondSpecificSummary.AssetType = (string) bondSheet.Cells[row, 4].Value;
            bondSpecificSummary.InvestmentType = (string) bondSheet.Cells[row,5].Value ?? null;
            bondSpecificSummary.TradingFactor = (double?) bondSheet.Cells[row,6].Value ?? null;
            bondSpecificSummary.PricingFactor = (double?) bondSheet.Cells[row,7].Value ?? null;
            _context.Add(bondSpecificSummary);
            await _context.SaveChangesAsync();

            var bondSecIdObj = new SecurityIdentifier();
            bondSecIdObj.ISIN = (string?) bondSheet.Cells[row,8].Value ?? null; 
            bondSecIdObj.BloombergTicker = (string?) bondSheet.Cells[row,9].Value ?? null; 
            bondSecIdObj.BloombergUniqueId = (string?) bondSheet.Cells[row,10].Value ?? null; 
            bondSecIdObj.CUSIP = (string?) bondSheet.Cells[row,11].Value ?? null; 
            bondSecIdObj.SEDOL = (string?) bondSheet.Cells[row,12].Value ?? null; 
            bondSecIdObj.BloombergUniqueName = null;
            bondSecIdObj.BloombergGlobalId = null;
            bondSecIdObj.BloombergTickerAndExchange = null;
            _context.Add(bondSecIdObj);
            await _context.SaveChangesAsync();
            Console.WriteLine("DDDDDDDDDDDDDDDDDDDDDDDDDDD");
            var bondSecDetails = new SecurityDetailsBond();
            // bondSecDetails = 
            Console.WriteLine(bondSheet.Cells[1,13].Value.ToString());
            var test = (double?) bondSheet.Cells[row,13].Value ?? null;
            if (test == null) {
                bondSecDetails.FirstCouponDate = null;
            } else {
                double sad = (double) test;
                DateTime dt = DateTime.FromOADate(sad);
                var dot = DateOnly.FromDateTime(dt);
                bondSecDetails.FirstCouponDate = dot;
            }
            bondSecDetails.CouponCap = (double?) bondSheet.Cells[row,14].Value ?? null;
            bondSecDetails.CouponFloor = (double?) bondSheet.Cells[row,15].Value ?? null;
            var coupFreq = (string?) bondSheet.Cells[row,16].Value ?? null;
            if (coupFreq == null) {
                bondSecDetails.MaximumCallNotificationDays = null;
            } else {
                int obj = Convert.ToInt32(coupFreq);
                bondSecDetails.MaximumPutNotificationDays = obj;
            }
            // bondSecDetails.CouponFrequency = (int?) bondSheet.Cells[row,16].Value ?? null;
            bondSecDetails.CouponRate = (double?) bondSheet.Cells[row,17].Value ?? null;
            bondSecDetails.CouponType = (string?) bondSheet.Cells[row,18].Value ?? null;
            Console.WriteLine("You Caught me");
            var isCall = (string?) bondSheet.Cells[row,20].Value ?? null;
            if (isCall == null) {
                bondSecDetails.IsCallable = null;
            } else {
                bool obj = Boolean.Parse(isCall);
                bondSecDetails.IsCallable = obj;
            }
            Console.WriteLine("I AM DONE");
            // bondSecDetails.IsCallable = (bool?) bondSheet.Cells[row,20].Value ?? null;
            var isFloat = (string?) bondSheet.Cells[row,21].Value ?? null;
            if (isFloat == null) {
                bondSecDetails.IsFixToFloat = null;
            } else {
                bool obj = Boolean.Parse(isFloat);
                bondSecDetails.IsFixToFloat = obj;
            }
            Console.WriteLine("I AM DONE TOOOO");
            // bondSecDetails.IsFixToFloat = (bool?) bondSheet.Cells[row,21].Value ?? null;
            var isPut = (string?) bondSheet.Cells[row,22].Value ?? null;
            if (isPut == null) {
                bondSecDetails.IsPutable = null;
            } else {
                bool obj = Boolean.Parse(isPut);
                bondSecDetails.IsPutable = obj;
            }
            // bondSecDetails.IsPutable = (bool?) bondSheet.Cells[row,22].Value ?? null;
            var issueDate = (double?) bondSheet.Cells[row,23].Value ?? null;
            if (issueDate == null) {
                bondSecDetails.IssueDate = null;
            } else {
                double sad = (double) issueDate;
                DateTime dt = DateTime.FromOADate(sad);
                var dot = DateOnly.FromDateTime(dt);
                bondSecDetails.IssueDate = dot;
            }
            var lastReset = (double?) bondSheet.Cells[row,24].Value ?? null;
            if (lastReset == null) {
                bondSecDetails.LastResetDate = null;
            } else {
                double sad = (double) lastReset;
                DateTime dt = DateTime.FromOADate(sad);
                var dot = DateOnly.FromDateTime(dt);
                bondSecDetails.LastResetDate = dot;
            }
            var maturityDate = (double?) bondSheet.Cells[row,25].Value ?? null;
            if (maturityDate == null) {
                bondSecDetails.MaturityDate = null;
            } else {
                double sad = (double) maturityDate;
                DateTime dt = DateTime.FromOADate(sad);
                var dot = DateOnly.FromDateTime(dt);
                bondSecDetails.MaturityDate = dot;
            }
            var penult = (double?) bondSheet.Cells[row,28].Value ?? null;
            if (penult == null) {
                bondSecDetails.PenultimateCouponDate = null;
            } else {
                double sad = (double) penult;
                DateTime dt = DateTime.FromOADate(sad);
                var dot = DateOnly.FromDateTime(dt);
                bondSecDetails.PenultimateCouponDate = dot;
            }
            var maxCall = (double?) bondSheet.Cells[row,26].Value ?? null;
            if (maxCall == null) {
                bondSecDetails.MaximumCallNotificationDays = null;
            } else {
                int obj = Convert.ToInt32(maxCall);
                bondSecDetails.MaximumCallNotificationDays = obj;
            }
            var maxPut = (double?) bondSheet.Cells[row,27].Value ?? null;
            if (maxPut == null) {
                bondSecDetails.MaximumPutNotificationDays = null;
            } else {
                int obj = Convert.ToInt32(maxPut);
                bondSecDetails.MaximumPutNotificationDays = obj;
            }
            var resetFreq = (double?) bondSheet.Cells[row,29].Value ?? null;
            if (resetFreq == null) {
                bondSecDetails.ResetFrequency = null;
            } else {
                int obj = Convert.ToInt32(resetFreq);
                bondSecDetails.MaximumPutNotificationDays = obj;
            }

            _context.Add(bondSecDetails);
            await _context.SaveChangesAsync();

            var bondRisk = new BondRisk();
            bondRisk.MacaulayDuration = (double?) bondSheet.Cells[row, 31].Value ?? null;
            bondRisk.Volatility30Day = (double?) bondSheet.Cells[row, 32].Value ?? null;
            bondRisk.Volatility90Day = (double?) bondSheet.Cells[row, 33].Value ?? null;
            bondRisk.Convexity = (double?) bondSheet.Cells[row, 34].Value ?? null;
            bondRisk.AverageVolume30Day = (double?) bondSheet.Cells[row, 35].Value ?? null;
            bondRisk.Spread = (double?) bondSheet.Cells[row, 34].Value ?? null;

            _context.Add(bondRisk);
            await _context.SaveChangesAsync();

            var bondReguDetail = new RegulatoryDetail();
            bondReguDetail.PFAssetClass = (string?) bondSheet.Cells[row, 36].Value ?? null; 
            bondReguDetail.PFCountry = (string?) bondSheet.Cells[row, 37].Value ?? null; 
            bondReguDetail.PFCreditRating = (string?) bondSheet.Cells[row, 38].Value ?? null; 
            bondReguDetail.PFCurrency = (string?) bondSheet.Cells[row, 39].Value ?? null; 
            bondReguDetail.PFInstrument = (string?) bondSheet.Cells[row, 40].Value ?? null; 
            bondReguDetail.PFLiquidityProfile = (string?) bondSheet.Cells[row, 41].Value ?? null; 
            bondReguDetail.PFMaturity = (string?) bondSheet.Cells[row, 42].Value ?? null; 
            bondReguDetail.PFNAICSCode = (string?) bondSheet.Cells[row, 43].Value ?? null; 
            bondReguDetail.PFRegion = (string?) bondSheet.Cells[row, 44].Value ?? null; 
            bondReguDetail.PFSector = (string?) bondSheet.Cells[row, 45].Value ?? null; 
            bondReguDetail.PFSubAssetClass = (string?) bondSheet.Cells[row, 46].Value ?? null; 

            _context.Add(bondReguDetail);
            await _context.SaveChangesAsync();

            var referData = new ReferenceDatum();
            referData.BloombergIndustryGroup = (string?) bondSheet.Cells[row, 47].Value ?? null;
            referData.BloombergIndustrySubGroup = (string?) bondSheet.Cells[row, 48].Value ?? null;
            referData.BloombergSector = (string?) bondSheet.Cells[row, 49].Value ?? null;
            referData.IssueCountry = (string?) bondSheet.Cells[row, 50].Value ?? null;
            referData.IssueCurrency = (string?) bondSheet.Cells[row, 51].Value ?? null;
            referData.Issuer = (string?) bondSheet.Cells[row, 52].Value ?? null;
            referData.RiskCurrency = (string?) bondSheet.Cells[row,53].Value ?? null;

            _context.Add(referData);
            await _context.SaveChangesAsync();

            var putobj = new PutSchedule();
            putobj.PutPrice = (double?) bondSheet.Cells[row,55].Value ?? null;
            var purDate = (double?) bondSheet.Cells[row,54].Value ?? null;
            if (purDate == null) {
                putobj.PutDate = null;
            } else {
                double sad = (double) purDate;
                DateTime dt = DateTime.FromOADate(sad);
                var dot = DateOnly.FromDateTime(dt);
                putobj.PutDate = dot;
            }
                _context.Add(putobj);
                await _context.SaveChangesAsync();

            var callobj = new CallSchedule();
            callobj.CallPrice = (double?) bondSheet.Cells[row,63].Value ?? null;
            var callDate = (double?) bondSheet.Cells[row,62].Value ?? null;
            if (callDate == null) {
                callobj.CallDate = null;
            } else {
                double sad = (double) callDate;
                DateTime dt = DateTime.FromOADate(sad);
                var dot = DateOnly.FromDateTime(dt);
                callobj.CallDate = dot;
            }
                _context.Add(callobj);
                await _context.SaveChangesAsync();

                var priceObj = new PricingDetail();
                var storeDeci = (double ?) bondSheet.Cells[row, 55].Value;
                if (storeDeci == null) {
                    priceObj.AskPrice = null;
                } else {
                    decimal x = (decimal) storeDeci;
                    priceObj.AskPrice = x;
                }
                priceObj.HighPrice = (double?) bondSheet.Cells[row, 56].Value ?? null;
                priceObj.LowPrice = (double?) bondSheet.Cells[row,57].Value ?? null;
                priceObj.OpenPrice = (double?) bondSheet.Cells[row,58].Value ?? null;
                var store = (double?) bondSheet.Cells[row,59].Value ?? null;
                if (store == null) {
                    priceObj.Volume = null;
                } else {
                    int obj = Convert.ToInt32(store);
                    priceObj.Volume = obj;
                }
                // priceObj.Volume = (int?) worksheet.Cells[row,52].Value ?? null;
                // priceObj.AskPrice = (decimal?) worksheet.Cells[row,54].Value; 
                priceObj.BidPrice = (double?) bondSheet.Cells[row,60].Value ?? null;
                priceObj.LastPrice = (double?) bondSheet.Cells[row,61].Value ?? null;
                _context.Add(priceObj);
                await _context.SaveChangesAsync();


            Console.WriteLine(test);
        }

/*
dotnet-ef dbcontext scaffold "Server=192.168.0.13\sqlexpress,49753;Database=IVP_3308_v3;TrustServerCertificate=True;user id=sa; password=sa@12345678" Microsoft.EntityFrameworkCore.SqlServer --context-dir Data --output-dir Models Tables SecuritySummaryEquity --data-annotations

*/

    return new CallSchedule();
        }
    }
}
    /*
    var something =   bondSheet.Cells[2,63].Text;
    DateOnly dat = DateOnly.Parse(something);
    var price = (double) bondSheet.Cells[2,64].Value;
    Console.WriteLine(price);
    var obj = new CallSchedule();
    obj.CallDate = dat;
    obj.CallPrice = price;
    _context.CallSchedules.Add(obj);
    await _context.SaveChangesAsync();
    // var blob = bondSheet.Cells[4,63] ?? string.Empty;
    if(bondSheet.Cells[4,63].Value == null) {
        Console.WriteLine("CONFIRMING GOD");
    } else {
        Console.WriteLine("DEVIl");
    };
    Console.WriteLine("Done");
    for (int row = 1; row <= bondRowCount; row++)
        {
            for (int col = 63; col <= bondColCount; col++)
            {
                Console.WriteLine(" Row:" + row + " column:" + col + " Value:" + bondSheet.Cells[row, col].Value.ToString().Trim());
            }
        }
    return obj;
    return new CallSchedule();
        }
    }
}

/*
using (var reader = new StreamReader(@"C:\Users\ysbavishi\Documents\Case Study\Security Master\SRM_Backend\File\20201231-20211231 S_P 500 Prices.csv"))
using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
    // var header = csv.ReadHeader();
    // Console.WriteLine(header);
    
    while(csv.Read()) {
        Console.WriteLine(csv.GetField<String>(1));
    }
}


using (var reader = new CsvReader(new ExcelParser(@"C:\Users\ysbavishi\Documents\Case Study\Security Master\SRM_Backend\File\Data for securities.xlsx"))){
    var header = reader.Read();
    Console.WriteLine(header);
}
*/



//}
