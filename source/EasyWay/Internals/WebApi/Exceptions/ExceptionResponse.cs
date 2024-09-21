namespace EasyWay.Internals.WebApi.Exceptions
{
    internal abstract class ExceptionResponse
    {
        public string? Title { get; protected set; }

        public string? Type { get; protected set; }

        public int Status { get; protected set; }

        public string? Detail { get; protected set; }

        public IEnumerable<string>? Errors { get; protected set; }

        protected ExceptionResponse() { }
    }
}
