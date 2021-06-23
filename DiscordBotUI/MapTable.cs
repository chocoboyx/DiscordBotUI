using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotUI
{
    public class MapTable
    {
        public List<MessageMap> maptable;

        
        public MapTable()
        {
            maptable = new List<MessageMap>();
            maptable.Add(new MessageMap("", ""));
        }
        public void Add(string i, string o)
        {
            maptable.Add(new MessageMap(i, o));
        }

        public string Reply(string ch, string msg)
        {
            foreach (MessageMap map in maptable)
            {
                if (map.channel != "" && map.channel != ch)
                    continue;
                if (msg.Contains(map.index)) 
                    return map.output;
            }
            return null;
        }

    }

    public class MessageMap
    {
        public MessageMap(string i, string o)
        {
            index = i;
            output = o;
            channel = "";
        }
        public string channel
        {
            set; get;
        }
        public string index 
        {
            set;  get; 
        }

        public string output
        {
            set; get;
        }

        
        

    }
}
