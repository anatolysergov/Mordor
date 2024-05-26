using Microsoft.EntityFrameworkCore;
using Mordor.Domain.Entities;

namespace Mordor.Infrastructure.Data;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
	}

	public DbSet<AppUser> Users { get; set; }
}