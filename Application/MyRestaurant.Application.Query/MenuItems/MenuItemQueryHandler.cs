using MyRestaurant.Application.Query.Contracts.MenuItems;
using MyRestaurant.EF.Read.Repositories.MenuItems;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.MenuItems
{
    internal class MenuItemQueryHandler(IMenuItemQueryRepository repository) :
        IQueryHandler<GetMenuItemFormDataQuery, MenuItemFormData>,
        IQueryHandler<GetMenuItemsQuery, IEnumerable<MenuItemQueryResult>>
    {
        public async Task<MenuItemFormData> Handle(GetMenuItemFormDataQuery request, CancellationToken cancellationToken)
        {
            var menuItem = await repository.GetById(request.Id, cancellationToken);
            return menuItem;
        }

        public async Task<IEnumerable<MenuItemQueryResult>> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAll(cancellationToken);
        }
    }
}
