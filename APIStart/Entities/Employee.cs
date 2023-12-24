namespace APIStart.Entities
{
    public class Employee :BaseEntity 
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string TwitLink { get; set; }
        public string FaceLink { get; set; }
        public string InstaLink { get; set; }
        public string LinkEdn { get; set; }
        public string ImageUrl { get; set; }
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }



    }
}
