using fl_students_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace fl_students_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IMongoCollection<Faculty> _collection;

        public FacultyController(IMongoClient client, IConfiguration config)
        {
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<Faculty>("Faculties");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var faculties = await _collection.Find(_ => true).ToListAsync();
            return Ok(faculties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var faculty = await _collection.Find(f => f.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefaultAsync();
            return faculty is null ? NotFound() : Ok(faculty);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Faculty faculty)
        {
            faculty.Id = MongoDB.Bson.ObjectId.GenerateNewId();
            await _collection.InsertOneAsync(faculty);
            return CreatedAtAction(nameof(GetById), new { id = faculty.Id.ToString() }, faculty);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Faculty updated)
        {
            var result = await _collection.ReplaceOneAsync(f => f.Id == MongoDB.Bson.ObjectId.Parse(id), updated);
            return result.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _collection.DeleteOneAsync(f => f.Id == MongoDB.Bson.ObjectId.Parse(id));
            return result.DeletedCount == 0 ? NotFound() : NoContent();
        }

        [HttpGet("search/by-name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _collection.Find(f => f.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            return Ok(result);
        }

    }
}
