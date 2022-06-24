#nullable disable

using Microsoft.EntityFrameworkCore;
using todoList;

    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {

        }
        public DbSet<ToDo> ToDo { get; set; }
        public DbSet<SubTd> SubTd {get;set;}
        public DbSet<TdList> TdList {get;set;}
        public DbSet<User> User {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasMany(s => s.SubItems).WithOne().HasForeignKey(a => a.ToDoId);
            modelBuilder.Entity<User>().HasMany(s => s.TdLists).WithOne().HasForeignKey(a => a.UserId);
            modelBuilder.Entity<TdList>().HasMany(s => s.ToDoItems).WithOne().HasForeignKey(a => a.TdListId);
        }
    }