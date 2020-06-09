using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO;

namespace WebApp.ApiControllers.Functionality
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class QuizControllerAdmin : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuizControllerAdmin(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles="admin")]
        public async Task<ActionResult<QuizWithAnswersDTO>> GetQuizWithAnswers(Guid id)
        {
            var domainQuiz = await _context.Quizzes.FindAsync(id);

            if (domainQuiz == null)
                return NotFound();
            
            var quiz = new QuizWithAnswersDTO()
            {
                Id = domainQuiz.Id,
                Title = domainQuiz.Title,
                TextEntryQuestions = _context.TextEntryQuestions.Where(q => q.QuizId == id).Select(q =>  new TextEntryQuestionWithAnswersDTO()
                {
                    Id = q.Id, 
                    Question = q.Question,
                    Answers = q.Answers.Select(a => new TextEntryAnswerDTO()
                    {
                        AppUserId = a.AppUserId,
                        Id = a.Id,
                        Answer = a.Answer
                    }).ToList()
                }).ToList()
            };

            return quiz;
        }
    }
}