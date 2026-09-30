using CountriesManagementSystem.Domain.Entities._Bases;

namespace CountriesManagementSystem.Domain.Entities
{
    public class City : BaseEntity<long>
    {
        #region Properties
        public string Name { get; private set; }
        #endregion

        #region Related Entities
        public Country Country { get; private set; }
        public long CountryId { get; private set; }
        #endregion

        #region Constructors
        private City() { }//for ORM
        private City(string name, long countryId)
        {
            Name = name;
            CountryId = countryId;
            CreatedAt = DateTime.UtcNow;
        }

        #endregion

        #region Behaviours
        public static City Create(string name, long countryId)
        {
            return new City(name, countryId);
        }

        public void Update(string name, long countryId)
        {
            Name = name;
            CountryId = countryId;
        }
        #endregion


    }
}
