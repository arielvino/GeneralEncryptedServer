using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomemadeCloudUtils.Commands
{
    internal static class AcountAction
    {
        public const byte Title = 0;
        public const byte CreateAcount = 1;
        public const byte DeleteAcount = 2;
        public const byte LoginWithPassword = 3;
        public const byte LoginWithToken = 4;
        public const byte Logout = 5;
        public const byte ChangePassword = 6;
        public const byte SetToken = 7;

        public static class Keys
        {
            public const byte UserName = 0;
        }
        

    }
}
