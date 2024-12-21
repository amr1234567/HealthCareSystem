namespace HSS.Domain.Enums
{
    public enum AppointmentType
    {
        [Display(Name = "كشف")]
        FirstTime,
        [Display(Name = "متابعة")]
        FollowUp,
        [Display(Name = "اعادة كشف")]
        ReExamination
    }
}