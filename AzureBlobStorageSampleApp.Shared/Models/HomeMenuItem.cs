using System;
using System.Collections.Generic;
using System.Text;

namespace AzureBlobStorageSampleApp.Shared
{ 
    public enum MenuItemType
    {
        Browse,
        About,
        GeographyListPage
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
