using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Messages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Infrastructure;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext>
        options) : base(options)
    {
    }

    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<TextMessage> TextMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TextMessage>().HasKey(x => x.Id);
        builder.Entity<ChatRoom>()
            .Property(e => e.Users)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        
        base.OnModelCreating(builder);
    }
}