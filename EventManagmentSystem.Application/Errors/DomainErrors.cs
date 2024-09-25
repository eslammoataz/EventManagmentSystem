using EventManagmentSystem.Application.Helpers;

namespace EventManagmentSystem.Application.Errors
{
    public static class DomainErrors
    {
        public static class Authentication
        {
            public static readonly Error InvalidPhoneNumber = new("Authentication.InvalidPhoneNumber", "Phone number cannot be empty or less than 10 characters.");
            public static readonly Error InvalidOtp = new("Authentication.InvalidOtp", "The provided OTP code is invalid.");
            public static readonly Error UserNotFound = new("Authentication.UserNotFound", "The user was not found.");
            public static readonly Error TokenNotFound = new("Authentication.TokenNotFound", "The authentication token was not found.");
        }
    }
}
