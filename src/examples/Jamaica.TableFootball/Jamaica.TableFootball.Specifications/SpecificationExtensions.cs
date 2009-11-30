using OpenRasta.Web;

namespace Jamaica.TableFootball.Specifications
{
    public static class SpecificationExtensions
    {
        public static T Response<T>(this OperationResult operationResult) where T : class
        {
            return (T) operationResult.ResponseResource;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}