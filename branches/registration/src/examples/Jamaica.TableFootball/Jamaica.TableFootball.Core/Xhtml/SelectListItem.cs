using System;

namespace Jamaica.TableFootball.Core.Xhtml
{
    public class SelectListItem
    {
        public SelectListItem(string displayAndValue) 
            : this(displayAndValue, displayAndValue)
        {
        }

        public SelectListItem(string display, string value)
        {
            Display = display;
            Value = value;
        }

        public string Display { get; private set; }
        public string Value { get; private set; }
    }
}