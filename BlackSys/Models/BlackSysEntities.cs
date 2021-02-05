namespace BlackSys.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Infrastructure;

    public partial class BlackSysEntities : DbContext
    {
        public BlackSysEntities(): base("name=BlackSysConnection")
        {
        }

        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Component> Component { get; set; }
        public virtual DbSet<ComponentCategory> ComponentCategory { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomPermission> CustomPermission { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<MenuTemp> MenuTemp { get; set; }
        public virtual DbSet<PermissionMenu> PermissionMenu { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<UnitMeasure> UnitMeasure { get; set; }
        public virtual DbSet<Fabric> Fabric { get; set; }
        public virtual DbSet<FabricCategory> FabricCategory { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Solicitudes> Solicitudes { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<RoleName> RoleName { get; set; }
        public virtual DbSet<Mensajes> Mensajes { get; set; }
        public virtual DbSet<Activos> Activos { get; set; }
        public virtual DbSet<Responsable> Responsables { get; set; }
        public virtual DbSet<SubActivos> SubActivos { get; set; }
        public virtual DbSet<EstadosTickets> EstadosTickets { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Catalogo_Activos> Catalogo_Activos { get; set; }
        public virtual DbSet<tab_Activos> tab_Activos { get; set; }
        public virtual DbSet<cat_Activos> cat_Activos { get; set; }
        public virtual DbSet<ProductPhoto> ProductPhoto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Component>()
               .Property(e => e.PurchasingPrice)
               .HasPrecision(19, 4);

            modelBuilder.Entity<Component>()
                .Property(e => e.ListPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ComponentCategory>()
                .HasMany(e => e.Component)
                .WithRequired(e => e.ComponentCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.Permission)
                .WithRequired(e => e.AspNetRoles)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UnitMeasure>()
              .HasMany(e => e.Component)
              .WithRequired(e => e.UnitMeasure)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fabric>()
                .Property(e => e.PurchasingPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.StandardCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.ListPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.WeightUnitMeasureCode)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.Weight)
                .HasPrecision(8, 2);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.CustomPermission)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.CustomPermission)
                .WithRequired(e => e.Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.Permission)
                .WithRequired(e => e.Menu)
                .WillCascadeOnDelete(false);
        }
    }
}
