using Microsoft.AspNetCore.Mvc;
using MiPC4.Models;
using System.Diagnostics;
using Microsoft.ML;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Net.Mime.MediaTypeNames;

namespace MiPC4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Sentiment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sentiment(string textoIngresado)
        {
            // Crear una instancia de ModelInput con el texto del usuario.
            var input = new SentimentModel.ModelInput
            {
                Col0 = textoIngresado
            };

            // Predecir el sentimiento utilizando el modelo.
            var result = SentimentModel.Predict(input);

            // Determinar si el sentimiento es positivo o negativo.
            var sentiment = result.PredictedLabel == 1 ? "Positive" : "Negative";

            // Pasar el resultado y el texto original de vuelta a la vista.
            ViewBag.ResultadoSentimiento = sentiment;
            ViewBag.TextoIngresado = textoIngresado;
            return View("Sentiment");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}