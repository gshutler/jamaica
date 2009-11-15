namespace Jamaica.Security
{
    public class Role
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public Role(string name)
        {
            Name = name;
        }
    }
}