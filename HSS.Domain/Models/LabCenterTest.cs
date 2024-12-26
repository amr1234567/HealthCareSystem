namespace HSS.Domain.Models
{
    public class LabCenterTest : BaseClass<int> 
    {
        [MaxLength(100)]
        public string? Name { get; set; }
        public string UserId { get; set; }
        [MaxLength(100)]
        public string TestType { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
        public string Details { get; set; }
    }
}
