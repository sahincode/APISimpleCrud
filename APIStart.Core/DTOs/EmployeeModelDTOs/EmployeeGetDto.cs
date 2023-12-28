namespace APIStart.Core.DTOs.EmployeeModelDTOs
{
    public class EmployeeGetDto
    {
        public string FullName { get; set; }

        public string TwitLink { get; set; }
        public string FaceLink { get; set; }
        public string InstaLink { get; set; }
        public string LinkLink { get; set; }
        public string ImageUrl { get; set; }
        public List<int> ProfessionIds { get; set; }
        public double Salary { get; set; }
        public string Description {  get; set; }


    }
}
