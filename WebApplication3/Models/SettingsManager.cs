using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace WebApplication3.Models
{
    public interface ISettingsManager
    {
        Settings Current { get; }
        void Update(Action<Settings> change);
    }

    class SettingsManager : ISettingsManager
    {
        readonly object syncLock = new object();
        readonly SettingsConfigurationProvider _configurationProvider;
        readonly IOptionsMonitor<Settings> _optionsMonitor;

        public SettingsManager(SettingsConfigurationProvider configurationProvider, IOptionsMonitor<Settings> optionsMonitor)
        {
            _configurationProvider = configurationProvider;
            _optionsMonitor = optionsMonitor;
        }

        public Settings Current => _optionsMonitor.CurrentValue;

        public void Update(Action<Settings> configure)
        {
            lock (syncLock)
            {
                var newOptions = new Settings(Current);
                configure(newOptions);

                var configSource = _configurationProvider.Source;
                var fileName = Path.Combine(((PhysicalFileProvider)configSource.FileProvider).Root, configSource.Path);
                var tempFileName = fileName + ".new";
                File.WriteAllText(tempFileName, JsonConvert.SerializeObject(newOptions));
                if (File.Exists(fileName))
                    File.Delete(fileName);
                File.Move(tempFileName, fileName);

                _configurationProvider.Reload();
            }
        }
    }
}