namespace VehicleShowroom.Management.Application.Utils
{
    public static class Constant
    {
        //STOCK
        public const string STOCKIN = "Stock_In";
        public const string STOCKOUT = "Stock_Out";
        // STATUS
        public const string PENDING = "Pending";
        public const string COMPLETED = "Completed";
        public const string PAID = "Paid";
        public const string FAILED = "Failed";
        // PAYMENT
        public const string NOTE_AWAIT = "Awaiting payment";
        public const string NOTE_PAID = "Payment processed successfully";
        public const string NOTE_FAILED = "Transfer failed";

        // Success messages
        public const string CreateSuccess = "Successfully created!";
        public const string UpdateSuccess = "Successfully updated!";
        public const string DeleteSuccess = "Successfully deleted!";

        // Error messages
        public const string CreateError = "Failed to create, please try again.";
        public const string UpdateError = "Failed to update, please try again.";
        public const string DeleteError = "Failed to delete, please try again.";

        // General messages
        public const string NotFound = "Data not found.";
        public const string InvalidData = "Invalid data.";
        public const string OperationFailed = "Operation failed. Please try again!";
        public const string InvalidForm = "Invalid data. Please correct the errors and try again.";
    }
}
