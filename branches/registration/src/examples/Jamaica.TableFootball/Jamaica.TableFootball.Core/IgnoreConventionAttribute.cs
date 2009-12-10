using System;

namespace Jamaica.TableFootball.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class IgnoreConventionAttribute : Attribute
    {
    }
}