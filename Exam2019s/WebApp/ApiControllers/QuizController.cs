using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuizController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDTOSummary>>> GetQuizzes()
        {
            return await _context.Quizzes.Select(domainQuiz => new QuizDTOSummary()
            {
                Id = domainQuiz.Id, 
                Title = domainQuiz.Title, 
                QuestionCount = domainQuiz.TextEntryQuestions == null ? 0 : domainQuiz.TextEntryQuestions.Count
            }).ToListAsync();
        }

        // GET: api/Quiz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDTO>> GetQuiz(Guid id)
        {
            var domainQuiz = await _context.Quizzes.FindAsync(id);

            if (domainQuiz == null)
                return NotFound();
            
            var quiz = new QuizDTO()
            {
                Id = domainQuiz.Id,
                Title = domainQuiz.Title,
                TextEntryQuestions = _context.TextEntryQuestions.Where(q => q.QuizId == id).Select(q =>  new TextEntryQuestionDTO()
                {
                    Id = q.Id, 
                    Question = q.Question,
                }).ToList()
            };

            return quiz;
        }
        
        // POST: api/Quiz
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles="admin")]
        public async Task<ActionResult> PostQuiz(CreateQuizDTO quiz)
        {
            var domainQuiz = new Domain.App.Quiz()
            {
                Title = quiz.Title
            };
            await _context.Quizzes.AddAsync(domainQuiz);
            await _context.SaveChangesAsync();
        
            return Ok(domainQuiz.Id);
        }
    }
}
