using ContactAPI.Data;
using ContactAPI.DbContexts;
using ContactAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ContactController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContact request)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = request.Address,
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContact req)
        {

            var contact = await dbContext.Contacts.FindAsync(id);

            if(contact == null)
            {
                return NotFound();
            }
            contact.Email = req.Email;
            contact.Phone = req.Phone;
            contact.Address = req.Address;
            contact.FullName = req.FullName;

           await dbContext.SaveChangesAsync();

            return Ok(contact);

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {

            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);

        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {

            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }
             dbContext.Contacts.Remove(contact);
            await dbContext.SaveChangesAsync();
            return Ok();
             
        }

        [HttpDelete]
   
        public async Task<IActionResult> DeleteAllContact([FromRoute] Guid id)
        {

             dbContext.Contacts.RemoveRange(await dbContext.Contacts.ToListAsync());

            return Ok();

        }
    }
}
