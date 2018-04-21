using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FacesApp.Functions
{
    public static class AnalizeFaceFunction
    {
        private static string _url = "https://southcentralus.api.cognitive.microsoft.com/vision/v1.0/analyze?visualFeatures=Faces&language=en";

        [FunctionName(nameof(AnalizeFaceFunction))]
        public static async Task Run(
            [BlobTrigger("facescontainer/{name}.png", Connection = "")]Stream image,
            string name,
            [Table("facestable", Connection = "")] IAsyncCollector<FaceRectangle> outTable,
            TraceWriter log)
        {
            string apiResult = await CallVisionApiAsync(image, log);
            log.Info($"Resultado de la operación: {apiResult}");

            if (!string.IsNullOrEmpty(apiResult))
            {
                ImageData imageData = JsonConvert.DeserializeObject<ImageData>(apiResult);
                log.Info($"imagenes:{imageData}");
                foreach (Face face in imageData.Faces)
                {
                    FaceRectangle faceRectangle = face.FaceRectangle;
                    faceRectangle.RowKey = Guid.NewGuid().ToString();
                    faceRectangle.PartitionKey = "AzureBootCamp";
                    faceRectangle.ImageFile = name + ".png";
                    faceRectangle.Age = face.Age;
                    faceRectangle.Gender = face.Gender;
                    await outTable.AddAsync(faceRectangle);
                }
            }
        }

        private static async Task<string> CallVisionApiAsync(Stream image, TraceWriter log)
        {
            using (HttpClient client = new HttpClient())
            {
                StreamContent content = new StreamContent(image);

                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("Vision_API_Subscription_Key"));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                HttpResponseMessage httpResponse = await client.PostAsync(_url, content);

                log.Info($"Estatus code {httpResponse.StatusCode}");
                log.Info($"Content {await httpResponse.Content.ReadAsStringAsync()}");
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    return await httpResponse.Content.ReadAsStringAsync();
                }
            }
            return null;
        }
    }
}