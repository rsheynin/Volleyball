using System;

namespace VB.Infrastructure.Models
{
    public class Player : IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int Age;
        public int Number;
        public int Height;
        public string Amplua;
        public string PhoneNumber;
        public string Mail;

        public DateTime Date;
    }
}
