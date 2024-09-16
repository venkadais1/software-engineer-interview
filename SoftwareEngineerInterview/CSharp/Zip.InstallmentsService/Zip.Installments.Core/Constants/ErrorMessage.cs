namespace Zip.Installments.Core.Constants
{
    /// <summary>
    ///     Constants for fluent validation error messages
    /// </summary>
    public sealed class ErrorMessage
    {
        /// <summary>
        ///     Property is invalid due to basic validation like null or empty
        /// </summary>
        public const string InvalidProperty = "Invalid {PropertyName}";

        /// <summary>
        ///     Name valid property in range
        /// </summary>
        public const string InvalidPropertyLength = "Invalid {PropertyName} or it's length";
    }
}
