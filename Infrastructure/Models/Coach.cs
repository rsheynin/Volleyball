using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace Volleyball.Models
{
  [ExcludeFromCodeCoverage]
  public class Coach : IModel
    {
        public Guid Id { get; set; }
        public string Name;
        public Guid TeamId;
        public int PhoneNumber;
        public MailAddress Mail;

        public DateTime Date;

    }
}
