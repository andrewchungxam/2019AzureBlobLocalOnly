using System;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using AsyncAwaitBestPractices.MVVM;
using AzureBlobStorageSampleApp.Shared;
using Xamarin.Forms;
using System.Collections.Generic;

namespace AzureBlobStorageSampleApp
{
    public class PhotoListViewModel : BaseViewModel
    {
        #region Fields
        bool _isRefreshing;
        bool _isBusy;
        ICommand _refreshCommand;
        ObservableCollection<PhotoModel> _allPhotosList;
        String _searchString;
        List<PhotoModel> unsortedPhotosList;

        #endregion

        #region Properties
        public ICommand RefreshCommand => _refreshCommand ??
            (_refreshCommand = new AsyncCommand(ExecuteRefreshCommand, continueOnCapturedContext: false));

        public PhotoListViewModel()
        {
            //SearchCommand = new Command(async () => await ExecuteSearchCommand());
            SearchCommand = new Command(() => ExecuteSearchCommand());

            }

        public ObservableCollection<PhotoModel> AllPhotosList
        {
            get => _allPhotosList;
            set => SetProperty(ref _allPhotosList, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public String SearchString
        {
            get => _searchString;
            set => SetProperty(ref _searchString, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public Command SearchCommand { get; set; }

        #endregion

        #region Methods
        //async Task ExecuteSearchCommand()
        void ExecuteSearchCommand()
        {   
            if(IsBusy)
                return;
            IsBusy = true;

             try
            {
                //throw new NotImplementedException();
                //AllPhotosList = new ObservableCollection<PhotoModel>(unsortedPhotosList.Where(x => x.Title == "Camo Jacket"));
                //AllPhotosList = new ObservableCollection<PhotoModel>(unsortedPhotosList.Join(x => x.Title == "Camo Jacket"));
                //AllPhotosList = new ObservableCollection<PhotoModel>(unsortedPhotosList.Where(x => x.Title.Contains("Camo")));

                if(this.SearchString==" ")
                { 
                    //AllPhotosList = new ObservableCollection<PhotoModel>(unsortedPhotosList.Where(x=>x.Title.Any()));//.Where(x => x.Title.Contains(this.SearchString)));
                    //return;

                    AllPhotosList.Clear();
                    foreach (var individualPhotos in unsortedPhotosList)
                    {
                        AllPhotosList.Add(individualPhotos);
                    }
                }
                else
                { 

                    AllPhotosList.Clear();
                    foreach (var individualPhotos in unsortedPhotosList.Where(x=>x.Title.Contains(this.SearchString)))
                    {
                        AllPhotosList.Add(individualPhotos);
                    }
                }

            }   
            catch (Exception e)
            {
                DebugServices.Log(e);
            }        
            finally
            { 
                IsBusy = false;
            }
        }

        async Task ExecuteRefreshCommand()
        {
            IsRefreshing = true;

            try
            {
//                var oneSecondTaskToShowSpinner = Task.Delay(1000);
                var oneSecondTaskToShowSpinner = Task.Delay(700);


                if (this.IsInternetConnectionActive == true) { 
                //NOT SURE WE NEED THIS ONE IN THE LOCAL ONLY SCNEARIO
                    await DatabaseSyncService.SyncRemoteAndLocalDatabases().ConfigureAwait(false);
                }



                //var unsortedPhotosList = await PhotoDatabase.GetAllPhotos().ConfigureAwait(false);
                unsortedPhotosList = await PhotoDatabase.GetAllPhotos().ConfigureAwait(false);

                //AllPhotosList = new ObservableCollection<PhotoModel>(unsortedPhotosList.OrderBy(x => x.Title));
                AllPhotosList = new ObservableCollection<PhotoModel>(unsortedPhotosList.OrderBy(x => x.CreatedAt));

                await oneSecondTaskToShowSpinner.ConfigureAwait(false);
            }
            catch (Exception e)
            {
                DebugServices.Log(e);
            }
            finally
            {
                IsRefreshing = false;
            }
        }
        #endregion
    }
}