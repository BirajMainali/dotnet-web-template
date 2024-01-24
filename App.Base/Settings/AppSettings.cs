namespace App.Base.Settings
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public bool UseMultiTenancy { get; set; } = false;
        public string DefaultDataProtectionPurpose { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
}