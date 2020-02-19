using System.Collections.Generic;

namespace ProjetDevMob.Models
{
    class ImageResponse
    {
        public Dataimg data { get; set; }
        public bool Is_success { get; set; }
        public string Error_code { get; set; }
        public string Error_message { get; set; }

    }

    class Dataimg
    {
        public int id { get; set; }
    }
}
