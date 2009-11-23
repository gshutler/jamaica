namespace Jamaica.TableFootball.Specifications
{
    public static class SpecificationExtensions
    {
        public static T As<T>(this object obj) where T : class
        {
            return obj as T;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}