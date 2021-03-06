﻿using System;

using Xamarin.Forms;

using AzureBlobStorageSampleApp.Shared;
using AzureBlobStorageSampleApp.Mobile.Shared;
using Xamarin.Essentials;
using AzureBlobStorageSampleApp.ViewModels;
using AzureBlobStorageSampleApp.Shared.Models;

namespace AzureBlobStorageSampleApp
{
    public class SelectPhotoListPage : BaseContentPage<SelectPhotoListViewModel>
    {
        #region Constant Fields
        readonly ListView _geographyListView;
        public FilterValueModel FilterValueModel { get;set;}
        #endregion

        #region Constructors
        public SelectPhotoListPage(FilterValueModel filterValueModel)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            { 
                ViewModel.IsInternetConnectionActive = true;
            }
            else 
            { 
               ViewModel.IsInternetConnectionActive = false;
            }

            FilterValueModel = filterValueModel;

            _geographyListView = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                IsPullToRefreshEnabled = true,
                BackgroundColor = Color.Transparent,
                AutomationId = AutomationIdConstants.PhotoListView,
                SeparatorVisibility = SeparatorVisibility.None
            };
            _geographyListView.ItemSelected += HandleItemSelected;
            _geographyListView.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.AllPhotosList));
            _geographyListView.HasUnevenRows = true;

            switch (filterValueModel.PropertyToSort) 
                {
                   case nameof(PhotoModel.CityState):
                        _geographyListView.ItemTemplate = new DataTemplate(typeof(GeoViewCell));
                        
                        break;
                   case nameof(PhotoModel.BarcodeString):
                        _geographyListView.ItemTemplate = new DataTemplate(typeof(BarcodeViewCell));
                   
                        break;
                   case nameof(PhotoModel.CreatedAtString):
                        _geographyListView.ItemTemplate = new DataTemplate(typeof(DateTimeWithTimeViewCell));

                        break;
                    default :
                        _geographyListView.ItemTemplate = new DataTemplate(typeof(PhotoViewCell));


                        break;
                } 





            Title = PageTitles.PhotoListPage;

            var stackLayout = new StackLayout();
            stackLayout.Children.Add(_geographyListView);


            var relativeLayout = new RelativeLayout();
            //relativeLayout.Children.Add(searchBar, _photosListView,

            relativeLayout.Children.Add(stackLayout,
                                       Constraint.Constant(0),
                                       Constraint.Constant(0),
                                       Constraint.RelativeToParent(parent => parent.Width),
                                       Constraint.RelativeToParent(parent => parent.Height));

            Content = relativeLayout;
        }

        #endregion

        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            ViewModel.LoadItemsCommandWithFilterValue.Execute(FilterValueModel);

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            { 
                ViewModel.IsInternetConnectionActive = true;
            }
            else
            { 
                ViewModel.IsInternetConnectionActive = false;
            }

        }


        void HandleItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            { 
                var listView = sender as ListView;
                var selectedPhoto = e?.SelectedItem as PhotoModel;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (selectedPhoto != null)
                    {
                        //SWITCH AFER TEST - KEEP THIS FOR NOW
                        await Navigation.PushAsync(new PhotoDetailsPage(selectedPhoto));
                        //await Navigation.PushAsync(new SelectedPhotoListPage(selectedPhoto));
                        listView.SelectedItem = null;
                    }
                });
            } else
            { 
                DisplayAlert("Please connect to the Internet ", string.Empty, "OK");
            }

        }

        #endregion
    }
}

