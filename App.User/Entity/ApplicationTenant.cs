using App.Base.Entities;

namespace App.User.Entity;

public class ApplicationTenant : FullAuditedEntity<Guid>
{
    public string Name { get; protected set; }
    public string DatabaseName { get; protected set; }

    public ApplicationTenant()
    {
    }

    public ApplicationTenant(string name, string databaseName)
        => Copy(name, databaseName);
    private void Copy(string name, string databaseName)
    {
        Name = name;
        DatabaseName = databaseName;
    }
}