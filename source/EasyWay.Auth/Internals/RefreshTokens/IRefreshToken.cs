using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWay.Internals.RefreshTokens
{
    internal interface IRefreshToken
    {
        string CreateRefreshToken();
    }
}
