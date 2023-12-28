using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab9.Models;

namespace lab9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ZavodRepository _repo = new ZavodRepository(new AppDbContext());
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<List<Zavod>> Get()
        {
            return await _repo.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zavod>> Get(int id)
        {
            var zavod = await _repo.GetById(id);
            if (zavod == null)
            {
                return NotFound();
            }
            return Ok(zavod);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<Zavod> Post([FromBody] Zavod value)
        {
            return await _repo.Create(value);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Zavod>> Put(int id, [FromBody] Zavod value)
        {
            var zavod = await _repo.Update(id, value);
            if (zavod == null)
            {
                return NotFound();
            }
            return Ok(zavod);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Zavod>> Delete(int id)
        {
            var zavod = await _repo.Delete(id);
            if (zavod == null)
            {
                return NotFound();
            }
            return Ok(zavod);
        }
    }
}
