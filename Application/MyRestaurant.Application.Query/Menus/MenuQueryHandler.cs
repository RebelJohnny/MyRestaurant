using MyRestaurant.Application.Query.Contracts.Menus;
using MyRestaurant.EF.Read.Repositories.Menus;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Menus
{
    internal class MenuQueryHandler(IMenuQueryRepository repository) :
        IQueryHandler<GetMenuQuery, IEnumerable<MenuQueryResult>>
    {
        public Task<IEnumerable<MenuQueryResult>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
