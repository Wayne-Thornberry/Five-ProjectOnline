﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MWord;
using Proline.Resource.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MWorld
{
    public static partial class WorldAPI
    {
        public static string GetNearestInterior()
        {
            try
            {
                var resourceData = ResourceFile.Load($"data/world/interiors.json");
                var worldBuildings = JsonConvert.DeserializeObject<string[]>(resourceData.Load());
                var distance = 99999f;
                var entranceString = "";
                var playPos = new Vector2(Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y);
                foreach (var item in worldBuildings)
                {
                    var resourceData2 = ResourceFile.Load($"data/world/interiors/{item}.json");
                    var buildingMetaData = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData2.Load());
                    var d = API.GetDistanceBetweenCoords(buildingMetaData.WorldPos.X,
                        buildingMetaData.WorldPos.Y, 0, playPos.X, playPos.Y, 0, false);
                    if (d < distance)
                    {
                        distance = d;
                        entranceString = buildingMetaData.Id;
                    }
                }
                return entranceString;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return null;
        }

        internal static Vector3 GetBuildingInterior(string buildingId)
        {
            try
            {
                var resourceData2 = ResourceFile.Load($"data/world/interiors/{buildingId}.json");
                var buildingMetaData = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData2.Load());
                return new Vector3(buildingMetaData.WorldPos.X, buildingMetaData.WorldPos.Y, 0f);
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return Vector3.One;
        }
        public static string GetNearestInteriorExit()
        {
            try
            {
                var building = GetNearestInterior();
                var resourceData2 = ResourceFile.Load($"data/world/interiors/{building}.json");
                var interiorMetadata = JsonConvert.DeserializeObject<InteriorMetadata>(resourceData2.Load());
                var distance = 99999f;
                var entranceString = "";
                foreach (var item in interiorMetadata.Exits)
                {
                    var newDistance = World.GetDistance(item.DoorPosition, Game.PlayerPed.Position);
                    if (World.GetDistance(item.DoorPosition, Game.PlayerPed.Position) < distance)
                    {
                        distance = newDistance;
                        entranceString = item.Id;
                    }
                }
                return entranceString;
            }
            catch (Exception e)
            {
                MDebugAPI.LogError(e);
            }
            return null;
        }

    }
}
