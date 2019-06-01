using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventOrganizer.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
