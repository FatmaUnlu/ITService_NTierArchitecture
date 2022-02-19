using System.ComponentModel.DataAnnotations;

namespace ItServiceApp.Core.Entities.Abstracts
{
    public interface IEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
