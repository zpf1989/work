﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.WebApi.Models
{
    public class EditTableViewModel : AddTableViewModel
    {
        [Required(ErrorMessageResourceName = "Edit_By_Id", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string id { get; set; }
    }
}
