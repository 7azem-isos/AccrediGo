using AccrediGo.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace AccrediGo.Controllers.Base
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly ICurrentRequest _currentRequest;

        protected ApiControllerBase(ICurrentRequest currentRequest)
        {
            _currentRequest = currentRequest;
        }

        /// <summary>
        /// Validates the model state and throws a BusinessValidationException if invalid.
        /// </summary>
        /// <param name="messageCode">The message code for the validation error</param>
        /// <param name="enMessage">The English error message</param>
        /// <param name="arMessage">The Arabic error message</param>
        protected void ValidateModelState(string messageCode, string enMessage = "Invalid request data", string arMessage = "بيانات الطلب غير صالحة")
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join(", ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                throw new BusinessValidationException(messageCode,
                    _currentRequest.Lang == "en"
                        ? $"{enMessage}: {errorMessage}"
                        : $"{arMessage}: {errorMessage}");
            }
        }

        /// <summary>
        /// Validates that a list is not null or empty and throws a BusinessValidationException if invalid.
        /// </summary>
        /// <typeparam name="T">The type of items in the list</typeparam>
        /// <param name="list">The list to validate</param>
        /// <param name="messageCode">The message code for the validation error</param>
        /// <param name="enMessage">The English error message</param>
        /// <param name="arMessage">The Arabic error message</param>
        protected void ValidateListNotEmpty<T>(IEnumerable<T> list, string messageCode, string enMessage = "The list cannot be empty", string arMessage = "القائمة لا يمكن أن تكون فارغة")
        {
            if (list == null || !list.Any())
            {
                throw new BusinessValidationException(messageCode,
                    _currentRequest.Lang == "en" ? enMessage : arMessage);
            }
        }

        /// <summary>
        /// Validates that a string is not null or empty and throws a BusinessValidationException if invalid.
        /// </summary>
        /// <param name="value">The string to validate</param>
        /// <param name="messageCode">The message code for the validation error</param>
        /// <param name="enMessage">The English error message</param>
        /// <param name="arMessage">The Arabic error message</param>
        protected void ValidateStringNotEmpty(string value, string messageCode, string enMessage = "The value cannot be empty", string arMessage = "القيمة لا يمكن أن تكون فارغة")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new BusinessValidationException(messageCode,
                    _currentRequest.Lang == "en" ? enMessage : arMessage);
            }
        }

        /// <summary>
        /// Validates that a value is not null and throws a BusinessValidationException if invalid.
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="value">The value to validate</param>
        /// <param name="messageCode">The message code for the validation error</param>
        /// <param name="enMessage">The English error message</param>
        /// <param name="arMessage">The Arabic error message</param>
        protected void ValidateNotNull<T>(T value, string messageCode, string enMessage = "The value cannot be null", string arMessage = "القيمة لا يمكن أن تكون فارغة")
        {
            if (value == null)
            {
                throw new BusinessValidationException(messageCode,
                    _currentRequest.Lang == "en" ? enMessage : arMessage);
            }
        }

        /// <summary>
        /// Validates that a condition is true and throws a BusinessValidationException if false.
        /// </summary>
        /// <param name="condition">The condition to validate</param>
        /// <param name="messageCode">The message code for the validation error</param>
        /// <param name="enMessage">The English error message</param>
        /// <param name="arMessage">The Arabic error message</param>
        protected void ValidateCondition(bool condition, string messageCode, string enMessage = "Validation failed", string arMessage = "فشل في التحقق من صحة البيانات")
        {
            if (!condition)
            {
                throw new BusinessValidationException(messageCode,
                    _currentRequest.Lang == "en" ? enMessage : arMessage);
            }
        }

        /// <summary>
        /// Gets the current user ID from the request context.
        /// </summary>
        /// <returns>The current user ID</returns>
        protected string GetCurrentUserId()
        {
            return _currentRequest.UserId;
        }

        /// <summary>
        /// Gets the current user name from the request context.
        /// </summary>
        /// <returns>The current user name</returns>
        protected string GetCurrentUserName()
        {
            return _currentRequest.UserName;
        }

        /// <summary>
        /// Gets the current user email from the request context.
        /// </summary>
        /// <returns>The current user email</returns>
        protected string GetCurrentUserEmail()
        {
            return _currentRequest.UserEmail;
        }

        /// <summary>
        /// Gets the current user role ID from the request context.
        /// </summary>
        /// <returns>The current user role ID</returns>
        protected int GetCurrentUserRoleId()
        {
            return _currentRequest.UserRoleId;
        }

        /// <summary>
        /// Gets the current language from the request context.
        /// </summary>
        /// <returns>The current language</returns>
        protected string GetCurrentLanguage()
        {
            return _currentRequest.Lang;
        }

        /// <summary>
        /// Gets the correlation ID for the current request.
        /// </summary>
        /// <returns>The correlation ID</returns>
        protected string GetCorrelationId()
        {
            return _currentRequest.CorrelationId;
        }
    }
} 