using System;

namespace AzureBlobStorageSampleApp.Shared
{
    public class PhotoBlobModelPlusId
    {
        public DateTimeOffset CreatedAt { get; set; }
        public byte[] Image { get; set; }
        public string Id { get; set; }
    }
}
