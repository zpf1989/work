﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Misc
{
    public interface ICopyable
    {
        void CopyTo(object target);
    }
}
