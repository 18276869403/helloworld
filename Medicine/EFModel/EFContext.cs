namespace EFModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EFContext : DbContext
    {
        public EFContext()
            : base("name=EFContext")
        {
        }

        public virtual DbSet<Classify> Classify { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DosageType> DosageType { get; set; }
        public virtual DbSet<EnterCompany> EnterCompany { get; set; }
        public virtual DbSet<EnterInfo> EnterInfo { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<MarketInfo> MarketInfo { get; set; }
        public virtual DbSet<MedicineInfo> MedicineInfo { get; set; }
        public virtual DbSet<PackTable> PackTable { get; set; }
        public virtual DbSet<PowerInfo> PowerInfo { get; set; }
        public virtual DbSet<PriceInfo> PriceInfo { get; set; }
        public virtual DbSet<R_RoleInfo_PowerInfo> R_RoleInfo_PowerInfo { get; set; }
        public virtual DbSet<R_UserInfo_RoleInfo> R_UserInfo_RoleInfo { get; set; }
        public virtual DbSet<Reposit> Reposit { get; set; }
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnterInfo>()
                .Property(e => e.EnterPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PackTable>()
                .Property(e => e.PackName)
                .IsUnicode(false);

            modelBuilder.Entity<PriceInfo>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.UserPwd)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
