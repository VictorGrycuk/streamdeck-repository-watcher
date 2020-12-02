using BarRaider.SdTools;
using RepositoryWatcher.Models;
using Newtonsoft.Json.Linq;
using Octokit;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace RepositoryWatcher
{
    [PluginActionId("com.victorgrycuk.repositorywatcher")]
    public class RepositoryWatcher : PluginBase
    {
        private readonly PluginSettings settings;
        private readonly Timer Timer;
        private DateTime dateTime = DateTime.Now;

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
            var test = WatcherFactory.GetWatcher(settings);
            var image = test.GetImage(dateTime);
            Connection.SetImageAsync(image);
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void KeyPressed(KeyPayload payload)
        {
            try
            {
                // It is necessary to "reset" the counter from when new items are counted
                dateTime = DateTime.Now;
                UpdateKey(null, null);

                // We open the corresponding url
                var url = settings.ResourceType == ResourceType.ISSUE
                    ? settings.RepositoryURL + "/issues"
                    : settings.RepositoryURL + "/pulls";
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, ex.Message);
                Connection.ShowAlert().Wait();
                throw;
            }
        }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            try
            {
                Tools.AutoPopulateSettings(settings, payload.Settings);
                UpdateSettingsEnum();
                Timer.Interval = settings.Interval * 60000;
                dateTime = dateTime.Subtract(new TimeSpan(settings.InitialOffset, 0, 0, 0));

                if (settings.IsEnabled) Timer.Start();
                else Timer.Stop();

                SaveSettings();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, ex.Message);
                Connection.ShowAlert().Wait();
                throw;
            }
        }

        private Task SaveSettings()
        {
            return Connection.SetSettingsAsync(JObject.FromObject(settings));
        }

        private void UpdateSettingsEnum()
        {
            settings.State = ParseEnum<ItemStateFilter>(settings.SelectedState);
            settings.FilterBy = ParseEnum<IssueFilter>(settings.SelectedFilterBy);
            settings.ResourceType = ParseEnum<ResourceType>(settings.SelectedResourceType);
            settings.PullRequestSortBy = ParseEnum<PullRequestSort>(settings.SelectedPullRequestSortBy);
        }

        private TEnum ParseEnum<TEnum>(string text) where TEnum : struct, IConvertible
        {
            _ = Enum.TryParse(text, true, out TEnum result);

            return result;
        }
        
        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick() { }
    }
}
