using AutoMapper; 
namespace FORUMAPPLICATION.ServiceLayer
{
    public static class MapperExtensions
    {
        public static void IgnoreUnmappedProperties(TypeMap map,IMappingExpression expr)
        {
            foreach(string propName in map.GetUnmappedPropertyNames())
            {
                if (map.SourceType.GetProperty(propName)!=null)
                {
                    expr.ForMember(propName, opt => opt.Ignore());
                }
                if (map.DestinationType.GetProperty(propName) != null)
                {
                    expr.ForMember(propName, opt => opt.Ignore());
                }
            }
        }
        public static void IgnorUnmapped(this IProfileExpression profile)
        {
            profile.ForAllMaps(IgnoreUnmappedProperties);
        }
    }
}
