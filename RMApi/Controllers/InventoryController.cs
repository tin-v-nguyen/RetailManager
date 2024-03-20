using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;


namespace RMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class InventoryController : ControllerBase
    {
        private readonly IConfiguration config;

        public InventoryController(IConfiguration config)
        {
            this.config = config;
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet]
        public List<InventoryModel> Get()
        {
            InventoryData data = new InventoryData(config);
            return data.GetInventory();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void Post(InventoryModel record)
        {
            InventoryData data = new InventoryData(config);
            data.SaveInventoryRecord(record);
        }
    }
}
