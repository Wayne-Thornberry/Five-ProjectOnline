﻿using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MWorld;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MWorld.Commands
{
    public class PopulateSixGarageRandomCommand : ModuleCommand
    {
        public PopulateSixGarageRandomCommand() : base("PopulateSixGarageRandom")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            DoStuff();
        }

        private static async Task DoStuff()
        {
            for (int i = 0; i < 6; i++)
            {
                //Array values = Enum.GetValues(typeof(VehicleHash));
                //Random random = new Random();
                //VehicleHash randomBar = (VehicleHash)values.GetValue(random.Next(values.Length));
                var vehicle = await World.CreateVehicle(new Model(VehicleHash.Buffalo3), Game.PlayerPed.Position);
                WorldAPI.PlaceVehicleInGarageSlot("6CarGarage", i, vehicle);
            }
        }
    }
}