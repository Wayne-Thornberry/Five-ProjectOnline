﻿using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.MDebug;

namespace Proline.ClassicOnline.LevelScripts.Object
{
    public class ObBankTruck 
    {
        private Blip _blip;

        public ObBankTruck()
        {
        }

        public async Task Execute(object[] args, CancellationToken token)
        {
            if(args.Length > 0)
            {
                var entityHandle = (int)args[0];
                DebugConsole.LogDebug(entityHandle);
                var entity = Entity.FromHandle(entityHandle);
                _blip = entity.AttachBlip();
                var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                var stat2 = MPStat.GetStat<long>("BANK_BALANCE");
                DebugConsole.LogDebug(stat.GetValue());
                DebugConsole.LogDebug(stat2.GetValue());
                while (true)
                {
                    Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to recive money");
                    if (Game.IsControlJustPressed(0, Control.Context))
                    {
                        stat.SetValue(stat.GetValue() + 1000);
                    }
                    await BaseScript.Delay(0);
                }
            }

            if (_blip != null)
                _blip.Delete();
        }
    }
}