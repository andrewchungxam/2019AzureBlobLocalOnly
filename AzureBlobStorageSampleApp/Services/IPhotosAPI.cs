﻿using System.Collections.Generic;
using System.Threading.Tasks;

using AzureBlobStorageSampleApp.Shared;

using Refit;

namespace AzureBlobStorageSampleApp
{
    [Headers("Accept-Encoding: gzip",
                "Accept: application/json")]
    public interface IPhotosAPI
    {
        [Get("/GetPhotos")]
        Task<List<PhotoModel>> GetAllPhotoModels();


        //#TODO - if not using Function with AuthorizationLevel.Anonymous, add something similar to the following (Also be user to make changes in APIService.cs)
        [Post("/PostBlob/{photoTitle}")]
        Task<PhotoModel> PostPhotoBlob([Body]PhotoBlobModel photoBlob, string photoTitle, [AliasAs("code")]string functionKey);

        [Post("/PostBlobPlusId/{photoTitle}")]
        Task<PhotoModel> PostPhotoBlobPlusId([Body]PhotoBlobModelPlusId photoBlob, string photoTitle, [AliasAs("code")]string functionKey);

        [Post("/PostPhoto/{photoTitle}")]
        Task<PhotoModel> PostPhoto([Body]PhotoModel photoModel, string photoTitle, [AliasAs("code")]string functionKey);

        [Post("/PatchPhoto/{photoTitle}")]
        Task<PhotoModel> PatchPhoto([Body]PhotoModel photoModel, string photoTitle, [AliasAs("code")]string functionKey);

        //[Post("/PostBlob/{photoTitle}")]
        //Task<PhotoModel> PostPhotoBlob([Body]PhotoBlobModel photoBlob, string photoTitle);

    }
}
