namespace Exam.Application.Permissions
{
    public static class PermissionConstants
    {
        public static class Restaurants
        {
            public const string View = "Permissions.Restaurants.View";
            public const string Manage = "Permissions.Restaurants.Manage";
        }

        public static class Menu
        {
            public const string View = "Permissions.Menu.View";
            public const string Manage = "Permissions.Menu.Manage";
        }

        public static class Orders
        {
            public const string View = "Permissions.Orders.View";
            public const string Create = "Permissions.Orders.Create";
            public const string Manage = "Permissions.Orders.Manage";
        }

        public static class Couriers
        {
            public const string View = "Permissions.Couriers.View";
            public const string Manage = "Permissions.Couriers.Manage";
        }
    }
}
