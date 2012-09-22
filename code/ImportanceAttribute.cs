using System;

namespace SortedArray.code
{
    public class ImportanceAttribute : Attribute
    {
        public ImportanceAttribute(int importance = 3)
        {
            Importance = importance;
        }

        public int Importance { get; set; }

        public static int Default
        {
            get { return 3; }
        }
    }
}