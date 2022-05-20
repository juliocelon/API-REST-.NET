namespace api.DTO
{
    public class DeleteRequestDTO : LoginRequestDTO
    {
        public string User { get; set; }

        public override bool RequestValidation()
        {
            if (base.RequestValidation() == false || User == null)
                return false;
            return true;
        }
    }
}
