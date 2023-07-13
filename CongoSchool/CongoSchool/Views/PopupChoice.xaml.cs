using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;

namespace CongoSchool.Views
{	
	public partial class PopupChoice : Popup
	{	
		public PopupChoice ()
		{
			InitializeComponent ();
		}

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
			Dismiss(true);
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            
        }

        async void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            
        }
    }
}

