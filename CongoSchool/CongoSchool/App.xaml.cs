using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CongoSchool.Services;
using CongoSchool.Views;
using System.IO;

namespace CongoSchool
{
    public partial class App : Application
    {
        static MockDataStore dataStore;

        public static MockDataStore MockDataStore
        {
            get
            {
                if (dataStore == null)
                {
                    dataStore = new MockDataStore(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CongoSchool.db3"));
                }
                return dataStore;
            }
        }

        public App ()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

