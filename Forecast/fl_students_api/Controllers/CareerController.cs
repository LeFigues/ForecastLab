using fl_students_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace fl_students_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareerController : ControllerBase
    {
        private readonly IMongoCollection<Career> _collection;

        public CareerController(IMongoClient client, IConfiguration config)
        {
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<Career>("Careers");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var careers = await _collection.Find(_ => true).ToListAsync();
            return Ok(careers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var career = await _collection.Find(c => c.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefaultAsync();
            return career is null ? NotFound() : Ok(career);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Career career)
        {
            career.Id = MongoDB.Bson.ObjectId.GenerateNewId();
            await _collection.InsertOneAsync(career);
            return CreatedAtAction(nameof(GetById), new { id = career.Id.ToString() }, career);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Career updated)
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
        [HttpGet("by-faculty/{facultyId}")]
        public async Task<IActionResult> GetByFaculty(string facultyId, [FromServices] IMongoClient client, [FromServices] IConfiguration config)
        {
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            var facultyCollection = db.GetCollection<Faculty>("Faculties");

            var careers = await _collection.Find(c => c.FacultyId == MongoDB.Bson.ObjectId.Parse(facultyId)).ToListAsync();

            var faculty = await facultyCollection.Find(f => f.Id == MongoDB.Bson.ObjectId.Parse(facultyId)).FirstOrDefaultAsync();

            var result = careers.Select(c => new
            {
                c.Id,
                c.Name,
                c.TotalSemesters,
                Faculty = faculty?.Name
            });

            return Ok(result);
        }

    }
}
