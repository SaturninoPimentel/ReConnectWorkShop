using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace FacesApp.Functions
{
    public static class GetFacesFuntion
    {
        [FunctionName(nameof(GetFacesFuntion))]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req,
            [Table("facestable", "AzureBootCamp")]IQueryable<FaceRectangle> table,
            TraceWriter log)
        {
            return req.CreateResponse(HttpStatusCode.OK, table.ToList());
        }
    }
}