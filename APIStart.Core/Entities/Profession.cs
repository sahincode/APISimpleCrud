namespace APIStart.Core.Entities
{
    public class Profession :BaseEntity
    {
        public string Name { get; set; }
        public List<ProfessionEmployee> ProfessionEmployees { get; set; }
    }
}
