using MyRestaurant.Application.Query.Contracts.Reports.AllPersonnelDailyReserves;
using MyRestaurant.EF.Read.Repositories.Meals;
using MyRestaurant.EF.Read.Repositories.Personnels;
using Stimulsoft.Report;

namespace MyRestaurant.Application.Query.Reports
{
    public sealed class AllPersonnelDailyReservesReportService(IPersonnelQueryRepository repository, IMealQueryRepository mealRepository, IWebHostEnvironment environment)
    {
        public async Task<StiReport> CreateReportAsync(AllPersonnelDailyReservesReportParams parameters, CancellationToken cancellationToken)
        {
            //apply validations here

            var rows = await repository.GetReservesOnDayMealPeriod(parameters.Date, parameters.MealPeriodId, cancellationToken);
            var report = new StiReport();
            var reportPath = Path.Combine(environment.ContentRootPath, "Application", "MyRestaurant.Application.Query.Contracts", "Reports", "AllPersonnelDailyReserves", "AllPersonnelDailyReservesReport.mrt");
            report.Load(reportPath);
            report.RegBusinessObject("AllDailyReserves", rows);
            report.Dictionary.SynchronizeBusinessObjects();
            return report;
        }
    }
}
