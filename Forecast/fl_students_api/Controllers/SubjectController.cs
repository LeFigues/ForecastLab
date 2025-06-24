using fl_students_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace fl_students_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IMongoCollection<Subject> _collection;

        public SubjectController(IMongoClient client, IConfiguration config)
        {
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<Subject>("Subjects");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subjects = await _collection.Find(_ => true).ToListAsync();
            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var subject = await _collection.Find(s => s.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefaultAsync();
            return subject is null ? NotFound() : Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Subject subject)
        {
            subject.Id = MongoDB.Bson.ObjectId.GenerateNewId();
            await _collection.InsertOneAsync(subject);
            return CreatedAtAction(nameof(GetById), new { id = subject.Id.ToString() }, subject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Subject updated)
        {
            var result = await _collection.ReplaceOneAsync(s => s.Id == MongoDB.Bson.ObjectId.Parse(id), updated);
            return result.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _collection.DeleteOneAsync(s => s.Id == MongoDB.Bson.ObjectId.Parse(id));
            return result.DeletedCount == 0 ? NotFound() : NoContent();
        }
        [HttpGet("by-career/{careerId}")]
        public async Task<IActionResult> GetByCareer(string careerId)
        {
            var subjects = await _collection.Find(s => s.CareerId == MongoDB.Bson.ObjectId.Parse(careerId)).ToListAsync();
            return Ok(subjects);
        }

        [HttpGet("by-career/{careerId}/semester/{semester}")]
        public async Task<IActionResult> GetByCareerAndSemester(string careerId, int semester)
        {
            var subjects = await _collection.Find(s =>
                s.CareerId == MongoDB.Bson.ObjectId.Parse(careerId) &&
                s.Semester == semester
            ).ToListAsync();
            return Ok(subjects);
        }

    }
}
