using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using AzureBlobStorageSampleApp.Shared;

namespace AzureBlobStorageSampleApp.Functions
{
    public static class PostBlobPlusId
    {
        #region Methods
        [FunctionName(nameof(PostBlobPlusId))]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "PostBlobPlusId/{title}")]HttpRequestMessage req, string title, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var imageBlobWithId = await JsonService.DeserializeMessage<PhotoBlobModelPlusId>(req).ConfigureAwait(false);
                var photo = await PhotosBlobStorageService.SavePhoto(imageBlobWithId.Image, title).ConfigureAwait(false);

                //ADDING ID COMPATABILITY
                photo.Id = imageBlobWithId.Id;

                //ALREADY ID COMPATIBLE
                //await PhotoDatabaseService.InsertPhoto(photo).ConfigureAwait(false);

                photo.CreatedAt = imageBlobWithId.CreatedAt;

                //BELIEVE THIS SHOULD BE TO ADJUST UPDATE TIME
                await PhotoDatabaseService.UpdatePhoto(photo).ConfigureAwait(false);


                return new CreatedResult(photo.Url, photo);
            }
            catch(Exception e)
            {
                log.LogError(e, e.Message);
                return new InternalServerErrorResult();
            }
        }
        #endregion
    }
}
