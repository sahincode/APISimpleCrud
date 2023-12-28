namespace APIStart.Core.Entities
{
    public class ProfessionEmployee  :BaseEntity
    { public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee{ get; set; }
        public int ProfessionId { get; set; }
        public Profession  Profession { get; set; }


    }
}
