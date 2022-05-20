namespace api.DTO
{
    public class LoginRequestDTO
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public virtual bool RequestValidation()
        {
            if (Login == null || Password == null)
                return false;
            return true;
        }
    }
}
