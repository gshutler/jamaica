using Jamaica.Security;

namespace Jamaica.Repositories
{
    public interface ISecurityPrincipalRepository
    {
        /// <summary>
        /// Retrieves a security principal by their name and hash.
        /// </summary>
        /// <returns>
        /// Matching security principal or null.
        /// </returns>
        ISecurityPrincipal GetByNameAndHash(string name, string hash);
    }
}