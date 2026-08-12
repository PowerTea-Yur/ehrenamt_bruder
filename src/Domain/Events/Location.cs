namespace Domain.Events;

public record Location
{
    public string Street { get; }

    public Location(string street)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be empty.", nameof(street));

        Street = street;
    }
}
