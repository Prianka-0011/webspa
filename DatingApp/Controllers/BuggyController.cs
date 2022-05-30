using DatingApp.Data;
using DatingApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DatingApp.Controllers
{
   
    public class BuggyController : BaseController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }
      
        [HttpGet("auth")]
        public ActionResult<string> GetScreate()
        {
            return "secreat text";
        }
       
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);
            if (thing==null)
            {
                return NotFound();
            }
            return Ok(thing);
        }
      
        [HttpGet("server-error")]
        public ActionResult<string> GetSeverError()
        {
                var thing = _context.Users.Find(1);
                var thinkReturn = thing.ToString();
                return thinkReturn;
            
            
        }
        
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This is not a good request");
        }
    }
}
