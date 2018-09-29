using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MediaView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        static IRandomAccessStreamWithContentType stream = null;
        static List<StorageFile> files = new List<StorageFile>();
        static StorageFile file = null;
        static Random random = new Random();
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileOpenPicker picker = new FileOpenPicker();
                picker.SuggestedStartLocation = PickerLocationId.Desktop;
                picker.FileTypeFilter.Add(".mp3");
                picker.FileTypeFilter.Add(".mp4");
                picker.FileTypeFilter.Add(".avi");
                var list = await picker.PickMultipleFilesAsync();

                foreach (StorageFile item in list)
                {
                    files.Add(item);

                }
                lstFile.ItemsSource = files;
            }
            catch (Exception k)
            {

            }

        }

        private async void lstFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var file = lstFile.SelectedItem as StorageFile;
                if (file != null)
                {
                    var stream = await file.OpenReadAsync();
                    mdElement.SetSource(stream, file.ContentType);
                    mdElement.Play();
                }

            }
            catch (Exception k)
            {

            }
        }



        private async void next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = files.IndexOf(file) + 1;
                if (index >= files.Count) index = 0;
                file = files.ElementAt(index);
                if (file != null)
                {
                    stream = await file.OpenReadAsync();
                    mdElement.SetSource(stream, file.ContentType);
                    mdElement.Play();
                }

            }
            catch (Exception k)
            {

            }

        }

        private async void previous_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = files.IndexOf(file) - 1;
                if (index < 0) index = files.Count - 1;
                file = files.ElementAt(index);
                if (file != null)
                {
                    stream = await file.OpenReadAsync();
                    mdElement.SetSource(stream, file.ContentType);
                    mdElement.Play();
                }
            }
            catch (Exception k)
            {

            }
        }

        private async void first_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                file = files.ElementAt(0);
                if (file != null)
                {
                    stream = await file.OpenReadAsync();
                    mdElement.SetSource(stream, file.ContentType);
                    mdElement.Play();
                }
            }
            catch (Exception k)
            {

            }
        }

        private async void last_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                file = files.ElementAt(files.Count - 1);
                if (file != null)
                {
                    stream = await file.OpenReadAsync();
                    mdElement.SetSource(stream, file.ContentType);
                    mdElement.Play();
                }
            }
            catch (Exception k)
            {

            }
        }

        private async void Repeat_Click(object sender, RoutedEventArgs e)
        {
            if (file != null)
            {
                stream = await file.OpenReadAsync();
                mdElement.SetSource(stream, file.ContentType);
                mdElement.Play();
            }
        }

        private async void Random_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int min = 0;
                int max = files.Count;
                int index = random.Next(min, max);
                file = files.ElementAt(index);
                if (file != null)
                {
                    stream = await file.OpenReadAsync();
                    mdElement.SetSource(stream, file.ContentType);
                    mdElement.Play();
                }
            }
            catch (Exception a)
            {

            }
        }
    }
}
