namespace Logiwa.Domain.Responses
{
    public class ServiceResult<T>
    {
        public ServiceResult()
        {
            Errors = new List<string>();
        }

        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public bool Success 
        {
            get
            {
                return Errors?.Count <= 0;
            }
        }

        public void AddError(string error) 
        {
            Errors.Add(error);
        }
    }
}
