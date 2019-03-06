using Microsoft.EntityFrameworkCore;

namespace Poll.Models {
    public class DefaultContext : DbContext {

        public virtual DbSet<PollQuestion> Questions { get; set; }
        public virtual DbSet<PollAnswer> Answers { get; set; }

        public virtual DbSet<UserAnswer> UserAnswers { get; set; }

        public virtual DbSet<UserAnswerSelectData> AnswerSelectData { get; set; }

        public DefaultContext(DbContextOptions<DefaultContext> opts) : base(opts) { }

        protected override void OnModelCreating(ModelBuilder builder) {

            base.OnModelCreating(builder);

            var initQuestions = new[] {
                new PollQuestion() {
                    Id = 1,
                    QuestionType = QuestionType.Single,
                    Text = "Укажите ваш пол"
                },
                new PollQuestion() {
                    Id = 2,
                    QuestionType = QuestionType.Multi,
                    Text = "Марка вашего телефона"
                }
            };

            builder.Entity<PollQuestion>().HasData(initQuestions);

            builder.Entity<PollAnswer>(entity => {
                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey("QuestionId");
            });

            builder.Entity<PollAnswer>().HasData(GetInitAnswers(initQuestions));

            builder.Entity<UserAnswer>();

            builder.Entity<UserAnswerSelectData>();

        }

        protected PollAnswer[] GetInitAnswers(PollQuestion[] initQuestions) {

            return new[] {
                new PollAnswer() {
                    Id = 1,
                    Text = "Мужской",
                    QuestionId = initQuestions[0].Id
                },
                new PollAnswer() {
                    Id = 2,
                    Text = "Женский",
                    QuestionId = initQuestions[0].Id
                },
                new PollAnswer() {
                    Id = 3,
                    Text = "Apple",
                    QuestionId = initQuestions[1].Id
                },
                new PollAnswer() {
                    Id = 4,
                    Text = "Samsung",
                    QuestionId = initQuestions[1].Id
                },
                new PollAnswer() {
                    Id = 5,
                    Text = "LG",
                    QuestionId = initQuestions[1].Id
                },
                new PollAnswer() {
                    Id = 6,
                    Text = "Nokia",
                    QuestionId = initQuestions[1].Id
                },
                new PollAnswer() {
                    Id = 7,
                    Text = "Другое",
                    QuestionId = initQuestions[1].Id
                }

            };

        }
    }
}

