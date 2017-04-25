using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Downwork_Notifier.Common.Converters
{
    // todo: add processing w/o name, just by types of data within binding
    public class GetConverterExtension : MarkupExtension
    {
        private string _converterName = string.Empty;

        public GetConverterExtension(String converterName)
        {
            if (String.IsNullOrWhiteSpace(converterName))
            {
                throw new ArgumentNullException(nameof(converterName));
            }

            _converterName = converterName;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return BindingHelper.Instance.Converters[_converterName];
        }
    }
}
