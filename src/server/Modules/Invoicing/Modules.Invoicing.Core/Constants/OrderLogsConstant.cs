namespace FluentPOS.Modules.Invoicing.Core.Constants
{
    public static class OrderLogsConstant
    {
        public const string Webhook = "Order webhook recieved to e-smart retail at {0}";
        public const string WebhookStarted = "Job started to import new orders at {0}";
        public const string NewOrderSaved = "New Order {1} saved at {0}";
        public const string NewOrderFailed = "Order {1} failed while importing at {0}";
        public const string ProcessOrderStart = "Process order {1} started at {0}";
        public const string CityCorrection = "Assigned to city correction at {0}";
        public const string CityCorrectionFailed = "Failed to assign city correction at {0}";
        public const string SearchingForAvailableQty = "Searching for warehouse with valid qty at {0}";
        public const string LastWHSkiiped = "Previously assigned warehouse skipped at {0}";
        public const string DistanceCalculated = "Distance calculated at {0}";
        public const string SignleStoreFound = "This can be fulfillment by single store ";
        public const string CheckingForMultipleStore = "Checking for multiple store to fulfill this order";
        public const string CheckingForSingleProductMultipleStore = "Checking if single product can fulfilled by multiple stores";
        public const string AssignedToOutlet = "outlet  assigned to this order at {0}";
        public const string SplitOrder = "Splitting order and assigned to outlet ";
        public const string SplitOrderFailed = "Splitting order failed for outlet ";
    }
}
