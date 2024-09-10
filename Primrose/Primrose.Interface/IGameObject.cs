using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primrose.Interface
{
    /// <summary>
    /// Combines both IRender and IUpdate into one interface.
    /// </summary>
    public interface IGameObject : IRender, IUpdate
    {}
}
