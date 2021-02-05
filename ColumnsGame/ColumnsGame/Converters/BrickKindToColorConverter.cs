using System;
using ColumnsGame.Ioc.Attributes;
using Xamarin.Forms;

namespace ColumnsGame.Converters
{
    [IocRegisterImplementation]
    internal class BrickKindToColorConverter
    {
        internal Color ConvertKind(int kind)
        {
            return kind switch
            {
                0 => Color.Red,
                1 => Color.Blue,
                2 => Color.Green,
                3 => Color.DarkGoldenrod,

                _ => throw new NotImplementedException($"Brick kind {kind} is not implemented.")
            };
        }
    }
}
