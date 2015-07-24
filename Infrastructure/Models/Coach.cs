using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace VB.Infrastructure.Models
{
  [ExcludeFromCodeCoverage]
  public class Coach : IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid TeamId;
        public string PhoneNumber;
        public string Mail;
        public DateTime Date;
    }
}
