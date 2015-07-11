using System;

namespace VB.Infrastructure.Models
{
    public class Player : IModel
    {
        public Guid Id { get; set; }
        public string Name;
        public int Age;
        public int Number;
        public int Height;
        public string Amplua;
        public int PhoneNumber;
        public string Mail;

        public DateTime Date;
    }
}
