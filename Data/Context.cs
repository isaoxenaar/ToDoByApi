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
    }