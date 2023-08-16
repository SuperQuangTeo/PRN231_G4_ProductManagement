using Microsoft.AspNetCore.Mvc;
using PRN231_G4_ProductManagement_BE.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PRN231_G4_ProductManagement_FE.Controllers
{
    public class StoreController : Controller
    {
        private readonly HttpClient client = null;
        private string StoreApiUrl = "";
        public StoreController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            StoreApiUrl = "http://localhost:5222/api/Stores";
        }
        // GET: BookController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(StoreApiUrl + "/GetAllStores");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            //dynamic temp = JsonObject.Parse(strData);
            //var lst = temp.value;

            List<Store> listStore = JsonSerializer.Deserialize<List<Store>>(strData, options);
            return View(listStore);
        }
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: UnitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection, Store store)
        {
            try
            {

                var requestBody = JsonSerializer.Serialize(store);
                HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(StoreApiUrl + "/AddStoreById", content);
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
                var response = await client.GetAsync(StoreApiUrl + "/GetStoreById/" + id);
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var listS = JsonSerializer.Deserialize<Store>(data, options);
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
        public async Task<ActionResult> Edit(int id, IFormCollection collection, Store store)
        {
            try
            {
                var response = await client.GetAsync(StoreApiUrl + "/GetStoreById/" + id);
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var listS = JsonSerializer.Deserialize<Store>(data, options);

                var requestBody = JsonSerializer.Serialize(store);
                HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                response = await client.PutAsync(StoreApiUrl + "/UpdateStoreById", content);

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
            var response = await client.GetAsync(StoreApiUrl + "/GetStoreById/" + id);
            string data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var listS = JsonSerializer.Deserialize<Store>(data, options);
            string deleteUrl = StoreApiUrl + "/DeleteStoreById/" + Convert.ToString(id);
            response = await client.DeleteAsync(deleteUrl);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "delete successfull";
                return Redirect("/Store/Index");
            }
            TempData["ErrorMessage"] = "delete failed";
            return Redirect("/Store/Index");
        }
    }
}
