using System;
using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Providers
{
    internal class SettingsProvider : ISettingsProvider
    {
        private WeakReference<IGameSettings> settingsInstance;

        public IGameSettings GetSettingsInstance()
        {
            this.settingsInstance.TryGetTarget(out var settings);

            return settings;
        }

        public void SetSettingsInstance(IGameSettings settings)
        {
            if (this.settingsInstance == null)
            {
                this.settingsInstance = new WeakReference<IGameSettings>(settings);
            }
            else
            {
                this.settingsInstance.SetTarget(settings);
            }
        }
    }
}