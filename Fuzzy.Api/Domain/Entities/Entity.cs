namespace Fuzzy.Api.Domain.Entities
{
    public abstract class Entity<TKeyType>
    {
        protected Entity(TKeyType id = default)
        {
            Id = id;
        }

        public virtual TKeyType Id { get; }

        public bool Deleted { get; set; }

    }
}