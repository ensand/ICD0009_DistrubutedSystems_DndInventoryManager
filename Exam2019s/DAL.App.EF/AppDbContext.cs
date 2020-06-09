using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Quiz> Quizzes { get; set; } = default!;
        public DbSet<TextEntryQuestion> TextEntryQuestions { get; set; } = default!;
        public DbSet<TextEntryAnswer> TextEntryAnswers { get; set; }
        // public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; } = default!;
        // public DbSet<Answer> Answers { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            builder.Entity<Quiz>()
                .HasMany(quiz => quiz.TextEntryQuestions)
                .WithOne(teq => teq.Quiz!)
                .HasForeignKey(teq => teq.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Quiz>()
                .HasMany(quiz => quiz.MultipleChoiceQuestions)
                .WithOne(mcq => mcq.Quiz!)
                .HasForeignKey(mcq => mcq.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<MultipleChoiceQuestion>()
                .HasMany(mcq => mcq.Answers)
                .WithOne(a => a.MultipleChoiceQuestion!)
                .HasForeignKey(a => a.MultipleChoiceQuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}