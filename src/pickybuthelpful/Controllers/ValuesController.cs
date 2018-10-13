using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace pickypicky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static readonly IDictionary<int,string> store = new Dictionary<int,string> {
          { 1, "value1" },
          { 2, "value2" },
        };

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
          return Ok(store.Values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
          return store.ContainsKey(id)
            ? (ActionResult<string>) Ok(store[id])
            : NotFound();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] string value)
        {
          store[id] = value;
          return Created("", value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
          store.Remove(id);
          return NoContent();
        }
    }
}
