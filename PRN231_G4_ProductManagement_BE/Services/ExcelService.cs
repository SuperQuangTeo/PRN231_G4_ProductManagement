using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using PRN231_G4_ProductManagement_BE.Models;
using ClosedXML.Excel;
using PRN231_G4_ProductManagement_BE.Utilities;

namespace PRN231_G4_ProductManagement_BE.Services
{
    public class ExcelService
    {
        private static string sheetName = "Product";
        public static DataTable productExcel(List<Product> listProduct)
        {
            DataTable dt = new DataTable(sheetName);
            dt.Columns.AddRange(new DataColumn[9]{
                //new DataColumn("Id"),
                new DataColumn("Product Name"),
                new DataColumn("Description"),
                new DataColumn("Quantity"),
                new DataColumn("Price"),
                new DataColumn("Profit Money"),
                new DataColumn("Active"),
                new DataColumn("Supplier"),
                new DataColumn("Category"),
                new DataColumn("Unit")
            });

            foreach (var o in listProduct)
            {
                dt.Rows
                    .Add(
                    //o.id,
                    o.ProductName,
                    o.Description,
                    o.Quantity,
                    o.Price,
                    o.ProfitMoney,
                    o.Active,
                    o.Supplier == null ? "Not yet" : o.Supplier.SupplierName,
                    o.Category == null ? "Not yet" : o.Category.CategoryType,
                    o.Unit == null ? "Not yet" : o.Unit.UnitType
                    );
            }
            return dt;
        }

        public static DataTable sampleTemplateProduct()
        {
            DataTable dt = new DataTable(sheetName);
            dt.Columns.AddRange(new DataColumn[9]{
                new DataColumn("Product Name"),
                new DataColumn("Description"),
                new DataColumn("Quantity"),
                new DataColumn("Price"),
                new DataColumn("Profit Money"),
                new DataColumn("Active"),
                new DataColumn("Supplier"),
                new DataColumn("Category"),
                new DataColumn("Unit")
            });
            return dt;
        }

        public static List<Product> readData(IFormFile file)
        {
            try
            {
                using var workbook = new XLWorkbook(file.OpenReadStream());
                var ws = workbook.Worksheet(1);
                List<Product> products = new List<Product>();

                foreach (IXLRow row in ws.RowsUsed().Skip(1))
                {
                    var check = Common.checkStringsIsNullOrEmpty(new string[]
                    {
                          row.Cell(1).Value.ToString(),
                          row.Cell(3).Value.ToString(),
                          row.Cell(4).Value.ToString(),
                          row.Cell(5).Value.ToString(),
                          row.Cell(6).Value.ToString(),
                    });
                    if (check)
                    {
                        throw new NullReferenceException("Check the information again please");
                    }
                    var o = new Product()
                    {
                        Id = 0,
                        ProductName = row.Cell(1).Value.ToString(),
                        Description = row.Cell(2).Value.IsBlank ? null : row.Cell(2).Value.ToString(),
                        Quantity = int.Parse(row.Cell(3).Value.ToString()),
                        Price = int.Parse(row.Cell(4).Value.ToString()),
                        ProfitMoney = int.Parse(row.Cell(5).Value.ToString()),
                        Active = bool.Parse(row.Cell(6).Value.ToString())
                    };
                    products.Add(o);
                }
                return products;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void addProductFromExcel(List<Product> products)
        {

            if (products == null || products.Count == 0)
                throw new NullReferenceException("empty products");
            var rs = new List<Product>();
            int k = 1;
            foreach (var item in products)
            {
                var o = processProduct(item);
                rs.Add(o);
                k++;
            }
            using (var context = new PRN231_Product_ManagementContext())
            {
                context.Products.AddRange(rs);
                var n = context.SaveChanges();
                if (n <= 0)
                    throw new Exception("something was wrong");
            }
        }

        private static Product processProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product cannot null");
            }
            product.Id = 0;
            if (Common.checkStringsIsNullOrEmpty(new string[]
            {
                product.ProductName, product.Price.ToString(), product.ProfitMoney.ToString(),
                product.Description, product.Quantity.ToString()
            }))
            {
                throw new ArgumentNullException("Check your information again");
            }
            product.Active = true;
            return product;
        }
    }
}
