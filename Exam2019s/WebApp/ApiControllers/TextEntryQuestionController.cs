using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextEntryQuestionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TextEntryQuestionController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/TextEntryQuestion
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles="admin")]
        public async Task<ActionResult> PostTextEntryQuestion(CreateTextEntryQuestionDTO textEntryQuestion)
        {
            var domainQuestion = new Domain.App.TextEntryQuestion()
            {
                QuizId = textEntryQuestion.QuizId,
                Question = textEntryQuestion.Question,
            };
            await _context.TextEntryQuestions.AddAsync(domainQuestion);
            await _context.SaveChangesAsync();
        
            return Ok(domainQuestion.Id);
        }
    }
}
