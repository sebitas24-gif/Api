using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modelos;

    public class ApiContext : DbContext
    {
        public ApiContext (DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Raza> Razas { get; set; } = default!;

public DbSet<Especie> Especies { get; set; } = default!;

public DbSet<Animal> Animals { get; set; } = default!;
    }
