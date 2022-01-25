namespace Clients.Infrastructure.Routes.ShoppingCart
{
    public static class ShoppingCartEndpoints
    {
        public static string GetCart = "api/v1/shopping/shoppingcarts/get-shopping-cart";
        public static string Save = "api/v1/shopping/shoppingcarts/add-edit";
        public static string Delete = "api/v1/shopping/shoppingcarts/delete";
        public static string RemoveFromCart = "api/v1/shopping/shoppingcarts/remove-from-cart";
    }
}
