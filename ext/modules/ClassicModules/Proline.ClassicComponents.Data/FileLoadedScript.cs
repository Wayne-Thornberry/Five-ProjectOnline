﻿using Proline.ClassicOnline.MData.Entity;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

#if SERVER
using Proline.DBAccess.Proxies;
#endif

namespace Proline.ClassicOnline.MData.Scripts
{
    public class FileLoadedScript : ModuleScript
    {
        public FileLoadedScript(Assembly source) : base(source)
        {
#if CLIENT
            EventHandlers.Add("FileLoadedHandler", new Action<string>(OnFileLoaded));
            EventHandlers.Add("SaveFileResponseHandler", new Action<int>(OnSaveFileResponse));
#elif SERVER
            EventHandlers.Add("SaveFileHandler", new Action<Player, string>(OnFileSave));
            EventHandlers.Add("LoadFileHandler", new Action<Player, long>(OnFileLoad));
#endif
        }

#if SERVER
        private void OnFileSave([FromSource] Player arg1, string arg2)
        {
            SaveFileAsync(arg1, arg2);
        }

          private static async Task SaveFileAsync(Player player, string arg2)
        {
            try
            {
                Debug.WriteLine(arg2);
                using (var x = new DBAccessClient())
                {
                    var response = await x.RegisterPlayer(new RegisterPlayerRequest() { Name = player.Name });
                    long id = response.Id;
                    Debug.WriteLine(String.Format("Registered user returned {0}, id {1}", response.ReturnCode, response.Id));
                    if (response.ReturnCode == 1)
                    {
                        var getPlayerResponse = await x.GetPlayer(new GetPlayerRequest() { Username = player.Name});
                        Debug.WriteLine(String.Format("Getting Player {0}, id {1}", getPlayerResponse.ReturnCode, getPlayerResponse.PlayerId));
                        id = getPlayerResponse.PlayerId;
                    } 
                    var response2 = await x.SaveFile(new InsertSaveRequest() { PlayerId = id, Data = arg2 });
                    if (response2.ReturnCode == 0)
                        player.TriggerEvent("SaveFileResponseHandler", 0);
                    else
                        player.TriggerEvent("SaveFileResponseHandler", 1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
        private void OnFileLoad([FromSource] Player arg1, long arg2)
        {
            LoadFileAsync(arg1, arg2);
        }

        private static async Task LoadFileAsync(Player arg1, long arg2)
        {
            try
            {
                var data = "";
                using (var x = new DBAccessClient())
                {
                    data = (await x.LoadFile(new GetSaveRequest() { Id = arg2 })).Data;
                }
                arg1.TriggerEvent("FileLoadedHandler", data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            };
        }
#elif CLIENT

        //public FileLoadedScript()
        //{
        //    EventHandlers.Add("FileLoadedHandler", new Action<string>(OnFileLoaded)); 
        //    EventHandlers.Add("SaveFileResponseHandler", new Action<int>(OnSaveFileResponse)); 
        //}

        private void OnSaveFileResponse(int obj)
        {
            var fm = FileManager.GetInstance();
            fm.IsSaveInProgress = false;
            fm.LastSaveResult = obj;
        }

        private void OnFileLoaded(string obj)
        {
            var instance = FileManager.GetInstance();
            instance.PutFileIntoMemory(obj);
        }
#endif
    }
}