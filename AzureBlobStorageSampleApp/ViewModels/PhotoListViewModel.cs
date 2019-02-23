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
                    //var searchResults = await indexClient.Documents.SearchAsync<Monkey>(text);
                    foreach (var individualPhotos in unsortedPhotosList)
                    {
                        AllPhotosList.Add(individualPhotos);
                    }
                }
                else
                { 
                    //AllPhotosList = new ObservableCollection<PhotoModel>(unsortedPhotosList.Where(x=>x.Title.Any()));//.Where(x => x.Title.Contains(this.SearchString)));

                    //AllPhotosList.Clear();
                    //AllPhotosList = new ObservableCollection<PhotoModel>(unsortedPhotosList.Where(x => x.Title.Contains(this.SearchString)));
                    //return; 

                        //async Task AzureSearch(string text)
                        //{
                            AllPhotosList.Clear();
                            //var searchResults = await indexClient.Documents.SearchAsync<Monkey>(text);
                            foreach (var individualPhotos in unsortedPhotosList.Where(x=>x.Title.Contains(this.SearchString)))
                            {
                                AllPhotosList.Add(individualPhotos);
                            }
                        //}

//https://github.com/xamarin/xamarin-forms-samples/bl                ob/master/WebServices/        AzureSea                rch/MonkeyAp                p/ViewModel        s/Search        PageViewModel.cs        
//https://github        .com/xamarin/xamarin-forms-samples/blob/        master/WebServices/AzureSearch/MonkeyApp/ViewModels/SearchPageViewModel.cs
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


//voi        d ExecuteSearchCommand()
//        {
//             try
//                    {
//                //throw new NotImplemen        tedException();
//                                //AllPhotosList = new Observab        leCollection<PhotoMod        el>(unsortedPhotosList.Where(x => x.Title == "Camo Jacket"));
//                //Al        lPhotosList = new ObservableCo        llection<PhotoModel>        (unsortedPhotosList.Joi        n(x => x.Title                 == "Camo Jacket"));
//                        //AllPhotosL        ist = new ObservableCol        lection<PhotoModel>(u        nsortedPhotosList.Where(x => x.Title.Contains("Camo")));

//                        if(this.SearchString==" ")
//                        { 
//                    AllPhotosList = new ObservableCollection<PhotoM        odel>(unsortedPhotosList.Where(x=>x.Title.Any                ()));//.Where(x => x.Title.Conta        ins(this.SearchString)));
//                    return;
//                }
//                        else
//                                { 
//                                                AllPhoto        sList = new ObservableCollection<PhotoModel>(unsorte        dPhotosList.Where(x=>x.Title.Any()));//.Wher        e(x => x.        Title.Contains(this.SearchString)));

//                            AllPhotosList.Clear();
//                            AllPhotosList = new ObservableCollection<Pho        toModel>(unsortedPhotosList        .Where(x => x.Title.Contains(this.Sear        chString)));
//                    return; 

//                                                        async Task AzureSearch(string te                xt)
//                                        {
//                                                            Monkeys.Clear();

//                                                    var se        archResults = await indexClient.Documents.Se        archAsync<Monkey>(text);
//                                                    foreach (SearchResul                t<Monkey> result in searchResults.Results        )
//                                                            {
//                                                Monkeys.Add(new Monkey
//                                                                {
//                                                                            Name = resu        lt.Document.Name,
//                                                                    Location = result.Do        cument.Location,
//                                                            Details = result.Document.Details,
//                                                                            ImageUrl = result.Document.ImageUrl
//                                                                });
//                                                    }
//                                        }

//                         
                   

             va
        lList =         new List<
        >();

/
                                       
        ([..])


           




                        {
//                                                  originalList.Add(someInstance);
//                                        }

//                  
                   

        leColle
        eClass> uiCollection = ne
        bleColl
        meClass>(originalList);




///
        github.

        n/x




        samples/blob/master/WebServices/AzureSearch/MonkeyApp/ViewModels/SearchPageViewModel.cs

////https://github.com/xamarin/xamarin-forms-samples/blob/master/WebServices/AzureSearch/MonkeyApp/ViewModels/SearchPageViewModel.cs
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        DebugServices.Log(e);
        //    }

        //}




