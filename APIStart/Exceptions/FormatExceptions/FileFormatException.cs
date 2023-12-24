namespace APIStart.Exceptions.FormatExceptions
{
    public class FileFormatException : Exception
    {
        public string Property { get;set; }
        public FileFormatException(){}
        public FileFormatException(string property ,string message) : base(message) 
        {
            Property = property;

        }
    }
}
