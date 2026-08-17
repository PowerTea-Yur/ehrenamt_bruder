namespace Domain.Volunteers;

public class Volunteer
{
    public Guid Id { get; }
    public string Name { get; }

    public Volunteer(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        Id = id;
        Name = name;
    }

    private Volunteer()
    {
        Name = null!;
    }
}
