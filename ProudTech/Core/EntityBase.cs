namespace ProudTech.Core
{
    public class EntityBase
    {
        public Guid Id { get; set; }
        public EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
