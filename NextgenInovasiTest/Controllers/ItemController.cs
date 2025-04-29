using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NextgenInovasiTest.BusinessLogic;
using NextgenInovasiTest.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace NextgenInovasiTest.Controllers;

public class ItemController : BaseController
{
    private readonly ILogger<ItemController> _logger;
    private readonly IBusinessLogic<Item> _itemBusinessLogic;
    private readonly UserManager<User> _user;
    public ItemController(ILogger<ItemController> logger, IBusinessLogic<Item> itemBusinessLogic, UserManager<User> user) : base(logger)
    {
        _logger = logger;
        _itemBusinessLogic = itemBusinessLogic;
        _user = user;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
    [Authorize]
    [HttpGet("Item/GetAll")]
    public async Task<JsonResult> GetAll()
    {
        // var data = await _itemBusinessLogic.GetAllAsync();
        // return Json(data);
        return await HandleResponse(async () => await _itemBusinessLogic.GetAllAsync());
    }
    [Authorize]
    [HttpGet("Item/GetItemTemplate")]
    public IActionResult GetItemTemplate()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ItemTemplate.xlsx");
        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ItemTemplate.xlsx");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> BulkInsert(IFormFile fileBase)
    {
        var userId = _user.GetUserId(User);
        if (fileBase == null || fileBase.Length == 0)
        {
            return BadRequest("File is empty");
        }
        var dataItem = new List<Item>();
        using (var stream = new MemoryStream())
        {
            await fileBase.CopyToAsync(stream);
            stream.Position = 0;

            var workbook = new XSSFWorkbook(stream);
            var sheet = workbook.GetSheetAt(0);

            var headerRow = sheet.GetRow(0);

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);

                if (row == null)
                    continue;
                var item = new Item();
                item.Name = row.GetCell(0)?.ToString();
                item.Description = row.GetCell(1)?.ToString();
                item.Quantity = GetIntValue(row.GetCell(2));
                item.Price = GetDecimalValue(row.GetCell(3));

                dataItem.Add(item);
            }

        }
        // await _itemBusinessLogic.AddBacth(dataItem, 1);
        // return Ok("Data inserted successfully");
        return await HandleResponse(async () => await _itemBusinessLogic.AddBacth(dataItem, 1));
    }

    #region ::: Helper
    private int GetIntValue(ICell cell)
    {
        if (cell == null) return 0;

        if (cell.CellType == CellType.Numeric)
            return Convert.ToInt32(cell.NumericCellValue);

        if (cell.CellType == CellType.String && int.TryParse(cell.StringCellValue, out int result))
            return result;

        return 0;
    }

    private decimal GetDecimalValue(ICell cell)
    {
        if (cell == null) return 0m;

        if (cell.CellType == CellType.Numeric)
            return Convert.ToDecimal(cell.NumericCellValue);

        if (cell.CellType == CellType.String && decimal.TryParse(cell.StringCellValue, out decimal result))
            return result;

        return 0m;
    }
    #endregion
}
