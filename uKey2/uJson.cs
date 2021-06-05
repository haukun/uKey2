using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
namespace uKey2
{
    public class uKeyJson
    {
        public Setting[] Settings { get; set; }
        public class Setting
        {
            public Boolean shift { get; set; }
            public Boolean control { get; set; }
            public Boolean alt { get; set; }
            public Boolean win { get; set; }
            public string key { get; set; }
            public uint message { get; set; }
            public uint wParam { get; set; }
            public uint lParam { get; set; }
        }

    }

    class uJson
    {
        public uKeyJson json { get; set; }

        //  設定Jsonの読み込み
        public Boolean Retrieve()
        {
            Boolean result = false;
            String text = File.ReadAllText("Setting\\setting.json");
            json = JsonSerializer.Deserialize<uKeyJson>(text);

            result = true;
            return result;
        }


    }
}
