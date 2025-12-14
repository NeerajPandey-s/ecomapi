using EcomAPI.Common.ServiceInstallers.Attributes.Base;

namespace EcomAPI.Common.ServiceInstallers.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TransientServiceAttribute : Attribute, IServiceAttributeProperties
    {
        public int Ordinance { get; set; }
    }
}