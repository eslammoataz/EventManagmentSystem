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

        public static class User
        {
            public static readonly Error UserAlreadyExists = new("User.UserAlreadyExists", "A user with the specified details already exists.");
            public static readonly Error UserNotAuthorized = new("User.UserNotAuthorized", "User is not authorized to perform this action.");
            public static readonly Error PhoneNumberAlreadyExists = new(
                "User.PhoneNumberAlreadyExists",
                "A user with this phone number already exists.");
        }

        public static class Organization
        {
            public static readonly Error OrganizationNotFound = new("Organization.OrganizationNotFound", "The organization was not found.");
            public static readonly Error InvalidAdmin = new("Organization.InvalidAdmin", "Only the admin of the organization can do actions.");
            public static readonly Error NoUsersAdded = new Error(
                    "Organization.NoUsersAdded",
                    "No users were added to the organization."
                );
            public static readonly Error NoUsersRemoved = new("Organization.NoUsersRemoved", "No users were removed from the organization.");

        }

        public static class Event
        {
            public static readonly Error EventAlreadyExists = new("Event.EventAlreadyExists", "An event with this name already exists.");
            public static readonly Error InvalidEventDates = new("Event.InvalidEventDates", "The event start date cannot be later than the end date.");
            public static readonly Error EventNotFound = new(
                   "Event.EventNotFound",
                   "The event was not found."
               );
        }


        public static class Ticket
        {
            public static readonly Error TicketsNotFound = new Error(
                "Ticket.TicketsNotFound",
                "The requested tickets were not found."
            );

            public static readonly Error DeletionFailed = new Error(
                "Ticket.DeletionFailed",
                "An error occurred while attempting to delete the tickets."
            );

            public static readonly Error TicketNotFound = new Error(
              "Ticket.TicketNotFound",
              "The requested ticket was not found."
          );

            public static readonly Error TicketAlreadyBooked = new Error(
                "Ticket.TicketAlreadyBooked",
                "The requested ticket is already booked."
            );

            public static readonly Error NoAvailableTickets = new Error(
                     "Ticket.NoAvailableTickets",
                     "No available tickets for the requested event."
                 );

            public static readonly Error NoTicketsForUser = new Error(
               "Ticket.NoTicketsForUser",
               "No tickets found for the specified user."
           );

            public static readonly Error AlreadyCheckedIn = new Error(
                           "Ticket.AlreadyCheckedIn",
                           "Ticket is already checked in."
                       );

            public static readonly Error SenderDoesNotOwnTicket = new Error(
                    "Ticket.SenderDoesNotOwnTicket",
                    "The sender does not own the specified ticket."
                );

            public static readonly Error TicketAlreadyGifted = new Error(
                "Ticket.TicketAlreadyGifted",
                "The ticket has already been gifted."
            );

            public static readonly Error TicketCannotBeGifted = new Error(
                "Ticket.TicketCannotBeGifted",
                "The ticket cannot be gifted due to restrictions."
            );
        }


        public static class General
        {
            public static readonly Error UnexpectedError = new Error(
                "General.UnexpectedError",
                "An unexpected error occurred. Please try again later."
            );
        }

    }
}
