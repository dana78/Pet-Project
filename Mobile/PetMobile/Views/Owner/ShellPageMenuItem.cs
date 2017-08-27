using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetMobile.Views.Owner
{

    public class ShellPageMenuItem
    {
        public ShellPageMenuItem()
        {
            TargetType = typeof(ShellPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}