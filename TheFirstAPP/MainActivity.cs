using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TheFirstAPP
{
    [Activity(Label = "TheFirstAPP", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var mainImageView = FindViewById<ImageView>(Resource.Id.mainImageView);
            var imageUrl = "https://4.bp.blogspot.com/-cy-TYW9QiWY/WLWLq_Wa5jI/AAAAAAAA7Cw/0Dy3CB2YHiYrrbFd7mbGKrxyKUXrBWaWwCLcB/s1600/17015984_10210909103558048_1701727630736098401_o.jpg";
            var imageBitmap = GetImageBitmapFromUrl(imageUrl);
            mainImageView.SetImageBitmap(imageBitmap);

             
            Button button = FindViewById<Button>(Resource.Id.myButton);
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString("http://dvd.kachinlina.com/home/GenerateRandomImage/1");
                var vImgLinks = JsonConvert.DeserializeObject<List<ImgLink>>(response);
                var imageUrl = "https://www.missviet.com.vn/wp-content/uploads/2016/11/7-4.jpg";
                if(vImgLinks != null && vImgLinks.Count > 0)
                {
                    imageUrl = vImgLinks[0].Name;
                }
                var mainImageView = FindViewById<ImageView>(Resource.Id.mainImageView);
                var imageBitmap = GetImageBitmapFromUrl(imageUrl);
                mainImageView.SetImageBitmap(imageBitmap);
            }            
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return imageBitmap;
        }
    }

    public class ImgLink
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

