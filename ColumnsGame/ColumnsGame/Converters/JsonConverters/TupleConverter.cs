using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ColumnsGame.Converters.JsonConverters
{
    public class TupleConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var elements = Convert.ToString(value).Replace(" ", string.Empty).Trim('(', ')')
                .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            return (int.Parse(elements.First()), int.Parse(elements.Last()));
        }
    }
}