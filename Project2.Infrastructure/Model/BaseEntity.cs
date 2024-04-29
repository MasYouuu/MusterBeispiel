using System.ComponentModel.DataAnnotations;

namespace Project2.Infrastructure.Model
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
