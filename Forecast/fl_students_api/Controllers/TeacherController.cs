using fl_students_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace fl_students_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMongoCollection<Teacher> _collection;

        public TeacherController(IMongoClient client, IConfiguration config)
        {
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<Teacher>("Teachers");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _collection.Find(_ => true).ToListAsync();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var teacher = await _collection.Find(t => t.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefaultAsync();
            return teacher is null ? NotFound() : Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            teacher.Id = MongoDB.Bson.ObjectId.GenerateNewId();
            await _collection.InsertOneAsync(teacher);
            return CreatedAtAction(nameof(GetById), new { id = teacher.Id.ToString() }, teacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Teacher updated)
        {
            var result = await _collection.ReplaceOneAsync(t => t.Id == MongoDB.Bson.ObjectId.Parse(id), updated);
            return result.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _collection.DeleteOneAsync(t => t.Id == MongoDB.Bson.ObjectId.Parse(id));
            return result.DeletedCount == 0 ? NotFound() : NoContent();
        }

        [HttpGet("search/by-name/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            var result = await _collection.Find(t => t.FullName.ToLower().Contains(name.ToLower())).ToListAsync();
            return Ok(result);
        }
    }
}
