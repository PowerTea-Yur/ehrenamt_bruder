namespace Domain.Volunteers;

public class Volunteer
{
    public Guid Id { get; }
    public string Name { get; }

    public Volunteer(Guid id, string name)
    {
        Id = id;

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        Name = name;
    }
}
