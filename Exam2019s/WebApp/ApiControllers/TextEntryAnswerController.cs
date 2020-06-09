using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextEntryAnswerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TextEntryAnswerController(AppDbContext context)
        {
            _context = context;
        }
        
        // GET: api/TextEntryAnswer
        [HttpGet][Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles="admin")]
        public async Task<ActionResult<IEnumerable<TextEntryAnswerDTOSummary>>> GetTextEntryAnswers()
        {
            return await _context.TextEntryAnswers.Select(a => new TextEntryAnswerDTOSummary()
            {
                TextEntryQuestionId = a.TextEntryQuestionId,
                Id = a.Id,
                Answer = a.Answer
            }).ToListAsync();
        }
        
        // POST: api/TextEntryAnswer
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles="admin")]
        [Authorize(Roles="user")]
        public async Task<ActionResult<TextEntryAnswer>> PostTextEntryAnswer(CreateTextEntryAnswerDTO textEntryAnswer)
        {
            var domainAnswer = new Domain.App.TextEntryAnswer()
            {
                AppUserId = User.UserId(),
                TextEntryQuestionId = textEntryAnswer.TextEntryQuestionId,
                Answer = textEntryAnswer.Answer
            };
            await _context.TextEntryAnswers.AddAsync(domainAnswer);
            await _context.SaveChangesAsync();
        
            return Ok(domainAnswer.Id);
        }
    }
}
