namespace api.DTO
{
    public class UpdateRequestDTO : LoginRequestDTO
    {
        public string Balance { get; set; }

        public override bool RequestValidation()
        {
            if (base.RequestValidation() == false || Balance == null)
                return false;
            return true;
        }
    }
}
