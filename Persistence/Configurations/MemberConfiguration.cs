using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable(TableNames.Members);
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Email)
                .HasConversion(x=> x.Value, v=> Email.Create(v).Value);
            builder.Property(x=>x.FirstName)
                .HasConversion(x=>x.Value, v=>FirstName.Create(v).Value)
                .HasMaxLength(FirstName.MaxLength);
            builder.Property(x => x.LastName)
                .HasConversion(x => x.Value, v => LastName.Create(v).Value)
                .HasMaxLength(LastName.MaxLength);


        }
    }
}
