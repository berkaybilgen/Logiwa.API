namespace Logiwa.Domain.Models
{
    public class ValidationModel
    {
        public ValidationModel()
        {
            Errors = new List<string>();
        }
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; set; }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}
