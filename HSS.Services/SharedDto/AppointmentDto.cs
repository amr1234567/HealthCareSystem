using HSS.Domain.Enums;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using HSS.Domain.Models.Aggregates;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.SharedDto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientNationalId { get; set; }
        public string? PatientName { get; set; }
        public DateTime PatientBirthDate { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public int HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public TimeSpan Duration { get; set; }
        public string? Notes { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ClinicId { get; set; }
        public string? ClinicName { get; set; }
        public TimeSpan ClinicStartAt { get; set; }
        public TimeSpan ClinicEndAt { get; set; }
        public int? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public string? ReasonForVisit { get; set; }
        public bool IsStarted { get; set; } = false;
        public bool IsConfirmed { get; set; } = false;
        public bool IsEnd { get; set; } = false;

        public AppointmentDto(ClinicAppointment appointment)
        {
            Id = appointment.Id;
            PatientId = appointment.PatientId;
            PatientName = appointment.Patient.Name;
            PatientNationalId = appointment.Patient.NationalId;
            PatientBirthDate = appointment.Patient.DateOfBirth;
            AppointmentType = appointment.AppointmentType;
            HospitalId = appointment.Clinic.HospitalId;
            HospitalName = appointment.Clinic.Hospital.Name;
            ClinicName = appointment.Clinic.Name;
            DoctorName = appointment.Doctor?.Name;
            Duration = appointment.Duration;
            Notes = appointment.Notes;
            AppointmentDate = appointment.AppointmentDate;
            CreatedAt = appointment.CreatedAt;
            ClinicId = appointment.ClinicId;
            DoctorId = appointment.DoctorId;
            ReasonForVisit = appointment.ReasonForVisit;
            IsStarted = appointment.IsStarted;
            IsConfirmed = appointment.IsConfirmed;
            IsEnd = appointment.IsEnd;
            ClinicStartAt = appointment.Clinic.StartAt;
            ClinicEndAt = appointment.Clinic.FinishAt;
        }
    }
}
