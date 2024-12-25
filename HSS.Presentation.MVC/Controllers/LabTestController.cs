using HSS.Domain.Models;
using HSS.Services.Services.LabTest;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace HSS.Presentation.MVC.Controllers.LabTest
{
    public class LabTestController : Controller
    {
        private readonly ILabTestService _labTestService;

        public LabTestController(ILabTestService labTestService)
        {
            _labTestService = labTestService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadTests()
        {
            var tests = await _labTestService.GetAllTestForUser("1");
            return View(tests);
        }

        [HttpGet]
        public IActionResult AddTest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTestToUser()
        {
            using var reader = new StreamReader(Request.Body);
            string jsonString = await reader.ReadToEndAsync();

            JObject jObject = JObject.Parse(jsonString);

            LabCenterTest test = new LabCenterTest();

            foreach (var prop in jObject.Properties())
            {
                if (prop.Name == "TestType")
                    test.TestType = prop.Value.ToString();

                if (prop.Name == "Details")
                    test.Details = prop.Value.ToString();
            }

            await _labTestService.AddLabTestForUser(test);

            return RedirectToAction("LoadTests");
        }

        [HttpGet]
        public async Task<IActionResult> TestDetails(int id)
        {
            LabCenterTest test = await _labTestService.GetTestById(id);
            JObject keyValuePairs = JObject.Parse(test.Details);
            var obj = new
            {
                Id = test.Id,
                TestType = test.TestType,
                Details = keyValuePairs.Properties()
            };
            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> EditTest(int id)
        {
            LabCenterTest test = await _labTestService.GetTestById(id);
            JObject keyValuePairs = JObject.Parse(test.Details);
            var obj = new
            {
                Id = test.Id,
                TestType = test.TestType,
                Details = keyValuePairs.Properties()
            };
            return View(obj);
        }
        [HttpPut]
        public async Task<IActionResult> EditTest()
        {
            using var reader = new StreamReader(Request.Body);
            string jsonString = await reader.ReadToEndAsync();

            JObject jObject = JObject.Parse(jsonString);

            LabCenterTest test = await _labTestService.GetTestById((int)jObject["id"]);

            foreach (var prop in jObject.Properties())
            {
                if (prop.Name == "TestType")
                    test.TestType = prop.Value.ToString();

                if (prop.Name == "Details")
                    test.Details = prop.Value.ToString();
            }

            await _labTestService.UpdateTest(test);

            return RedirectToAction("LoadTests");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTest(int id)
        {
            await _labTestService.DeleteTest(id);
            return RedirectToAction("LoadTests");
        }

    }
}
