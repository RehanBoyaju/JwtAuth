using Domain.Primitives;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Member : Entity
    {
        private Member(Guid id, Email email, FirstName firstName, LastName lastName)
            : base(id)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            CreatedOnUtc = DateTime.UtcNow;
        }

        private Member()
        {
        }
        public Email Email { get; private set; }

        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public string HashedPassword { get; private set; }


        public DateTime CreatedOnUtc { get; set; }

        public DateTime? ModifiedOnUtc { get; set; }

        public static Member Create(
            Guid id,
            Email email,
            FirstName firstName,
            LastName lastName)
        {
            var member = new Member(
                id,
                email,
                firstName,
                lastName
                );

            

            return member;
        }
        public void SetPassword(string hashedPassword)
        {
            HashedPassword = hashedPassword;
            ModifiedOnUtc = DateTime.UtcNow;
        }
        public void ChangeName(FirstName firstName, LastName lastName)
        {
            if (!FirstName.Equals(firstName) || !LastName.Equals(lastName))
            {
                //Systemwide updates here
                ModifiedOnUtc = DateTime.UtcNow;
            }

            FirstName = firstName;
            LastName = lastName;
        }

        
    }
}
