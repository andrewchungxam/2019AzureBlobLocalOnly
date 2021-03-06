﻿using System;

namespace AzureBlobStorageSampleApp.Shared
{
    public class PhotoBlobModelPlusId
    {
        public DateTimeOffset CreatedAt { get; set; }
        public byte[] Image { get; set; }

        public string Id { get; set; }


        public float Lat { get;set;}
        public float Long { get;set;}

        public string City {get;set;}
        public string LocationState {get;set;}
        public string Country {get;set;}
        public string CityState {get;set;}



        public string Tag1 { get;set;}
        public string Tag2 { get;set;}
        public string Tag3 { get;set;}
        public string Tag4 { get;set;}
        public string Tag5 { get;set;}
        public string Tag6 { get;set;}
        public string Tag7 { get;set;}
        public string Tag8 { get;set;}
        public string Tag9 { get;set;}
        public string Tag10 { get;set;}
        public string TagsSeperatedWithSpaces { get;set;}
        public string CustomTag1 { get;set;}
        public string CustomTag2 { get;set;}
        public string CustomTag3 { get;set;}
        public string CustomTag4 { get;set;}
        public string CustomTag5 { get;set;}
        public string CustomTag6 { get;set;}
        public string CustomTag7 { get;set;}
        public string CustomTag8 { get;set;}
        public string CustomTag9 { get;set;}
        public string CustomTag10 { get;set;}
        public string CustomTagsSeperatedWithSpaces { get;set;}

        public string CreatedAtString { get; set; }

        public string BarcodeString { get;set;}

    }
}
