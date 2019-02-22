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

                //CARRY OVER ADDIIONAL FIELDS
                photo.Tag1 = imageBlob.Tag1;
                photo.Tag2 = imageBlob.Tag2;
                photo.Tag3 = imageBlob.Tag3;
                photo.Tag4 = imageBlob.Tag4;
                photo.Tag5 = imageBlob.Tag5;
                photo.Tag6 = imageBlob.Tag6;
                photo.Tag7 = imageBlob.Tag7;
                photo.Tag8 = imageBlob.Tag8;
                photo.Tag9 = imageBlob.Tag9;
                photo.Tag10 = imageBlob.Tag10;

                photo.TagsSeperatedWithSpaces = imageBlob.TagsSeperatedWithSpaces;

                photo.CustomTag1 = imageBlob.CustomTag1;
                photo.CustomTag2 = imageBlob.CustomTag2;
                photo.CustomTag3 = imageBlob.CustomTag3;
                photo.CustomTag4 = imageBlob.CustomTag4;
                photo.CustomTag5 = imageBlob.CustomTag5;
                photo.CustomTag6 = imageBlob.CustomTag6;
                photo.CustomTag7 = imageBlob.CustomTag7;
                photo.CustomTag8 = imageBlob.CustomTag8;
                photo.CustomTag9 = imageBlob.CustomTag9;
                photo.CustomTag10 = imageBlob.CustomTag10;

                photo.CustomTagsSeperatedWithSpaces = imageBlob.CustomTagsSeperatedWithSpaces;
                photo.CreatedAtString = imageBlob.CreatedAtString;

                photo.City = City;
                photo.LocationState = LocationState;
                photo.Country = LocationState;
                photo.CityState = LocationState;

                photo.Lat = Lat;
                photo.Long = Long;

                //BELIEVE THIS SHOULD BE TO ADJUST UPDATE TIME
                await PhotoDatabaseService.InsertUpdatedPhoto(photo).ConfigureAwait(false);


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
