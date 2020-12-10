using BarRaider.SdTools;
using Newtonsoft.Json.Linq;
using RepositoryWatcher.Models;
using System;
using System.Timers;

namespace RepositoryWatcher
{
    [PluginActionId("com.victorgrycuk.repositorywatcher")]
    public class RepositoryWatcher : PluginBase
    {
        private readonly PluginSettings settings;
        private readonly Timer Timer;
        private DateTime initialDateOffset = DateTime.Now;
        private DateTime dateTime;
        private IWatcher watcher;

        public RepositoryWatcher(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            settings = payload.Settings == null || payload.Settings.Count == 0
                ? PluginSettings.CreateDefaultSettings()
                : payload.Settings.ToObject<PluginSettings>();

            watcher = WatcherFactory.GetWatcher(settings);
            Timer = new Timer();
            Timer.AutoReset = true;
            Timer.Elapsed += new ElapsedEventHandler(UpdateKey);
        }

        private void UpdateKey(object sender, ElapsedEventArgs e)
        {
            try
            {
                var image = watcher.GetImage(initialDateOffset);
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
            try
            {
                UpdateKey(null, null);
                dateTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, ex.Message);
                Connection.ShowAlert().Wait();
            }
        }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            try
            {
                Tools.AutoPopulateSettings(settings, payload.Settings);
                settings.UpdateSettingsEnum();
                watcher = WatcherFactory.GetWatcher(settings);
                initialDateOffset = initialDateOffset.Subtract(new TimeSpan(settings.InitialOffset, 0, 0, 0));
                
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
                    // It is necessary to "reset" the counter from when new items are counted
                    initialDateOffset = DateTime.Now;

                    // We open the corresponding url
                    System.Diagnostics.Process.Start(watcher.GetUrl());
                }
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
