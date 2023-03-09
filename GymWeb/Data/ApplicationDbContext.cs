using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GymWeb.Models;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace GymWeb.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        public DbSet<GymWeb.Models.Aluno>? Alunos { get; set; }
        public DbSet<GymWeb.Models.Exercicio>? Exercicios { get; set; }
        public DbSet<GymWeb.Models.Treino>? Treinos { get; set; }
        public DbSet<ExercicioTreino> ExerciciosTreinos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

          
            }
                 
                
        }


   }

