using System.Collections.Generic;

namespace _0_Framework.Infrastructure
{
    public interface IPermissionExpose
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }
}

