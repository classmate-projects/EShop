using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopNative.MetaData
{
    public class AppUserProfile
    {
        public string KeycloakUserId { get; set; }
        public string DisplayName { get; set; }
        public bool IsShopOwner { get; set; }
    }
}
