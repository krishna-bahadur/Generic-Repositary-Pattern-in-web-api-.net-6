using Domain.Interfaces;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessEF;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IGenericRepo<User> _repositary;

        public PersonController(IGenericRepo<User> personRepository)
        {
            _repositary = personRepository;
        }


        [HttpGet]
        [Route("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                //check if any item exists in the database or not
                if (!_repositary.Exists())
                {
                    return NotFound();
                }
                //get all data from database
                var users = await _repositary.GetAll();
                return Ok(users);
            }
            catch
            {
                throw new Exception();
            }
            
        }
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User model)
        {
            //check if model is valid or not
            if (ModelState.IsValid)
            {
                _repositary.Add(model);
                var affectedRows =  await _repositary.Save();
                
                //check if savechange has been successfull or not
                if(affectedRows > 0)
                    return Ok(new Response { Id = model.Id, Status = "Sucess" });
                else
                    return BadRequest();
            }
            return BadRequest(new Response { Status="Failed"});
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                //check if any item exists in the database or not
                if (!_repositary.Exists())
                {
                    return NotFound("Items in Database not found");
                }
                //check if id exist or not
                var user = await _repositary.GetById(id);
                if (user == null)
                {
                    return NotFound("User not Found");
                }
                return Ok(user);
            }
            catch(CustomException ex)
            {
                throw new CustomException(ex.Message);
            }
            
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //check if any item exists in the database or not
            if (!_repositary.Exists())
            {
                return NotFound("No items in Database");
            }
            //check if id exist or not
            var checkId = await _repositary.GetById(id);
            if(checkId == null)
            {
                return NotFound("Id Not Found");
            }
            //delete data from db
            _repositary.Delete(id);
            int affectedRows = await _repositary.Save();
            if(affectedRows > 0)
                return Ok(new Response { Id = id, Status = "Sucess" });
            else
                return NoContent();

        }
        [HttpPatch]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, User model)
        {
            if (!_repositary.Exists())
            {
                return NotFound("No items in Database");
            }
            //check if id exist or not
            var user = await _repositary.GetById(id);
            if (user == null)
            {
                return NotFound("Id Not Found");
            }
            model.Id = user.Id;
            _repositary.Update(model);
           var affectedRows = await _repositary.Save();
            if (affectedRows > 0)
                return Ok();
            else
                return BadRequest("Failed to delete");
        }

        [HttpPatch]
        [Route("Edit")]
        public async Task<IActionResult> Edit(User user)
        {
            _repositary.Edit(user);
            var affectedRows = await _repositary.Save();
            if (affectedRows > 0)
                return Ok("Saved");
            else
                return Problem("Problem");
        }
    }
}
