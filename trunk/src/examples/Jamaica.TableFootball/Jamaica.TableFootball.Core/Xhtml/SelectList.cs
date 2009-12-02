using System;
using System.Collections.Generic;

namespace Jamaica.TableFootball.Core.Xhtml
{
    public class SelectList
    {
        public SelectList(IEnumerable<SelectListItem> selectListItems)
        {
            Items = selectListItems;
        }

        public SelectList() : this(new List<SelectListItem>())
        {
        }

        public IEnumerable<SelectListItem> Items { get; private set;}
    }
}