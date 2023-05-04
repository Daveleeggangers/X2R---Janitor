using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using X2R.Insight.Janitor.WebApi.Dto;
using X2R.Insight.Janitor.WebApi.interfaces;
using X2R.Insight.Janitor.WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace X2R.Insight.Janitor.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JanitorController : ControllerBase
    {
        private readonly IQueryInterface _queryInterface;
        private readonly IMapper _mapper;

        public JanitorController(IQueryInterface queryInterface, IMapper mapper)
        {
            _queryInterface = queryInterface;
            _mapper = mapper;
        }

        // GET: api/Janitor     - gets all querys
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Querys>))]
        public IActionResult GetQuerys()
        {
            var Query = _mapper.Map<List<QueryDto>>(_queryInterface.GetQuerys());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Query);
        }

        // GET: api/Janitor     - gets all DateTimes
        [HttpGet("DateTimes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Querys>))]
        public IActionResult GetDateTimesQuery()
        {
            var query = _queryInterface.GetDateTimesQuery();

            foreach (var x in query)
            {
                if (x.DateTime_Start == DateTime.Now || x.DateTime_Start > DateTime.Now)
                {
                    var status = _queryInterface.GetStatus(x.TaskId);
                    if (status == "Active")
                    {
                        var execute = _queryInterface.ExecuteQuery(x.Query);
                        if (execute != 0)
                        {
                            if (x.RecurringSchedule == "Once")
                            {
                                _queryInterface.ChangeDetails(x.TaskId, execute.ToString());
                            }
                        }
                    }
                }
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(query);
        }
        [HttpGet("Execute")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ExecuteQuery(string query)
        {
            _queryInterface.ExecuteQuery(query);
            return Ok(query);
        }

        [ProducesResponseType(200, Type = typeof(Querys))]
        [ProducesResponseType(400)]
        // GET api/Janitor/5    - gets the selected id query
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Querys))]
        [ProducesResponseType(400)]
        public IActionResult GetQuery(int id)
        {
            if (!_queryInterface.QueryExists(id))
                return NotFound();

            var Query = _mapper.Map<QueryDto>(_queryInterface.GetQuery(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Query);
        }

        // POST api/<JanitorController>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQuery([FromBody] QueryDto queryCreate)
        {
            if (queryCreate == null)
                return BadRequest(ModelState);

            var query = _queryInterface.GetQuerys()
                .Where(c => c.TaskId == queryCreate.TaskId)
                .FirstOrDefault();

            if (query != null)
            {
                ModelState.AddModelError("", "Already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var queryMap = _mapper.Map<Querys>(queryCreate);

            if (!_queryInterface.CreateQuery(queryMap))
            {
                ModelState.AddModelError("", "Not Saved");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }
    }
}
