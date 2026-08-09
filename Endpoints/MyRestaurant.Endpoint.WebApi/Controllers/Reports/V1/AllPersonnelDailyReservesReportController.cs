//using Microsoft.AspNetCore.Mvc;
//using MyRestaurant.Application.Query.Contracts.Reports.AllPersonnelDailyReserves;
//using MyRestaurant.Application.Query.Reports;
//using Stimulsoft.Report;
//using Stimulsoft.Report.React;

//namespace MyRestaurant.Api.Controllers.Reports;

//[ApiController]
//[Route("api/reports/daily-reserves")]
//public class AllPersonnelDailyReservesReportController(AllPersonnelDailyReservesReportService reportService) : ControllerBase
//{

//    [HttpPost("viewer/{action}")]
//    public IActionResult ViewerData()
//    {
//        var requestParams = StiReactViewer.GetRequestParams(this);

//        return StiReactViewer.ViewerDataResult(requestParams);
//    }

//    [HttpPost("viewer/GetReport")]
//    public async Task<IActionResult> GetReport([FromQuery] DateTimeOffset date, [FromQuery] long mealPeriodId, CancellationToken cancellationToken)
//    {
//        var parameters = new AllPersonnelDailyReservesReportParams
//        {
//            Date = date,
//            MealPeriodId = mealPeriodId
//        };

//        var report = await reportService.CreateReportAsync(parameters, cancellationToken);

//        return StiReactViewer.GetReportResult(this, report);
//    }

//    [HttpPost("viewer/ViewerEvent")]
//    public IActionResult ViewerEvent()
//    {
//        return StiReactViewer.ViewerEventResult(this);
//    }
//}