using fl_students_api.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace fl_students_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IMongoClient _mongoClient;
        private readonly string _dbName;

        public HealthController(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoOptions)
        {
            _mongoClient = mongoClient;
            _dbName = mongoOptions.Value.DatabaseName;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var db = _mongoClient.GetDatabase(_dbName);
                var collections = await db.ListCollectionNamesAsync();
                var count = await collections.AnyAsync();

                return Ok(new
                {
                    apiStatus = "OK",
                    mongoStatus = "OK",
                    database = _dbName,
                    collectionsFound = count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    apiStatus = "OK",
                    mongoStatus = "FAIL",
                    error = ex.Message
                });
            }
        }
    }
}
