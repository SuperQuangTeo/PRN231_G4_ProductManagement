using Microsoft.AspNetCore.Mvc;
using PRN231_G4_ProductManagement_BE.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PRN231_G4_ProductManagement_FE.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly HttpClient client = null;
        private string SupplierApiUrl = "";
        public SuppliersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            SupplierApiUrl = "http://localhost:5222/api/Suppliers";
        }
        // GET: BookController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(SupplierApiUrl + "/GetAllSuppliers");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            //dynamic temp = JsonObject.Parse(strData);
            //var lst = temp.value;

            List<Supplier> listSupplier = JsonSerializer.Deserialize<List<Supplier>>(strData, options);
            return View(listSupplier);
        }
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: UnitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection, Supplier supplier)
        {
            try
            {

                var requestBody = JsonSerializer.Serialize(supplier);
                HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(SupplierApiUrl + "/AddSupplier", content);
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
                var response = await client.GetAsync(SupplierApiUrl + "/GetSupplierById/" + id);
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var listS = JsonSerializer.Deserialize<Supplier>(data, options);
                return View(listS);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: UnitController/UpdateUnitById/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection, Supplier supplier)
        {
            try
            {
                var response = await client.GetAsync(SupplierApiUrl + "/GetSupplierById/" + id);
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var listS = JsonSerializer.Deserialize<Supplier>(data, options);

                var requestBody = JsonSerializer.Serialize(supplier);
                HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                response = await client.PutAsync(SupplierApiUrl + "/UpdateSupplierById", content);

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
            var response = await client.GetAsync(SupplierApiUrl + "/GetSupplierById/" + id);
            string data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var listS = JsonSerializer.Deserialize<Supplier>(data, options);
            string deleteUrl = SupplierApiUrl + "/DeleteSupplierById/" + Convert.ToString(id);
            response = await client.DeleteAsync(deleteUrl);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "delete successfull";
                return Redirect("/Suppliers/Index");
            }
            TempData["ErrorMessage"] = "delete failed";
            return Redirect("/Suppliers/Index");
        }
    }
}
