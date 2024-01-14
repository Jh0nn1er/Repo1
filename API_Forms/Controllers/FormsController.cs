using API_Forms.Data;
using API_Forms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Forms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public FormsController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetContactForms()
        {
            
            return Ok(_db.Contacts.ToList());
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetForm(int id)
        {
            if(id==0) 
            {
                return BadRequest();
            }

            var form = _db.Contacts.FirstOrDefault(v => v.Id == id);

            if (form == null)
            {
                return NotFound(); 
            }

            return Ok(form);
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateForms([FromBody] Contact formData)
        {
            if (_db.Contacts.FirstOrDefault(v => v.Email.ToLower() == formData.Email.ToLower()) != null)
            {
                ModelState.AddModelError("mailExists", "a form with that email already exists");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contact newContact = new()
                {
                    Id= formData.Id,
                    FullName = formData.FullName,
                    Email = formData.Email,
                    DocumentType = formData.DocumentType,
                    Identifier = formData.Identifier,
                    Comment = formData.Comment,
                };
                _db.Add(newContact);
                await _db.SaveChangesAsync();

                return Ok(new { id = newContact.Id });
           
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteForms(int id) 
        { 
        if (id == 0)
            {
                return BadRequest();
            }
            var Form =_db.Contacts.FirstOrDefault(v=> v.Id == id);
            if(Form == null)
            {
                return NotFound(); 
            }
            _db.Contacts.Remove(Form);
            _db.SaveChanges();

            return NoContent();
        }
        [HttpPut]
        public  IActionResult UpdateForms(int id, [FromBody] Contact formData) 
        {
            if(formData == null || id!=formData.Id)
            {
                return BadRequest();    
            }
            Contact newContact = new()
            {
                Id = formData.Id,
                FullName = formData.FullName,
                Email = formData.Email,
                DocumentType = formData.DocumentType,
                Identifier = formData.Identifier,
                Comment = formData.Comment,
            };
            _db.Update(newContact);
            return NoContent();

        }
      

    }
}   
