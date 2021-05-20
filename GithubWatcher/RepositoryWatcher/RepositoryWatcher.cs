using BarRaider.SdTools;
using Newtonsoft.Json.Linq;
using RepositoryWatcher.Models;
using RepositoryWatcher.Properties;
using System;
using System.Drawing;
using System.Timers;

namespace RepositoryWatcher
{
    [PluginActionId("com.victorgrycuk.repositorywatcher")]
    public class RepositoryWatcher : PluginBase
    {
        private readonly PluginSettings settings;
        private readonly Timer Timer;
        private DateTime dateTime;
        private IWatcher watcher;

        public RepositoryWatcher(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            settings = payload.Settings == null || payload.Settings.Count == 0
                ? PluginSettings.CreateDefaultSettings()
                : payload.Settings.ToObject<PluginSettings>();

            Timer = new Timer();
            Timer.AutoReset = true;
            Timer.Elapsed += new ElapsedEventHandler(UpdateKey);
        }

        private void UpdateKey(object sender, ElapsedEventArgs e)
        {
            try
            {
                var processing = (Image)Resources.ResourceManager.GetObject("processing");
                Connection.SetImageAsync(processing).Wait();
                var image = watcher.GetImage();
                Connection.SetImageAsync(image);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, ex.Message);
                Connection.ShowAlert().Wait();
            }
        }

        public override void Dispose()
        {
            Timer.Dispose();
        }

        public override void KeyPressed(KeyPayload payload)
        {
            dateTime = DateTime.Now;
        }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            try
            {
                Tools.AutoPopulateSettings(settings, payload.Settings);
                settings.UpdateSettingsEnum();
                watcher = WatcherFactory.GetWatcher(settings);
                
                UpdateTimer();
                SaveSettings();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, ex.Message);
                Connection.ShowAlert().Wait();
            }
        }

        private void UpdateTimer()
        {
            if (settings.Interval < 1)
                throw new ArgumentException("The interval configuration cannot be lower than 1");

            Timer.Interval = settings.Interval * 60000;
            if (settings.IsEnabled) Timer.Start();
            else Timer.Stop();
        }

        private void SaveSettings() => Connection.SetSettingsAsync(JObject.FromObject(settings));
        
        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }

        public override void KeyReleased(KeyPayload payload) 
        {
            try
            {
                if ((DateTime.Now - dateTime).TotalSeconds > 2)
                {
                    // We open the corresponding url
                    System.Diagnostics.Process.Start(watcher.GetUrl());
                }

                UpdateKey(null, null);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, ex.Message);
                Connection.ShowAlert().Wait();
            }
        }

        public override void OnTick() { }
    }
}
