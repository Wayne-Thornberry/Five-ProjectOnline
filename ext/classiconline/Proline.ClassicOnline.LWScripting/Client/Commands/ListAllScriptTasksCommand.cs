﻿using Proline.ClassicOnline.MScripting.Internal;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MScripting.Client.Commands
{
    public class ListAllScriptTasksCommand : ModuleCommand
    {
        public ListAllScriptTasksCommand() : base("ListAllScriptTasks")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var sm = ScriptTaskManager.GetInstance(); 
            foreach (var scriptTask in sm.Values)
            {
                Console.WriteLine(String.Format("Task Id {0}, Is Complete {1}, Status {2} ", scriptTask.Id, scriptTask.IsCompleted, scriptTask.Status));
            }
        } 
    }
}
