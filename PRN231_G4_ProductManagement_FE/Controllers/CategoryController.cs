using Microsoft.AspNetCore.Mvc;
using PRN231_G4_ProductManagement_BE.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PRN231_G4_ProductManagement_FE.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient client = null;
        private string CategoryApiUrl = "";
        public CategoryController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CategoryApiUrl = "http://localhost:5222/api/Categorys";
        }
        // GET: BookController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(CategoryApiUrl + "/GetAllCategories");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            //dynamic temp = JsonObject.Parse(strData);
            //var lst = temp.value;

            List<Category> listCategory = JsonSerializer.Deserialize<List<Category>>(strData, options);
            return View(listCategory);
        }
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: UnitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection, Category category)
        {
            try
            {

                var requestBody = JsonSerializer.Serialize(category);
                HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(CategoryApiUrl + "/AddCategoryById", content);
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
                var response = await client.GetAsync(CategoryApiUrl + "/GetCategoryById/" + id);
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var listC = JsonSerializer.Deserialize<Category>(data, options);
                return View(listC);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: UnitController/UpdateUnitById/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection, Category category)
        {
            try
            {
                var response = await client.GetAsync(CategoryApiUrl + "/GetCategoryById/" + id);
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var listC = JsonSerializer.Deserialize<Category>(data, options);

                var requestBody = JsonSerializer.Serialize(category);
                HttpContent content = new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json");

                response = await client.PutAsync(CategoryApiUrl + "/UpdateCategoryById", content);

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
            var response = await client.GetAsync(CategoryApiUrl + "/GetCategoryById/" + id);
            string data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var listC = JsonSerializer.Deserialize<Category>(data, options);
            string deleteUrl = CategoryApiUrl + "/DeleteCategoryById/" + Convert.ToString(id);
            response = await client.DeleteAsync(deleteUrl);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "delete successfull";
                return Redirect("/Category/Index");
            }
            TempData["ErrorMessage"] = "delete failed";
            return Redirect("/Category/Index");
        }
    }
}
