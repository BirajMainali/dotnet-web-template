using App.Base.Entities;

namespace App.User.Entity;

public class AppUser : FullAuditedEntity<long>
{
    public ApplicationTenant? Tenant { get; protected set; }
    public long? TenantId { get; protected set; }
    public string Name { get; protected set; }
    public string Gender { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public string Address { get; protected set; }
    public string Phone { get; protected set; }

    public AppUser? ParentUser { get; protected set; }
    public long? ParentUserId { get; protected set; }

    public AppUser SetTenant(ApplicationTenant tenant)
    {
        Tenant = tenant;
        return this;
    }

    public AppUser()
    {
    }

    public AppUser(string name, string gender, string email, string password, string address, string phone)
        => Copy(name, gender, email, password, address, phone);

    public void Update(string name, string gender, string email, string password, string address, string phone)
        => Copy(name, gender, email, password, address, phone);

    private void Copy(string name, string gender, string email, string password, string address, string phone)
    {
        Name = name;
        Gender = gender;
        Email = email;
        Password = password;
        Address = address;
        Phone = phone;
    }
}