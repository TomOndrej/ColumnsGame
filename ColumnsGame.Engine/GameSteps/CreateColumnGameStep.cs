using System.Threading.Tasks;
using ColumnsGame.Engine.Columns;
using ColumnsGame.Engine.Drivers;
using ColumnsGame.Engine.Interfaces;
using ColumnsGame.Engine.Ioc;

namespace ColumnsGame.Engine.GameSteps
{
    internal class CreateColumnGameStep : GameStepBase
    {
        public CreateColumnGameStep(IGameSettings gameSettings) : base(gameSettings)
        { }

        protected override Task ProcessStep()
        {
            var newColumn = ContainerProvider.Resolve<IColumnFactory>().CreateColumn(this.GameSettings);
            ContainerProvider.Resolve<IColumnDriver>().Drive(newColumn);

            return Task.CompletedTask;
        } 
    }
}
