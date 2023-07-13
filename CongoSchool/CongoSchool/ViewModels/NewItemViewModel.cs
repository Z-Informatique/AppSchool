using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using CongoSchool.Models;
using Xamarin.Forms;

using Xamarin.CommunityToolkit.Extensions;
using CongoSchool.Views;
using Xamarin.Essentials;

using CongoSchool.Helpers;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;

namespace CongoSchool.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class NewItemViewModel : BaseViewModel
    {
        private string nom;
        private string prenom;
        private string sexe;
        private DateTime dateNaissance;
        private string lieuNaissance;
        private string paysNaissance;
        private string natureCandidat;
        private string image;
        private string examen;
        private string serie;
        private bool showSerie;
        private bool imageVisible;
        private bool showSectionImage;
        private string btnTexte;
        private ImageSource imageSource;
        public Stream stream { get; set; } = new MemoryStream();

        public ImageSource SourceImage
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        public string BtnTexte
        {
            get => btnTexte;
            set => SetProperty(ref btnTexte, value);
        }

        public bool ShowSerie
        {
            get => showSerie;
            set => SetProperty(ref showSerie, value);
        }

        public bool ImageVisible
        {
            get => imageVisible;
            set => SetProperty(ref imageVisible, value);
        }

        public bool ShowSectionImage
        {
            get => showSectionImage;
            set => SetProperty(ref showSectionImage, value);
        }

        private string titre;
        public string Titre
        {
            get => titre;
            set => SetProperty(ref titre, value);
        }

        private int itemId;
        public int ItemId
        {
            get => itemId;
            set
            {
                itemId = value;
                if (itemId > 0)
                {
                    LoadItemId(value);
                    Titre = "Modifier un eleve";
                }
                OnPropertyChanged();
            }
        }

        public List<string> Genre { get; set; } = new List<string>
        {
            "Feminin",
            "Masculin"
        };
        public List<string> Natures { get; set; } = new List<string>
        {
            "Candidat officiel",
            "Candidat libre"
        };
        public List<string> Examens { get; set; } = new List<string>
        {
            "CEPE",
            "BEPC",
            "BAC"
        };
        public List<string> Series { get; set; } = new List<string>
        {
            "A4",
            "C",
            "D",
            "G1",
            "G2",
            "G3",
            "BG",
            "F3",
            "F1"
        };
        private Item item;
        public Item Item
        {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();
            }
        }
        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            TakePicAsync = new Command(TakePic);
            PicAsyncCommand = new Command(PicAsync);
            OpenCommand = new Command(OpenAsync);


            this.PropertyChanged += 
                (_, __) => SaveCommand.ChangeCanExecute();

            DateNaissance = DateTime.Today;

            Titre = "Ajouter un eleve";
            BtnTexte = "Inserer une photo (4X4)";
        }

        private void OpenAsync(object obj)
        {
            ShowSectionImage = !showSectionImage;
            ImageVisible = false;
        }
        private async void PicAsync(object obj)
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                }

                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Choisir une photo",
                });

                if (result != null)
                {
                    await LoadPhotoAsync(result);
                    ImageVisible = true;
                    //DependencyService.Get<IFileService>().SavePicture("ImageName.jpg", stream, Examen);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Congo school", ex.Message, "OK");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            if(photo == null)
            {
                Image = null;
                return;
            }
            //save the file into local storage 
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (Stream stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
            {
                await stream.CopyToAsync(newStream);
            }
            Image = newFile;
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(examen)
                && !String.IsNullOrWhiteSpace(natureCandidat)
                && !String.IsNullOrWhiteSpace(nom)
                && !String.IsNullOrWhiteSpace(prenom)
                && !String.IsNullOrWhiteSpace(sexe)
                && !String.IsNullOrWhiteSpace(lieuNaissance)
                && !String.IsNullOrWhiteSpace(paysNaissance);
        }

        public string Examen
        {
            get => examen;
            set => SetProperty(ref examen, value);
        }

        public string Nom
        {
            get => nom;
            set => SetProperty(ref nom, value);
        }

        public string Prenom
        {
            get => prenom;
            set => SetProperty(ref prenom, value);
        }

        public string SelectedGenre
        {
            get => sexe;
            set => SetProperty(ref sexe, value);
        }

        public string SelectedExamen
        {
            get => examen;
            set
            {
                examen = value;
                if (examen.Equals("BAC"))
                {
                    ShowSerie = true;
                }
                else
                {
                    ShowSerie = false;
                }
                OnPropertyChanged();
            }
        }

        public string SelectedNature
        {
            get => natureCandidat;
            set => SetProperty(ref natureCandidat, value);
        }

        public string SelectedSerie
        {
            get => serie;
            set => SetProperty(ref serie, value);
        }

        public DateTime DateNaissance
        {
            get => dateNaissance;
            set => SetProperty(ref dateNaissance, value);
        }

        public string LieuNaissance
        {
            get => lieuNaissance;
            set => SetProperty(ref lieuNaissance, value);
        }

        public string PaysNaissance
        {
            get => paysNaissance;
            set => SetProperty(ref paysNaissance, value);
        }

        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command TakePicAsync { get; }
        public Command PicAsyncCommand { get; }
        public Command OpenCommand { get; }


        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        private async void TakePic()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Camera>();
                }

                var status1 = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                if (status1 != PermissionStatus.Granted)
                {
                    status1 = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }

                var result = await MediaPicker.CapturePhotoAsync();

                if (result != null)
                {
                    await LoadPhotoAsync(result);
                    ImageVisible = true;
                    //DependencyService.Get<IFileService>().SavePicture("ImageName.jpg", stream, Examen);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Congo school", ex.Message, "OK");
            }
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Examen = SelectedExamen,
                DateNaissance = DateNaissance,
                LieuNaissance = LieuNaissance,
                NatureCandidat = SelectedNature,
                Nom = Nom.ToUpper(),
                Prenom = Prenom,
                PaysNaissance = PaysNaissance,
                Sexe = SelectedGenre,
                Image = Image
            };
            if (ShowSerie)
                newItem.Serie = SelectedSerie;

            if (ItemId > 0)
                newItem.Id = Item.Id;

            
            var result = await App.MockDataStore.AddItemAsync(newItem);
            if (result > 0)
            {
                await App.Current.MainPage.DisplayAlert("Succes", "Eleve enregistre avec succes", "Ok");
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }


        public async void LoadItemId(int itemId)
        {
            try
            {
                Item = await App.MockDataStore.GetItemAsync(itemId);
                if (Item != null)
                {
                    SelectedExamen = item.Examen;
                    if (!string.IsNullOrEmpty(item.Serie))
                    {
                        SelectedSerie = item.Serie;
                        showSerie = true;
                    }
                    SelectedNature = item.NatureCandidat;
                    Nom = item.Nom;
                    Prenom = item.Prenom;
                    SelectedGenre = item.Sexe;
                    DateNaissance = item.DateNaissance;
                    LieuNaissance = item.LieuNaissance;
                    PaysNaissance = item.PaysNaissance;
                    Image = item.Image;
                    ShowSectionImage = true;
                    ImageVisible = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}

