using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using PRN231_G4_ProductManagement_BE.Models;
using System.Diagnostics;
using System.Net;

namespace PRN231_G4_ProductManagement_FE.Controllers
{
    public class UnitController : Controller
    {
        private readonly HttpClient client = null;
        private string UnitApiUrl = "";
        public UnitController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UnitApiUrl = "http://localhost:5222/api/Units";
        }
        // GET: BookController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(UnitApiUrl + "/GetAllUnits");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            //dynamic temp = JsonObject.Parse(strData);
            //var lst = temp.value;

            List<Unit> listUnit = JsonSerializer.Deserialize<List<Unit>>(strData, options);
            return View(listUnit);
        }
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: UnitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection, Unit unit)
        {
            try
            {
                
                var requestBody = JsonSerializer.Serialize(unit);
                HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(UnitApiUrl + "/AddUnit", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "create successfull";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "create failed";
                    return RedirectToAction(nameof(Index));
                }
                
                //if (!response.IsSuccessStatusCode)
                //    throw new Exception(response.StatusCode.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: BookController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var response = await client.GetAsync(UnitApiUrl + "/GetUnitById/" + id);
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var listU = JsonSerializer.Deserialize<Unit>(data, options);
                return View(listU);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: UnitController/UpdateUnitById/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection, Unit unit)
        {
            try
            {
                var response = await client.GetAsync(UnitApiUrl + "/GetUnitById/" + id);
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var listU = JsonSerializer.Deserialize<Unit>(data, options);

                var requestBody = JsonSerializer.Serialize(unit);
                HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                response = await client.PutAsync(UnitApiUrl + "/UpdateUnitById", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "update successfull";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "update failed";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var response = await client.GetAsync(UnitApiUrl + "/GetUnitById/" + id);
            string data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var listU = JsonSerializer.Deserialize<Unit>(data, options);
            string deleteUrl = UnitApiUrl + "/DeleteUnitById/" + Convert.ToString(id);
            response = await client.DeleteAsync(deleteUrl);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "delete successfull";
                return Redirect("/Unit/Index");
            }
            TempData["ErrorMessage"] = "delete failed";
            return Redirect("/Unit/Index");
        }
    }
}
