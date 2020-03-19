using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace WebApplication3.Models
{
    public class SettingsConfigurationProvider : JsonConfigurationProvider
    {
        public SettingsConfigurationProvider(JsonConfigurationSource source) : base(source)
        {
        }

        public void Reload()
        {
            Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            var file = Source.FileProvider?.GetFileInfo(Source.Path);
            if (file != null && file.Exists)
                using (var stream = file.CreateReadStream())
                    Load(stream);

            OnReload();
        }
    }
    
    class SettingsConfigurationSource : JsonConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            Optional = true;
            ReloadOnChange = false;
            EnsureDefaults(builder);
            return new SettingsConfigurationProvider(this);
        }
    }
}