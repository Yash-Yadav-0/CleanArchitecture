﻿using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities
{
    public class Details : EntityBase
    {
        public Details()
        {

        }
        public Details(string title, string description, int categoryId)
        {
            Title = title;
            Description = description;
            CategoryId = categoryId;
        }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
