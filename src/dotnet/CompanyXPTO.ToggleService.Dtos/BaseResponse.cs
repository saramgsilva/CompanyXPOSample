namespace CompanyXPTO.ToggleService.Dtos
{
    /// <summary>
    /// Defines the base response from platform.
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the message. The Message can be related with ErrorCode and it allow custom message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the error code. The code can be related with business's error, validation's error from platform,....
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public string ErrorCode { get; set; }
    }
}