using CountriesManagementSystem.Domain.Entities._Bases;

namespace CountriesManagementSystem.Domain.Entities
{
    public class Country : BaseEntity<long>
    {
        #region Properties
        public string Name { get; private set; }

        #endregion

        #region Related Collections
        private List<City> _Cities = new List<City>();
        public virtual IReadOnlyCollection<City> Cities { get { return _Cities; } }

        #endregion
        #region Constructors
        private Country() { }//for ORM
        private Country(string name)
        {
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        #endregion

        #region Behaviours
        public static Country Create(string name)
        {
            return new Country(name);
        }

        public void Update(string name)
        {
            Name = name;
        }
        #endregion


    }
}
