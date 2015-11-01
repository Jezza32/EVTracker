using System.ComponentModel;
using System.Reflection;

namespace EVTracker
{
    public enum Items
    {
        None,
        [Description("Macho Brace")]
        MachoBrace,
        [Description("Power Weight")]
        PowerWeight,
        [Description("Power Bracer")]
        PowerBracer,
        [Description("Power Belt")]
        PowerBelt,
        [Description("Power Lens")]
        PowerLens,
        [Description("Power Band")]
        PowerBand,
        [Description("Power Anklet")]
        PowerAnklet
    }

    public static class ItemsExtensions
    {
        public static string GetName(this Items items)
        {
            var fi = items.GetType().GetField(items.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : items.ToString();
        }
    }
}