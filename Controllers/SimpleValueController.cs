using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OrleansWebAPI7AppDemo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;
using DotNetEnv;

namespace OrleansWebAPI7AppDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SimpleValueController : ControllerBase
    {

        public SimpleValueController()
        {
            Env.Load();
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
        public async Task<IActionResult> AnswerQuestion(string question)
        {
            try
            {
                var openAiEndpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
                var openAiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY");

                var openAiClient = new OpenAIClient(new Uri(openAiEndpoint), new AzureKeyCredential(openAiKey));

                var prompt = question;
                var completionsResponse = await openAiClient.GetCompletionsAsync("text-davinci-003", prompt);
                var completion = completionsResponse.Value.Choices[0].Text;

                return Ok(new { Question = question, Answer = completion });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
