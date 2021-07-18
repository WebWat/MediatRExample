using MediatR.Application.Common;

namespace MediatR.Application.Entities
{
    public class Item : BaseEntity
    {
        public string Title { get; set; }
        public double UniqueNumber { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            var item = obj as Item;

            return Id == item.Id &&
                   Title == item.Title &&
                   UniqueNumber == item.UniqueNumber;
        }
    }
}
