using System;

namespace VB.Infrastructure.Models
{
    public class Match : IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public string Place;
        public DateTime Date;
    }


   
}
