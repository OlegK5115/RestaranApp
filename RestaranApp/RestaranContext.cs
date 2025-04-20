using Microsoft.EntityFrameworkCore;
using RestaranApp.Entities;

namespace RestaranApp
{
    public class RestaranContext : DbContext
    {
        // добавление таблиц
        public DbSet<User> User { get; set; } 
        public DbSet<Restaran> Restaran { get; set; }
        public DbSet<Order> Order { get; set; }

        // 1ый вариант иниц. подключения (объект подключения создается извне)
        public RestaranContext(DbContextOptions<RestaranContext> options)
        : base(options)
        {
            Database.EnsureCreated(); // создание бд
        }

        // 2ой вариант иниц. подключения (объект подключения создается внутри)
        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=example;Username=root;Password='1234'");
        } */
    }
}

/* класс Context необходим для подключения к базе данных */
/* Для работа с любой субд необходимо установить отдельный пакет:
 *    1) Postgres: Npgsql.EntityFrameworkCore.PostgreSQL
 */