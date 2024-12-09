namespace HSS.Domain.Models.ManyToManyRelationEntitys
{
    public class LabCenterLabTest
    {
        [Required]
        public int LabCenterId { get; set; }
        public LabCenter LabCenter { get; set; }

        [Required]
        public int LabCenterTestId { get; set; }
        public LabCenterTest LabCenterTest { get; set; }
    }
}
