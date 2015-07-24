using System;

namespace VB.Infrastructure.Models
{
    public interface IModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}