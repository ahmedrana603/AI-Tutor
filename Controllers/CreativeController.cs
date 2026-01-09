using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AITutorWebsite.Controllers
{
    public class CreativeController : Controller
    {
        // Quiz questions data
        private static readonly List<QuizQuestion> QuizQuestions = new List<QuizQuestion>
        {
            new QuizQuestion { Id = 1, Question = "What is the capital of France?", OptionA = "London", OptionB = "Paris", OptionC = "Berlin", OptionD = "Madrid", CorrectAnswer = "B" },
            new QuizQuestion { Id = 2, Question = "Which planet is known as the Red Planet?", OptionA = "Venus", OptionB = "Mars", OptionC = "Jupiter", OptionD = "Saturn", CorrectAnswer = "B" },
            new QuizQuestion { Id = 3, Question = "What is the largest ocean on Earth?", OptionA = "Atlantic", OptionB = "Indian", OptionC = "Pacific", OptionD = "Arctic", CorrectAnswer = "C" },
            new QuizQuestion { Id = 4, Question = "Who painted the Mona Lisa?", OptionA = "Vincent van Gogh", OptionB = "Pablo Picasso", OptionC = "Leonardo da Vinci", OptionD = "Michelangelo", CorrectAnswer = "C" },
            new QuizQuestion { Id = 5, Question = "What is the chemical symbol for gold?", OptionA = "Go", OptionB = "Gd", OptionC = "Au", OptionD = "Ag", CorrectAnswer = "C" },
            new QuizQuestion { Id = 6, Question = "Which country is known as the Land of the Rising Sun?", OptionA = "China", OptionB = "Japan", OptionC = "Korea", OptionD = "Thailand", CorrectAnswer = "B" },
            new QuizQuestion { Id = 7, Question = "What is the smallest country in the world?", OptionA = "Monaco", OptionB = "Vatican City", OptionC = "Liechtenstein", OptionD = "San Marino", CorrectAnswer = "B" },
            new QuizQuestion { Id = 8, Question = "Which programming language was created by Microsoft?", OptionA = "Java", OptionB = "Python", OptionC = "C#", OptionD = "JavaScript", CorrectAnswer = "C" },
            new QuizQuestion { Id = 9, Question = "What is the fastest land animal?", OptionA = "Lion", OptionB = "Cheetah", OptionC = "Leopard", OptionD = "Tiger", CorrectAnswer = "B" },
            new QuizQuestion { Id = 10, Question = "Which year did World War II end?", OptionA = "1944", OptionB = "1945", OptionC = "1946", OptionD = "1947", CorrectAnswer = "B" },
            new QuizQuestion { Id = 11, Question = "What is the currency of Japan?", OptionA = "Won", OptionB = "Yuan", OptionC = "Yen", OptionD = "Dong", CorrectAnswer = "C" },
            new QuizQuestion { Id = 12, Question = "Which element has the atomic number 1?", OptionA = "Helium", OptionB = "Hydrogen", OptionC = "Lithium", OptionD = "Carbon", CorrectAnswer = "B" },
            new QuizQuestion { Id = 13, Question = "What is the largest mammal in the world?", OptionA = "African Elephant", OptionB = "Blue Whale", OptionC = "Giraffe", OptionD = "Hippopotamus", CorrectAnswer = "B" },
            new QuizQuestion { Id = 14, Question = "Which country hosted the 2020 Summer Olympics?", OptionA = "China", OptionB = "Japan", OptionC = "Brazil", OptionD = "United Kingdom", CorrectAnswer = "B" },
            new QuizQuestion { Id = 15, Question = "What is the hardest natural substance on Earth?", OptionA = "Gold", OptionB = "Iron", OptionC = "Diamond", OptionD = "Platinum", CorrectAnswer = "C" },
            new QuizQuestion { Id = 16, Question = "Which planet is closest to the Sun?", OptionA = "Venus", OptionB = "Mercury", OptionC = "Earth", OptionD = "Mars", CorrectAnswer = "B" },
            new QuizQuestion { Id = 17, Question = "What is the national flower of Pakistan?", OptionA = "Rose", OptionB = "Jasmine", OptionC = "Lotus", OptionD = "Tulip", CorrectAnswer = "B" },
            new QuizQuestion { Id = 18, Question = "Which scientist developed the theory of relativity?", OptionA = "Isaac Newton", OptionB = "Albert Einstein", OptionC = "Galileo Galilei", OptionD = "Stephen Hawking", CorrectAnswer = "B" },
            new QuizQuestion { Id = 19, Question = "What is the largest desert in the world?", OptionA = "Sahara", OptionB = "Antarctic", OptionC = "Arabian", OptionD = "Gobi", CorrectAnswer = "B" },
            new QuizQuestion { Id = 20, Question = "Which programming language is known for web development?", OptionA = "C++", OptionB = "Java", OptionC = "JavaScript", OptionD = "Assembly", CorrectAnswer = "C" }
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateQuiz()
        {
            // Randomly select 10 questions from the 20 available
            var random = new System.Random();
            var selectedQuestions = QuizQuestions.OrderBy(x => random.Next()).Take(10).ToList();
            
            return Json(new { success = true, questions = selectedQuestions });
        }
    }

    public class QuizQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; } = "";
        public string OptionA { get; set; } = "";
        public string OptionB { get; set; } = "";
        public string OptionC { get; set; } = "";
        public string OptionD { get; set; } = "";
        public string CorrectAnswer { get; set; } = "";
    }
}