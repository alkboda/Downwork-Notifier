using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Downwork_Notifier.Common.Converters
{
    public class GenericConverter<TIn, TOut> : MarkupExtension, IValueConverter
    {
        private Func<TIn, TOut> _convert = null;
        private Func<TOut, TIn> _convertBack = null;

        public GenericConverter(Func<TIn, TOut> convert = null, Func<TOut, TIn> convertBack = null)
        {
            _convert = convert;
            _convertBack = convertBack;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_convert == null)
            {
                throw new NotImplementedException();
            }

            return _convert((TIn)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_convertBack == null)
            {
                throw new NotImplementedException();
            }

            return _convertBack((TOut)value);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // here!
            throw new NotImplementedException();
        }
    }
}
