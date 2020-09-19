using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Role : IdentityRole<string>
    {
        public string Description { get; set; }
    }
}
