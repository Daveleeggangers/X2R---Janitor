using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using X2R.Insight.Janitor.WebApi.Dto;
using X2R.Insight.Janitor.WebApi.interfaces;
using X2R.Insight.Janitor.WebApi.Models;


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
        [ProducesResponseType(200, Type = typeof(IEnumerable<_Querys>))]
        public IActionResult GetQuerys()
        {
            var Query = _mapper.Map<List<QueryDto>>(_queryInterface.GetQuerys());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Query);
        }

        // GET: api/Janitor     - gets all DateTimes
        [HttpGet("DateTimes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<_Querys>))]
        public IActionResult GetDateTimesQuery()
        {
            var query = _queryInterface.GetDateTimesQuery();

            foreach (var x in query)
            {
                if (x.DateTime_Start < DateTime.Now)
                {
                    var status = _queryInterface.GetStatus(x.TaskId);
                    if (status == "Active")
                    {
                        var execute = _queryInterface.ExecuteQuery(x.Query);
                        if (Convert.ToInt32(execute) != 0)
                        {
                            if (x.RecurringSchedule == "Once")
                            {
                                _queryInterface.ChangeStatus(x.TaskId);
                                _queryInterface.ChangeDetails(x.TaskId, execute.ToString());
                            }
                            if (x.RecurringSchedule == "Multiple")
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

        // GET: Ability to execute a since query filled in
        [HttpGet("Execute")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ExecuteQuery(string query)
        {
            _queryInterface.ExecuteQuery(query);
            return Ok(query);
        }

        // GET api/Janitor/5    - gets the selected id query
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(_Querys))]
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

        // Ability to fill in a new row in the Querys table
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

            var queryMap = _mapper.Map<_Querys>(queryCreate);

            if (!_queryInterface.CreateQuery(queryMap))
            {
                ModelState.AddModelError("", "Not Saved");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }
    }
}
