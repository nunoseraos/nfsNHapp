using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnNotif.Model
{
    public  class UserData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string HubTag { get; set; }
        [MaxLength(300)]
        public string HubName { get; set; }

        [MaxLength(300)] 
        public string HubConnString { get; set; }
    }
}