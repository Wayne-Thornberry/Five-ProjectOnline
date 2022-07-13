﻿using CitizenFX.Core;
using CitizenFX.Core.UI;
using Newtonsoft.Json;
using Proline.ClassicOnline.MGame;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Proline.CFXExtended.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MScripting;

namespace Proline.ClassicOnline.SClassic
{
    public class PassiveSaving
    {

        public async Task Execute(object[] args, CancellationToken token)
        {
            var nextSaveTime = DateTime.UtcNow.AddMinutes(1);
            while (!token.IsCancellationRequested)
            {
                if (DateTime.UtcNow > nextSaveTime)
                {
                    MScriptingAPI.StartNewScript("SaveNow");
                    nextSaveTime = DateTime.UtcNow.AddMinutes(1);
                }
                await BaseScript.Delay(0);
            }

        }
    }
}
