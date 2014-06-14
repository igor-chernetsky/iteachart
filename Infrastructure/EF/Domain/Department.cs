namespace Infrastructure.EF.Domain
{
    public class Department
    {
        public int Id { get; set; }
        public string DepCode { get; set; }
        public string Name { get; set; }
        public int NumUsers { get; set; }
    }
}
