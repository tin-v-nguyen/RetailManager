using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMWindowsUI.Models
{
    public class UserDisplayModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();
        public string RoleDisplayList
        {
            get 
            {
                return string.Join(", ", Roles.Select(x => x.Value)); 
            }
        }
    }
}
