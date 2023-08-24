using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_G4_ProductManagement_BE.DTO;
using PRN231_G4_ProductManagement_BE.Services;
using PRN231_G4_ProductManagement_BE.Utilities;
using System.Net;


namespace PRN231_G4_ProductManagement_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private ProductService productService;
        private IMapper mapper;

        public ExcelController(IMapper _mapper)
        {
            mapper = _mapper;
            productService = new ProductService();
        }
        [HttpGet("ExportProductToExcel")]
        public IActionResult ExportProducttoExcel()
        {
            string fileName = "testexcel.xlsx";
            var dt = ExcelService.productExcel(productService.allProduct());
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.ColumnWidth = 25;
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("templateProduct")]
        public IActionResult ExportTemplateExcel()
        {
            string fileName = "template-product.xlsx";
            var dt = ExcelService.sampleTemplateProduct();
            try
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("readProduct")]
        public ResponseBody<List<ProductDTO>> ReadExcel([FromForm] IFormFile file)
        {
            try
            {
                var rs = ExcelService.readData(file);
                return new ResponseBody<List<ProductDTO>>()
                {
                    code = HttpStatusCode.OK,
                    message = Resource.SUCCESSFULLY,
                    data = mapper.Map<List<ProductDTO>>(rs),
                };
            }
            catch (Exception ex)
            {
                return new ResponseBody<List<ProductDTO>>()
                {
                    code = HttpStatusCode.NotAcceptable,
                    message = ex.Message,
                };
            }
        }

    }
}
