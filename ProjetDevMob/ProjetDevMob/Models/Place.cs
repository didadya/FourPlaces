using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjetDevMob.Models
{
    public class Place
    {

        public List<DataPlace> Data { get; set; }
        public bool Is_success { get; set; }
        public string Error_code { get; set; }
        public string Error_message { get; set; }

    }

    public class DataPlace
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int image_id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public DataPlace(string title, string description, int img_id, double latitude, double longitude)
        {
            this.title = title;
            this.description = description;
            this.image_id = img_id;
            this.latitude = latitude;
            this.longitude = longitude;
        }

    }

    public class Photo
    {
        public ImageSource TilteImage { get; set; }

        public Photo(ImageSource titleImage)
        {
            TilteImage = titleImage;
        }
    }

    public class ListviewAggregatedPlace
    {
        //private DataPlace dataPlace;
        //private Task<Photo> task;

        public DataPlace DdataPlace { get; set; }
        public Photo TiltePhoto { get; set; }

        public ListviewAggregatedPlace(DataPlace dataPlace, Photo titlePhoto)
        {
            DdataPlace = dataPlace;
            TiltePhoto = titlePhoto;
        }

        //public ListviewAggregatedPlace(DataPlace dataPlace, Task<Photo> task)
        //{
        //    this.dataPlace = dataPlace;
        //    this.task = task;
        //}
    }

}
