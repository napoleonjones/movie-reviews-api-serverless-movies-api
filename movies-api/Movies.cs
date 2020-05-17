using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace movies_api
{
    public static class Movies
    {
        const string get = "get";
        const string post = "post";
        const string patch = "patch";

        [FunctionName("movies")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", "patch", Route = null)] HttpRequest req,
            ILogger log)
        {
            switch (req.Method.ToLower())
            {
                case get:
                    {
                        return new JsonResult("get");
                    }

                case post:
                    {
                        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                        dynamic data = JsonConvert.DeserializeObject(requestBody);
                        return new JsonResult("post");
                    }

                case patch:
                    {
                        return new JsonResult("patch");
                    }
                default:
                    return new BadRequestObjectResult("This API only accepts GET|POST|PATCH");
            }
        }
    }
}
