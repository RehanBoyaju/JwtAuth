using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class Password : ValueObject
    {
        public const int MinLength = 8;

        private Password(string value)
        {
            Value = value;
        }

        private Password()
        {
        }

        public string Value { get; private set; }

        public static Result<Password> Create(string password) =>
            Result.Create(password)
                .Ensure(
                    e => !string.IsNullOrWhiteSpace(e),
                    DomainErrors.Password.Empty)
                .Ensure(
                    e => e.Length >= MinLength,
                    DomainErrors.Password.TooShort)
                .Ensure(
                    e => e.IsValidFormat(),
                    DomainErrors.Password.InvalidFormat)
                .Map(e => new Password(e));

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
    internal static class PasswordExtensions
    {
        public static bool IsValidFormat(this string password)
        {
            const string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
