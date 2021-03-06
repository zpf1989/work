﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Base
{
    public class AddDictViewModel
    {
        [Required(ErrorMessageResourceName = "Code_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [MaxLength(40, ErrorMessageResourceName = "Dict_Code_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string dict_code { get; set; }

        [Required(ErrorMessageResourceName = "Name_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        [MaxLength(20, ErrorMessageResourceName = "Dict_Name_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string dict_name { get; set; }

        [Required(ErrorMessageResourceName = "Dict_Type_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string dict_type { get; set; }
        public bool dict_system { get; set; }
        public bool dict_default { get; set; }
        public int dict_order { get; set; }
    }
}