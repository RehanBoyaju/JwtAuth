using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class DomainErrors
    {
        public static class Member
        {
            public static readonly Error EmailAlreadyInUse = new(
                "Member.EmailAlreadyInUse",
                "The specified email is already in use");

            public static readonly Func<Guid, Error> NotFound = id => new Error(
                "Member.NotFound",
                $"The member with the identifier {id} was not found.");

            public static readonly Error InvalidCredentials = new(
                "Member.InvalidCredentials",
                "The provided credentials are invalid");
        }

        public static class Gathering
        {
            public static readonly Func<Guid, Error> NotFound = id => new Error(
                "Gathering.NotFound",
                $"The gathering with the identifier {id} was not found.");

            public static readonly Error InvitingCreator = new(
                "Gathering.InvitingCreator",
                "Can't send invitation to the gathering creator");

            public static readonly Error AlreadyPassed = new(
                "Gathering.AlreadyPassed",
                "Can't send invitation for gathering in the past");

            public static readonly Error Expired = new(
                "Gathering.Expired",
                "Can't accept invitation for expired gathering");
        }

        public static class Email
        {
            public static readonly Error Empty = new(
                "Email.Empty",
                "Email is empty");

            public static readonly Error TooLong = new(
                "Email.TooLong",
                "Email is too long");

            public static readonly Error InvalidFormat = new(
                "Email.InvalidFormat",
                "Email format is invalid");
        }

        public static class FirstName
        {
            public static readonly Error Empty = new(
                "FirstName.Empty",
                "First name is empty");

            public static readonly Error TooLong = new(
                "LastName.TooLong",
                "FirstName name is too long");
        }

        public static class LastName
        {
            public static readonly Error Empty = new(
                "LastName.Empty",
                "Last name is empty");

            public static readonly Error TooLong = new(
                "LastName.TooLong",
                "Last name is too long");
        }

        public static class Password
        {
            public static readonly Error Empty = new(
                "Password.Empty",
                "Password is empty");

            public static readonly Error TooShort = new(
                "Password.TooShort",
                "Password is too short");

            public static readonly Error InvalidFormat = new(
                "Password.InvalidFormat",
                "Password format is invalid");

            public static readonly Error SamePassword = new(
                "Password.Same",
                "New password can't be same as old password");
        }
        public static class PasswordResetToken
        {
            public static readonly Error Invalid = new("Token.NotFound", "Token is invalid");

            public static readonly Error Expired = new("Token.Expired", "Token has already expired. Please request a new token");
        }
    }
}