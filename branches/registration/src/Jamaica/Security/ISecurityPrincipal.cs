using System.Collections.Generic;

namespace Jamaica.Security
{
    public interface ISecurityPrincipal
    {
        string Name { get; }
        string Hash { get; }
        IEnumerable<ISecurityRole> Roles { get; } 
    }
}