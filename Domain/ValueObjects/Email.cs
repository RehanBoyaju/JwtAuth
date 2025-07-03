using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        public const int MaxLength = 255;

        //Private constructor(s) means you cannot just create an Email by calling new Email().
        //Instead, you must use the factory method Create
        private Email(string value) => Value = value;

        private Email()
        {
        }

        public string Value { get; private set; }

        public static Result<Email> Create(string email) =>
            Result.Create(email)
                .Ensure(
                    e => !string.IsNullOrWhiteSpace(e),
                    DomainErrors.Email.Empty)
                .Ensure(
                    e => e.Length <= MaxLength,
                    DomainErrors.Email.TooLong)
                .Ensure(
                    e => e.Split('@').Length == 2,
                    DomainErrors.Email.InvalidFormat)
                .Map(e => new Email(e));

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value; //yield is used inside an iterator method to return elements one at a time, instead of creating and returning a whole collection all at once.
        }
    }
}
