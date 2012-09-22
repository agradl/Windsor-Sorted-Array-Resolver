using System;

namespace SortedArray.code
{
    public static class Utility
    {
        //sorted array resolver sorts ascending
        //so we invert the result to get the desired effect
        public static int SortFunction(object x, object y)
        {
            return -1*(x.GetImportance() - y.GetImportance());
        }

        private static int GetImportance(this object obj)
        {
            var attr = (ImportanceAttribute) Attribute.GetCustomAttribute(obj.GetType(), typeof (ImportanceAttribute));
            return (attr == null) ? ImportanceAttribute.Default : attr.Importance;
        }
    }
}