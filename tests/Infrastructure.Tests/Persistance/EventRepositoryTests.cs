using Domain.Events;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Infrastructure.Tests.Persistance;

public class EventRepositoryTests : IAsyncLifetime
{
  private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest").Build();

  public async Task InitializeAsync()
  {
    await _sqlContainer.StartAsync();

    await using var context = CreateContext();
    await context.Database.MigrateAsync();
  }

  public async Task DisposeAsync()
  {
    await _sqlContainer.DisposeAsync();
  }

  private AppDbContext CreateContext()
  {
    var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseSqlServer(_sqlContainer.GetConnectionString())
      .Options;

    return new AppDbContext(options);
  }

  [Fact]
  public async Task AddAsync_WithValidEvent_PerstistsEventToDatabase()
  {
    await using var writeContext = CreateContext();
    var sut = new EventRepository(writeContext);
    var location = new Location("Main Street 1");
    var newEvent = new Event(
        Guid.NewGuid(),
        Guid.NewGuid(),
        "Beach Cleanup",
        location
    );

    await sut.AddAsync(newEvent);

    await using var readContext = CreateContext();
    var saved = await readContext.Events.FirstOrDefaultAsync(e => e.Id == newEvent.Id);

    Assert.NotNull(saved);
    Assert.Equal("Beach Cleanup", saved.Name);
    Assert.Equal(newEvent.OrganizationId, saved.OrganizationId);
    Assert.Equal(EventStatus.Draft, saved.Status);
  }
}
