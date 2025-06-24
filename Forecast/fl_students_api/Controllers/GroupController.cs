using fl_students_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace fl_students_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMongoCollection<Group> _collection;

        public GroupController(IMongoClient client, IConfiguration config)
        {
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<Group>("Groups");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _collection.Find(_ => true).ToListAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var group = await _collection.Find(g => g.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefaultAsync();
            return group is null ? NotFound() : Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Group group)
        {
            group.Id = MongoDB.Bson.ObjectId.GenerateNewId();
            await _collection.InsertOneAsync(group);
            return CreatedAtAction(nameof(GetById), new { id = group.Id.ToString() }, group);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Group updated)
        {
            var result = await _collection.ReplaceOneAsync(g => g.Id == MongoDB.Bson.ObjectId.Parse(id), updated);
            return result.MatchedCount == 0 ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _collection.DeleteOneAsync(g => g.Id == MongoDB.Bson.ObjectId.Parse(id));
            return result.DeletedCount == 0 ? NotFound() : NoContent();
        }
        [HttpGet("by-subject/{subjectId}")]
        public async Task<IActionResult> GetBySubject(string subjectId)
        {
            var groups = await _collection.Find(g => g.SubjectId == MongoDB.Bson.ObjectId.Parse(subjectId)).ToListAsync();
            return Ok(groups);
        }

        [HttpGet("by-cycle/{cycleId}")]
        public async Task<IActionResult> GetByCycle(string cycleId)
        {
            var groups = await _collection.Find(g => g.CycleId == MongoDB.Bson.ObjectId.Parse(cycleId)).ToListAsync();
            return Ok(groups);
        }

        // Avanzado: Buscar por carrera (requiere lookup manual)
        [HttpGet("by-career/{careerId}")]
        public async Task<IActionResult> GetByCareer(string careerId, [FromServices] IMongoClient client, [FromServices] IConfiguration config)
        {
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            var subjectCollection = db.GetCollection<Subject>("Subjects");

            var subjects = await subjectCollection.Find(s => s.CareerId == MongoDB.Bson.ObjectId.Parse(careerId)).ToListAsync();
            var subjectIds = subjects.Select(s => s.Id).ToList();

            var groups = await _collection.Find(g => subjectIds.Contains(g.SubjectId)).ToListAsync();

            return Ok(groups);
        }
        [HttpGet("full")]
        public async Task<IActionResult> GetFullGroups(
    [FromServices] IMongoClient client,
    [FromServices] IConfiguration config)
        {
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);

            var subjectsCollection = db.GetCollection<Subject>("Subjects");
            var teachersCollection = db.GetCollection<Teacher>("Teachers");
            var cyclesCollection = db.GetCollection<Cycle>("Cycles");

            var groups = await _collection.Find(_ => true).ToListAsync();

            var subjectIds = groups.Select(g => g.SubjectId).Distinct().ToList();
            var teacherIds = groups.Select(g => g.TeacherId).Distinct().ToList();
            var cycleIds = groups.Select(g => g.CycleId).Distinct().ToList();

            var subjects = await subjectsCollection.Find(s => subjectIds.Contains(s.Id)).ToListAsync();
            var teachers = await teachersCollection.Find(t => teacherIds.Contains(t.Id)).ToListAsync();
            var cycles = await cyclesCollection.Find(c => cycleIds.Contains(c.Id)).ToListAsync();

            var result = groups.Select(g => new
            {
                groupId = g.Id.ToString(),
                studentCount = g.StudentCount,
                subject = subjects.FirstOrDefault(s => s.Id == g.SubjectId)?.Name ?? "Desconocido",
                teacher = teachers.FirstOrDefault(t => t.Id == g.TeacherId)?.FullName ?? "Desconocido",
                cycle = cycles
                    .Where(c => c.Id == g.CycleId)
                    .Select(c => new { c.Year, c.Semester })
                    .FirstOrDefault() ?? new { Year = 0, Semester = 0 }
            });

            return Ok(result);
        }

    }
}
