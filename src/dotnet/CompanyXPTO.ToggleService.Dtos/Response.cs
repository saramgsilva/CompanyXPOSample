namespace CompanyXPTO.ToggleService.Dtos
{
    /// <summary>
    /// Defines a generic response from platform.
    /// </summary>
    /// <typeparam name="T">The entity</typeparam>
    /// <seealso cref="BaseResponse" />
    public class Response<T> : BaseResponse
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public T Result { get; set; }
    }
}