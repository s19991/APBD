using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace c03.EntityModels
{
    public partial class s19991Context : DbContext
    {
        public s19991Context()
        {
        }

        public s19991Context(DbContextOptions<s19991Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<Budzet> Budzet { get; set; }
        public virtual DbSet<Dept> Dept { get; set; }
        public virtual DbSet<Emp> Emp { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Gosc> Gosc { get; set; }
        public virtual DbSet<Kategoria> Kategoria { get; set; }
        public virtual DbSet<Lekarz> Lekarz { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }
        public virtual DbSet<Pacjent> Pacjent { get; set; }
        public virtual DbSet<Password> Password { get; set; }
        public virtual DbSet<Podwyzka> Podwyzka { get; set; }
        public virtual DbSet<Pokoj> Pokoj { get; set; }
        public virtual DbSet<Procedure> Procedure { get; set; }
        public virtual DbSet<ProcedureAnimal> ProcedureAnimal { get; set; }
        public virtual DbSet<Proj> Proj { get; set; }
        public virtual DbSet<ProjEmp> ProjEmp { get; set; }
        public virtual DbSet<Rezerwacja> Rezerwacja { get; set; }
        public virtual DbSet<Salgrade> Salgrade { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(x => x.IdAnimal)
                    .HasName("Animal_pk");

                entity.Property(e => e.AdmissionDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdOwnerNavigation)
                    .WithMany(p => p.Animal)
                    .HasForeignKey(x => x.IdOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Animal_Owner");
            });

            modelBuilder.Entity<Budzet>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("budzet");

                entity.Property(e => e.Wartosc).HasColumnName("wartosc");
            });

            modelBuilder.Entity<Dept>(entity =>
            {
                entity.HasKey(x => x.Deptno)
                    .HasName("PK__DEPT__E0EB08D79498139D");

                entity.ToTable("DEPT");

                entity.Property(e => e.Deptno)
                    .HasColumnName("DEPTNO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Dname)
                    .HasColumnName("DNAME")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Loc)
                    .HasColumnName("LOC")
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Emp>(entity =>
            {
                entity.HasKey(x => x.Empno)
                    .HasName("PK__EMP__14CCF2EECEAC9954");

                entity.ToTable("EMP");

                entity.Property(e => e.Empno)
                    .HasColumnName("EMPNO")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comm).HasColumnName("COMM");

                entity.Property(e => e.Deptno).HasColumnName("DEPTNO");

                entity.Property(e => e.Ename)
                    .HasColumnName("ENAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hiredate)
                    .HasColumnName("HIREDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Job)
                    .HasColumnName("JOB")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Mgr).HasColumnName("MGR");

                entity.Property(e => e.Sal).HasColumnName("SAL");

                entity.HasOne(d => d.DeptnoNavigation)
                    .WithMany(p => p.Emp)
                    .HasForeignKey(x => x.Deptno)
                    .HasConstraintName("FK__EMP__DEPTNO__04E4BC85");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(x => x.IdEnrollment)
                    .HasName("Enrollment_pk");

                entity.Property(e => e.IdEnrollment).ValueGeneratedNever();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdStudyNavigation)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(x => x.IdStudy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Enrollment_Studies");
            });

            modelBuilder.Entity<Gosc>(entity =>
            {
                entity.HasKey(x => x.IdGosc)
                    .HasName("PK__Gosc__8126AB6D5C1B5E15");

                entity.Property(e => e.IdGosc).ValueGeneratedNever();

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ProcentRabatu).HasColumnName("Procent_rabatu");
            });

            modelBuilder.Entity<Kategoria>(entity =>
            {
                entity.HasKey(x => x.IdKategoria)
                    .HasName("PK__Kategori__31412B26087B5C1E");

                entity.Property(e => e.IdKategoria).ValueGeneratedNever();

                entity.Property(e => e.Cena).HasColumnType("numeric(8, 2)");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lekarz>(entity =>
            {
                entity.HasKey(x => x.IdLekarz)
                    .HasName("PK__Lekarz__4E8CF2D3F51A04E5");

                entity.Property(e => e.Imie)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Specjalizacja)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(x => x.IdOwner)
                    .HasName("Owner_pk");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Pacjent>(entity =>
            {
                entity.HasKey(x => x.IdPacjent)
                    .HasName("PK__Pacjent__2C044263E3F698D0");

                entity.Property(e => e.DataUr)
                    .HasColumnName("Data_ur")
                    .HasColumnType("date");

                entity.Property(e => e.Imie)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Password>(entity =>
            {
                entity.HasKey(x => x.StudentIndexNumber)
                    .HasName("Password_pk");

                entity.Property(e => e.StudentIndexNumber).HasMaxLength(100);

                entity.Property(e => e.Password1)
                    .IsRequired()
                    .HasColumnName("Password")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Podwyzka>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataPodwyzki)
                    .HasColumnName("data_podwyzki")
                    .HasColumnType("date");

                entity.Property(e => e.Dzial).HasColumnName("dzial");

                entity.Property(e => e.IdOsoba).HasColumnName("id_osoba");

                entity.Property(e => e.Mgr).HasColumnName("mgr");

                entity.Property(e => e.Nazwisko)
                    .HasColumnName("nazwisko")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NowaPensja).HasColumnName("nowa_pensja");

                entity.Property(e => e.StaraPensja).HasColumnName("stara_pensja");
            });

            modelBuilder.Entity<Pokoj>(entity =>
            {
                entity.HasKey(x => x.NrPokoju)
                    .HasName("PK__Pokoj__18804ABE9631C9C8");

                entity.Property(e => e.NrPokoju).ValueGeneratedNever();

                entity.Property(e => e.LiczbaMiejsc).HasColumnName("Liczba_miejsc");

                entity.HasOne(d => d.IdKategoriaNavigation)
                    .WithMany(p => p.Pokoj)
                    .HasForeignKey(x => x.IdKategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pokoj__IdKategor__70DDC3D8");
            });

            modelBuilder.Entity<Procedure>(entity =>
            {
                entity.HasKey(x => x.IdProcedure)
                    .HasName("Procedure_pk");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ProcedureAnimal>(entity =>
            {
                entity.HasKey(x => new { x.ProcedureIdProcedure, x.AnimalIdAnimal, x.Date })
                    .HasName("Procedure_Animal_pk");

                entity.ToTable("Procedure_Animal");

                entity.Property(e => e.ProcedureIdProcedure).HasColumnName("Procedure_IdProcedure");

                entity.Property(e => e.AnimalIdAnimal).HasColumnName("Animal_IdAnimal");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.AnimalIdAnimalNavigation)
                    .WithMany(p => p.ProcedureAnimal)
                    .HasForeignKey(x => x.AnimalIdAnimal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_3_Animal");

                entity.HasOne(d => d.ProcedureIdProcedureNavigation)
                    .WithMany(p => p.ProcedureAnimal)
                    .HasForeignKey(x => x.ProcedureIdProcedure)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_3_Procedure");
            });

            modelBuilder.Entity<Proj>(entity =>
            {
                entity.HasKey(x => x.Projno)
                    .HasName("PK__PROJ__F7E30F7E4812F3B0");

                entity.ToTable("PROJ");

                entity.Property(e => e.Projno).HasColumnName("PROJNO");

                entity.Property(e => e.Budget)
                    .HasColumnName("BUDGET")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.EndDate)
                    .HasColumnName("END_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.Pname)
                    .HasColumnName("PNAME")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasColumnName("START_DATE")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<ProjEmp>(entity =>
            {
                entity.HasKey(x => new { x.Projno, x.Empno })
                    .HasName("PROJ_EMP_PRIMARY_KEY");

                entity.ToTable("PROJ_EMP");

                entity.Property(e => e.Projno).HasColumnName("PROJNO");

                entity.Property(e => e.Empno).HasColumnName("EMPNO");

                entity.HasOne(d => d.EmpnoNavigation)
                    .WithMany(p => p.ProjEmp)
                    .HasForeignKey(x => x.Empno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EMP_PROJEMP_KEY");

                entity.HasOne(d => d.ProjnoNavigation)
                    .WithMany(p => p.ProjEmp)
                    .HasForeignKey(x => x.Projno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PROJ_PROJEMP_KEY");
            });

            modelBuilder.Entity<Rezerwacja>(entity =>
            {
                entity.HasKey(x => x.IdRezerwacja)
                    .HasName("PK__Rezerwac__68F5E186315A7C3B");

                entity.Property(e => e.IdRezerwacja).ValueGeneratedNever();

                entity.Property(e => e.DataDo).HasColumnType("datetime");

                entity.Property(e => e.DataOd).HasColumnType("datetime");

                entity.HasOne(d => d.IdGoscNavigation)
                    .WithMany(p => p.Rezerwacja)
                    .HasForeignKey(x => x.IdGosc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rezerwacj__IdGos__73BA3083");

                entity.HasOne(d => d.NrPokojuNavigation)
                    .WithMany(p => p.Rezerwacja)
                    .HasForeignKey(x => x.NrPokoju)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rezerwacj__NrPok__74AE54BC");
            });

            modelBuilder.Entity<Salgrade>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SALGRADE");

                entity.Property(e => e.Grade).HasColumnName("GRADE");

                entity.Property(e => e.Hisal).HasColumnName("HISAL");

                entity.Property(e => e.Losal).HasColumnName("LOSAL");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(x => x.IndexNumber)
                    .HasName("Student_pk");

                entity.Property(e => e.IndexNumber).HasMaxLength(100);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdEnrollmentNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(x => x.IdEnrollment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Enrollment");
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(x => x.IdStudy)
                    .HasName("Studies_pk");

                entity.Property(e => e.IdStudy).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
