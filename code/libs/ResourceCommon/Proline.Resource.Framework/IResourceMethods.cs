﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public interface IResourceMethods
    {
        string LoadResourceFile(string resourceName, string fileName);
        string GetCurrentResourceName();
    }
}