using OR.Throwaway.DataAccess;

namespace OR.Throwaway.Domain
{
    public class Tag : IAutoMapped
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}