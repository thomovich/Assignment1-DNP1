using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment1_DNP1.Data;
using Microsoft.AspNetCore.Mvc;
using Models;


namespace TodosWebAP.Controllers {
    
    [ApiController]
    [Route("[controller]")]
    public class AdultController : Controller
    {
        private IAdultdata websiteData;

        public AdultController(IAdultdata websiteData)
        {
            this.websiteData = websiteData;
        }
        [HttpGet]
        public async Task<ActionResult<IList<Adult>>> GetTodos([FromQuery] int? userId, [FromQuery] bool? isCompleted) {
            try
            {
                IList<Adult> adults = await websiteData.GetAdultsAsync();
                return Ok(adults);
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteAdult([FromRoute] int id)
        {
            try
            {
                await websiteData.RemoveAdult(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Adult>> AddAdult([FromBody] Adult adult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Adult added = await websiteData.AddAdult(adult);
                return Created($"/{added.Id}", added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Adult>> UpdateAdult([FromBody] Adult adult)
        {
            try
            {
                Adult updatedAdult = await websiteData.UpdateAsync(adult);
                return Ok(updatedAdult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}