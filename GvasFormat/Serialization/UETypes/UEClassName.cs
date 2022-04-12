using GvasFormat.Serialization.Exceptions;

namespace GvasFormat.Serialization.UETypes
{
    public class UEClassName
    {
        //is last part like .ClassName_C?
        public readonly bool ClassInstance;
        public readonly string Name;
        public readonly string Path;
        public readonly string FullName;

        public UEClassName(string fullName)
        {
            FullName = fullName;
            int index = fullName.LastIndexOf('/');
            bool ClassInstance = fullName.EndsWith("_C");
            int dotIndex = fullName.LastIndexOf('.');
            if (ClassInstance)
            {
                //double check
                string leftName = fullName.Substring(index + 1, dotIndex - index - 1);
                string rightName = fullName.Substring(dotIndex + 1, fullName.Length - dotIndex - 2 - 1);
                if (leftName.Equals(rightName))
                {
                    Name = leftName;
                }
                else
                {
                    throw new UnexpectedClassName();
                }
            }
            else
            {
                Name = fullName.Substring(dotIndex + 1, fullName.Length - dotIndex - 1);
            }


            Path = fullName.Substring(0, index + 1);
            FullName = fullName;
        }
    }
}