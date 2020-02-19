using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMob.Models
{
    class AuthVerif
    {
        public List<Data> Datas { get; set; }
        public bool Is_success { get; set; }
        public string Error_code { get; set; }
        public string Error_message { get; set; }

    }

    class Data
    {
        public string Access_token { get; set; }
        public string Refresh_token { get; set; }
        public int Expires_in { get; set; }
        public string Token_type { get; set; }
    }
}
