using HSS.DataAccess.Contexts;
using HSS.DataAccess.Helpers;
using HSS.DataAccess.Repositories;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSS.Presentation.Patient.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(Helper helper, ApplicationDbContext context, AccountServicesHelpers accountServices) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            await helper.Seed();
            return Ok();
        }

        [HttpPost("seed-receptionist")]
        public async Task<IActionResult> SeedReceptionist(string name, string password, string nationalId)
        {
            var salt = accountServices.CreateSalt();
            var hospitalId = 1;
            var admin = new HospitalAdmin
            {
                Name = "محمد علي",
                NationalId = "12345678901234",
                Salt = salt,
                CreatedAt = DateTime.Now,
                Roles =
                [
                    new Role
                    {
                        RoleName = Domain.Enums.ApplicationRole.Admin
                    }
                ],
                Password = accountServices.HashPasswordWithSalt(salt, "123456"),
            };
            var hospital = new Hospital
            {
                Id = hospitalId,
                Name = "مستشفى الحياة العامة",
                Location = "123 شارع الرئيسي، القاهرة",
                Capacity = 500,
                ContactNumber = "+201234567890",
                Email = "contact@hayahospital.com",
                EstablishedDate = new DateTime(1985, 5, 20),
                StartAt = new TimeSpan(8, 0, 0),
                EndAt = new TimeSpan(20, 0, 0),
                BedAvailability = 150,
                NumberOfDoctors = 100,
                NumberOfNurses = 200,
                DepartmentsCount = 15,
                Latitude = 30.0444f,
                Longitude = 31.2357f,
                WebsiteUrl = "http://www.hayahospital.com",
                LicenseNumber = "HA123456789",
                TaxIdentificationNumber = "TIN987654321",
                Rating = 4.5f
            };
            var reception = new Reception
            {
                Id = 1,
                EndAt = new TimeSpan(17,0,0),
                StartAt = new TimeSpan(9, 0, 0),
                HospitalId = hospital.Id,
            };
            var receptionist = new Receptionist
            {
                Name = name,
                NationalId = nationalId,
                Salt = salt,
                CreatedAt = DateTime.Now,
                ReceptionId = reception.Id,
                Roles =
                [
                    new Role
                    {
                        RoleName = Domain.Enums.ApplicationRole.Receptionist
                    }
                ],
                Password = accountServices.HashPasswordWithSalt(salt, password)
            };

            await context.Hospitals.AddAsync(hospital);
            await context.Receptions.AddAsync(reception);
            await context.Receptionists.AddAsync(receptionist);
            await context.SaveChangesAsync();
            return Created();
        }
    }
}
