using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Adstra_task
{
    public class VMLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class VMProfile
    {

        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Name { get; set; }

    }

    public class VMRegister
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
