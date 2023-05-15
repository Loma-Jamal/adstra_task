using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adstra_task
{
    public class VMLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class VMRegister
    {
        public string UserName { get; set; }
        public string Passwprd { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string FullName { get; set; }
    }
}
