﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoIdentity.Models;

namespace ProyectoIdentity.Datos;

public class ApplicationDbContext : IdentityDbContext
{
  public ApplicationDbContext(DbContextOptions options) : base(options) { }

  public DbSet<AppUsuario> AppUsuarios { get; set; }
}
