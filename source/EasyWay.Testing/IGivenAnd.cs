using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWay
{
    public interface IGivenAnd
    {
        IGivenAnd And();

        IWhen When();
    }
}
