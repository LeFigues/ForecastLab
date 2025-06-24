using fl_students_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace fl_students_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CycleController : ControllerBase
    {
        private readonly IMongoCollection<Cycle> _collection;

        public CycleController(IMongoClient client, IConfiguration config)
        {
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<Cycle>("Cycles");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cycles = await _collection.Find(_ => true).ToListAsync();
            return Ok(cycles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var cycle = await _collection.Find(c => c.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefaultAsync();
            return cycle is null ? NotFound() : Ok(cycle);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cycle cycle)
        {
            cycle.Id = MongoDB.Bson.ObjectId.GenerateNewId();
            await _collection.InsertOneAsync(cycle);
            return CreatedAtAction(nameof(GetById), new { id = cycle.Id.ToString() }, cycle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Cycle updated)
        {
            var result = await _collection.ReplaceOneAsync(c => c.Id == MongoDB.Bson.ObjectId.Parse(id), updated);
            return result.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _collection.DeleteOneAsync(c => c.Id == MongoDB.Bson.ObjectId.Parse(id));
            return result.DeletedCount == 0 ? NotFound() : NoContent();
        }
        [HttpGet("year/{year}")]
        public async Task<IActionResult> GetByYear(int year)
        {
            var result = await _collection.Find(c => c.Year == year).ToListAsync();
            return Ok(result);
        }

        [HttpGet("year/{year}/semester/{semester}")]
        public async Task<IActionResult> GetByYearAndSemester(int year, int semester)
        {
            var result = await _collection.Find(c => c.Year == year && c.Semester == semester).ToListAsync();
            return Ok(result);
        }

    }
}
