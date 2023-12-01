using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication3
{
    public class GenresFilterViewModel
    {
        public string Name { get; }
        public string Description { get; }

        public GenresFilterViewModel(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
