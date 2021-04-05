namespace Service.Utils
{
    public static class TranslationKeys
    {
        public static class Orders
        {
            public const string OrderIsNotFoundWithGivenOrderId = "order_is_not_found_with_given_orderId";
            public const string OrderIsAlreadyExistWithGivenOrderId = "order_is_already_exist_with_given_orderId";
        }

        public static class Products
        {
            public const string ProductIsNotFoundWithGivenUnitTypes = "product_is_not_found_with_given_unit_types";
        }

        public static class User
        {
            public const string NotFound = "user_is_not_found";
            public const string UserHasNoClaim = "user_has_no_claim";
            public const string UserIsNotActive = "user_is_not_active";
        }

        public static class Db
        {
            public const string InternalServerError = "internal_server_error";
        }
    }
}
