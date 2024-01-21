using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomemadeServer
{
    internal interface IUserMetadataProvider
    {
        public byte[] GetHashedPassword(string userName);
        public byte[] GetPasswordSalt(string userName);
        public byte[] GetPublicKey(string userName);
        public byte[] GetEncryptedPrivateKey(string userName);
    }
}
