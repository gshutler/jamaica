namespace Jamaica.Security
{
    public class Role
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; protected set; }

        protected Role()
        {
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}