namespace CountriesManagementSystem.Domain.Entities._Bases
{
    public class AppEntityBase { }//marker
    public class BaseEntity<TKey> : AppEntityBase
    {
        public TKey Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public BaseEntity()
        {
        }
    }
}
