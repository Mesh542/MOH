namespace MOH.Models
{
    public class Enum
    {
        public enum Status : byte
        {
            Active = 1,
            Inactive = 2,
        }
        public enum UserType : byte
        {
            GeneralUser = 1,
            Admin = 2,
        }
    }
}
