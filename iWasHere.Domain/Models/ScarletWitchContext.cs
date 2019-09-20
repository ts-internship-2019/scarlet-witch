using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iWasHere.Domain.Models
{
    public partial class ScarletWitchContext : DbContext
    {
        public ScarletWitchContext()
        {
        }

        public ScarletWitchContext(DbContextOptions<ScarletWitchContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CountryXlanguage> CountryXlanguage { get; set; }
        public virtual DbSet<DictionaryCity> DictionaryCity { get; set; }
        public virtual DbSet<DictionaryCountry> DictionaryCountry { get; set; }
        public virtual DbSet<DictionaryCounty> DictionaryCounty { get; set; }
        public virtual DbSet<DictionaryCurrency> DictionaryCurrency { get; set; }
        public virtual DbSet<DictionaryInterval> DictionaryInterval { get; set; }
        public virtual DbSet<DictionaryLandmarkType> DictionaryLandmarkType { get; set; }
        public virtual DbSet<DictionaryLanguage> DictionaryLanguage { get; set; }
        public virtual DbSet<DictionaryTicketType> DictionaryTicketType { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Landmark> Landmark { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ts-internship-2019.database.windows.net;Initial Catalog=ScarletWitch;Persist Security Info=False;User ID=sa_admin;Password=A123456a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<CountryXlanguage>(entity =>
            {
                entity.ToTable("CountryXLanguage");

                entity.Property(e => e.CountryXlanguageId).HasColumnName("CountryXLanguageId");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryXlanguage)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CountryXL__Count__756D6ECB");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.CountryXlanguage)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CountryXL__Langu__7755B73D");
            });

            modelBuilder.Entity<DictionaryCity>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__Dictiona__F2D21B766350E6D9");

                entity.Property(e => e.CityName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.County)
                    .WithMany(p => p.DictionaryCity)
                    .HasForeignKey(d => d.CountyId)
                    .HasConstraintName("FK__Dictionar__Count__44CA3770");
            });

            modelBuilder.Entity<DictionaryCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__Dictiona__10D1609F0BCC6A01");

                entity.HasIndex(e => e.CountryName)
                    .HasName("UQ__Dictiona__E056F201EF00D2BF")
                    .IsUnique();

                entity.HasIndex(e => e.LanguageId)
                    .HasName("UQ__Dictiona__B93855AA590E2E64")
                    .IsUnique();

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DictionaryCounty>(entity =>
            {
                entity.HasKey(e => e.CountyId)
                    .HasName("PK__Dictiona__B68F9D97FA9C0A58");

                entity.Property(e => e.CountyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.DictionaryCounty)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Dictionar__Count__45BE5BA9");
            });

            modelBuilder.Entity<DictionaryCurrency>(entity =>
            {
                entity.HasKey(e => e.CurrencyId)
                    .HasName("PK__Dictiona__14470AF0ADABDE37");

                entity.Property(e => e.CurrencyCode).HasMaxLength(100);

                entity.Property(e => e.CurrencyExchange).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CurrencyName).HasMaxLength(256);


            });

            modelBuilder.Entity<DictionaryInterval>(entity =>
            {
                entity.HasKey(e => e.VisitIntervalId)
                    .HasName("PK__Dictiona__3B57BD8B787186AC");

                entity.HasIndex(e => e.VisitIntervalName)
                    .HasName("UQ__Dictiona__1884603892DA23B0")
                    .IsUnique();

                entity.Property(e => e.VisitIntervalName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DictionaryLandmarkType>(entity =>
            {
                entity.HasKey(e => e.LandmarkTypeId)
                    .HasName("PK__Dictiona__A5900F200914E136");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.LandmarkTypeCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionaryLanguage>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                    .HasName("PK__Dictiona__B93855AB0345D294");

                entity.HasIndex(e => e.LanguageCode)
                    .HasName("UQ__Dictiona__8B8C8A34B720C469")
                    .IsUnique();

                entity.Property(e => e.LanguageCode)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LanguageName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DictionaryTicketType>(entity =>
            {
                entity.HasKey(e => e.TicketTypeId)
                    .HasName("PK__Dictiona__6CD684317BF97CBC");

                entity.Property(e => e.TicketTypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__Images__7516F70C6343A6DC");

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.HasOne(d => d.Landmark)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.LandmarkId)
                    .HasConstraintName("FK__Images__Landmark__47A6A41B");
            });

            modelBuilder.Entity<Landmark>(entity =>
            {
                entity.Property(e => e.LandmarkDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StreetName).HasMaxLength(256);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__Landmark__CityId__6FB49575");

                entity.HasOne(d => d.LandmarkType)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.LandmarkTypeId)
                    .HasConstraintName("FK__Landmark__Landma__6EC0713C");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK__Landmark__Ticket__6AEFE058");

                entity.HasOne(d => d.VisitInterval)
                    .WithMany(p => p.Landmark)
                    .HasForeignKey(d => d.VisitIntervalId)
                    .HasConstraintName("FK__Landmark__VisitI__40058253");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.Grade).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Review1).HasColumnName("Review");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Landmark)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.LandmarkId)
                    .HasConstraintName("FK__Review__Landmark__489AC854");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Review__UserId__7849DB76");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TicketName).HasMaxLength(256);

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("FK__Ticket__Currency__793DFFAF");

                entity.HasOne(d => d.TicketType)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.TicketTypeId)
                    .HasConstraintName("FK__Ticket__TicketTy__6CD828CA");
            });
        }
    }
}
