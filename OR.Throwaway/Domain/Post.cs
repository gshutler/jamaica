using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Jamaica.Security;
using OR.Throwaway.DataAccess;

namespace OR.Throwaway.Domain
{
    public class Post : IAutoMapped
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Must specify a title")] 
        public virtual string Title { get; set; }

        [Required(ErrorMessage = "Must specify a description")] 
        public virtual string Description { get; set; }

        [Required(ErrorMessage = "Must specify an author")] 
        public virtual User Author { get; set; }

        public virtual IList<Tag> Tags { get; protected set; }

        public Post()
        {
            Tags = new List<Tag>();
        }
    }
}