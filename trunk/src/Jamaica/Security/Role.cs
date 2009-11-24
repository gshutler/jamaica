namespace Jamaica.Security
{
    public class Role : ISecurityRole
    {
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