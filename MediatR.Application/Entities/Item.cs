namespace MediatR.Application.Entities
{
    public class Item : BaseEntity
    {
        public string Title { get; set; }
        public double UniqueNumber { get; set; }
    }
}
