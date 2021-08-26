﻿

using Proline.Engine.Data;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class ExtensionManager
    {
        private static ExtensionManager _instance; 
        private List<EngineExtension> _extensions;

        ExtensionManager()
        { 
            _extensions = new List<EngineExtension>();
        }

        internal static ExtensionManager GetInstance()
        {
            if (_instance == null)
                _instance = new ExtensionManager();
            return _instance;
        }


        internal void InitializeExtension(ExtensionDetails extensionPath)
        {
            var assembly = Assembly.Load(extensionPath.Assembly);
            foreach (var item in extensionPath.ExtensionClasses)
            {
                var types = assembly.GetType(item);
                var extension = (EngineExtension)Activator.CreateInstance(types, null);
                extension.OnInitialize();
                _extensions.Add(extension);
            }
        }

        internal IEnumerable<EngineExtension> GetExtensions()
        {
            return _extensions;
        }
    }
}