using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OrleansWebAPI7AppDemo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using static System.Environment;
using OpenAI_API;
using DotNetEnv;
using OrleansCodeGen.Orleans;

namespace OrleansWebAPI7AppDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SimpleValueController : ControllerBase
    {
        private string apiKey { get; set; } = String.Empty;

        public SimpleValueController()
        {
            
            string? api = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (api != null)
            {
                apiKey = api;
            }
            // Env.Load();
        }

        [HttpGet()]
        public String TestValue()
        {
            return "jaja";
        }

        [HttpGet()]
        public String Hi()
        {
            return "jojo";
        }

        [HttpGet()]
        public IEnumerable<string> TestArray()
        {
            string[] items = new string[] { "あいうえお", "かきくけこ", "さしすせそ" };
            return items;
        }

        [HttpGet()]
        public Animal TestObject()
        {
            Animal animal_1 = new Animal();
            animal_1.Name = "いぬ";
            return animal_1;
        }

        [HttpGet()]
        public IActionResult StatusCode()
        {
            return StatusCode(200);
        }

        [HttpPost()]
        public Animal TestPostObject(Animal animal)
        {
            animal.Age = 20;
            return animal;
        }
        [HttpGet("{question}")]
        [HttpGet("{question}")]
        public async Task<IActionResult> AnswerQuestion(string question)
        {
            try
            {
                apiKey = "sk - 2pUFyYfUx4fDoKJKe5oIT3BlbkFJNMneePz0uaH1jdttoboe";
                OpenAIAPI api = new OpenAIAPI(apiKey);

                string result = await api.Completions.GetCompletion(question);
                return Ok(new { Question = question, Answer = result.Trim() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
