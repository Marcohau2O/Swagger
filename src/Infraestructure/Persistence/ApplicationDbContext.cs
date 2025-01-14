﻿
using ApplicationCore.Interfaces;
using Domain.Entities;
using Finbuckle.MultiTenant;
using Infraestructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class ApplicationDbContext : BaseDbContext
    {
        public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUserService currentUser)
            : base(currentTenant, options, currentUser)
        {

        }

        public DbSet<persona> persona {  get; set; }
        public DbSet<logs> logs { get; set; }
        public DbSet<jugador> jugador { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet <Colaboradores> Colaboradores { get; set; }
        public DbSet<Profesor> Profesor { get; set; }
        public DbSet<Administrativo> Administrativo { get; set; }
        public DbSet<FormularioContacto> FormularioContactos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Profesor>()
                .HasOne(p => p.Colaboradores)
                .WithOne(c => c.Profesor)
                .HasForeignKey<Profesor>(p => p.FkColaborador);

            modelBuilder.Entity<Administrativo>()
                .HasOne(a => a.Colaboradores)
                .WithOne(c => c.Administrativo)
                .HasForeignKey<Administrativo>(a => a.FkColaborador);

        }      
    }
}
