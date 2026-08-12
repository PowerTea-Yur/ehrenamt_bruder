namespace Domain.Organizations;

public class Organization
{
    public Guid Id { get; }
    public string Name { get; }

    public Organization(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        Id = id;
        Name = name;
    }
}
