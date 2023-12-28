using APIStart.Core.enums;


namespace APIStart.Core.Entities
{
    public class Employee :BaseEntity 
    {
        public string FullName { get; set; }
        public List<ProfessionEmployee> Professions { get; set; } = new List<ProfessionEmployee>();
        public string LinkLink { get; set; }
        public string InstaLink { get; set; }
        public string FaceLink { get; set; }
        public string TwitLink { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get;set; }
        public double Salary { get; set; }
        public Status OrderStatus { get; set; }


    }
}
