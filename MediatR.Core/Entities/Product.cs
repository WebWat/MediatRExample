using MediatR.Core.Interfaces;
using System;

namespace MediatR.Core.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }
    }
}
