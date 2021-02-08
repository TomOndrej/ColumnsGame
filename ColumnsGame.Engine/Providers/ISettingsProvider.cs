using ColumnsGame.Engine.Interfaces;

namespace ColumnsGame.Engine.Providers
{
    internal interface ISettingsProvider
    {
        IGameSettings GetSettingsInstance();

        void SetSettingsInstance(IGameSettings settings);
    }
}