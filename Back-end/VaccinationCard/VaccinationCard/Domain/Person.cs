namespace VaccinationCard.Domain
{
    public class Person : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public List<Vaccination> Vaccinations { get; set; } = [];   
    }
}
