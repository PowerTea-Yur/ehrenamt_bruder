namespace Domain.Events;

public class ShiftRole
{
    public Guid Id { get; }
    public string Name { get; }
    public bool RequiresApproval { get; }

    public ShiftRole(Guid id, string name, bool requiresApproval)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        Id = id;
        Name = name;
        RequiresApproval = requiresApproval;
    }

    private ShiftRole()
    {
        Name = null!;
    }
}
