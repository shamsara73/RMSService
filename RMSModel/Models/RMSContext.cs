using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RMSModel.Models;

public partial class RMSContext : DbContext
{
    public RMSContext(DbContextOptions<RMSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLog { get; set; }

    public virtual DbSet<InvApprv> InvApprv { get; set; }

    public virtual DbSet<MsBuyer> MsBuyer { get; set; }

    public virtual DbSet<MsInvoice> MsInvoice { get; set; }

    public virtual DbSet<MsMenu> MsMenu { get; set; }

    public virtual DbSet<MsParameter> MsParameter { get; set; }

    public virtual DbSet<MsPayment> MsPayment { get; set; }

    public virtual DbSet<MsRoleMenu> MsRoleMenu { get; set; }

    public virtual DbSet<MsSeller> MsSeller { get; set; }

    public virtual DbSet<MsUser> MsUser { get; set; }

    public virtual DbSet<MsUserMenu> MsUserMenu { get; set; }

    public virtual DbSet<MsUserRole> MsUserRole { get; set; }

    public virtual DbSet<MsUserToken> MsUserToken { get; set; }

    public virtual DbSet<Payment> Payment { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetail { get; set; }

    public virtual DbSet<Temp_TrInvoiceUpload> Temp_TrInvoiceUpload { get; set; }

    public virtual DbSet<TrInvoicePayment> TrInvoicePayment { get; set; }

    public virtual DbSet<TrInvoicePaymentDetail> TrInvoicePaymentDetail { get; set; }

    public virtual DbSet<TrInvoiceUpload> TrInvoiceUpload { get; set; }

    public virtual DbSet<VwUserMenu> VwUserMenu { get; set; }

    public virtual DbSet<VwUserRoleMenu> VwUserRoleMenu { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string basePath = System.AppContext.BaseDirectory;
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("dbConfig.json")
            .Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

    }
    public RMSContext()
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AuditLog__3214EC07C837B613");

            entity.Property(e => e.CreatedDate)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IP)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InvApprv>(entity =>
        {
            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ApprovedDate)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ApprovedRole)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MsBuyer>(entity =>
        {
            entity.HasKey(e => e.BuyerId);

            entity.Property(e => e.BuyerAddrs)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BuyerCity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BuyerCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BuyerDist)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BuyerName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BuyerPaymentTo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.BuyerPhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BuyerProv)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BuyerZipCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MsInvoice>(entity =>
        {
            entity.HasKey(e => e.InvId);

            entity.Property(e => e.InvRef)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.InvRemarks)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.InvUplDate)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MsMenu>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("MsMenu_PK");

            entity.Property(e => e.LINK)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NAME)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MsParameter>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("MsParameter_PK");

            entity.Property(e => e.CREATED_DATE).HasColumnType("datetime");
            entity.Property(e => e.PRM_DESC).IsUnicode(false);
            entity.Property(e => e.PRM_DESC2).IsUnicode(false);
            entity.Property(e => e.PRM_TEXT).IsUnicode(false);
        });

        modelBuilder.Entity<MsPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId);

            entity.Property(e => e.PaymentCode)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MsRoleMenu>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("MsRoleMenu_PK");

            entity.HasOne(d => d.IDMENUNavigation).WithMany(p => p.MsRoleMenu)
                .HasForeignKey(d => d.IDMENU)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MsRoleMenu_FK");

            entity.HasOne(d => d.IDROLENavigation).WithMany(p => p.MsRoleMenu)
                .HasForeignKey(d => d.IDROLE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MsRoleMenu_FK_1");
        });

        modelBuilder.Entity<MsSeller>(entity =>
        {
            entity.HasKey(e => e.SellerId);

            entity.Property(e => e.SellerAddrs)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SellerCity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SellerCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SellerDist)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SellerName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SellerPhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SellerProv)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SellerZipCode)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MsUser>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("MsUser_PK");

            entity.Property(e => e.CreatedDate)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsApproved).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsVerified).HasDefaultValueSql("((0))");
            entity.Property(e => e.PASSWORD)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.USERNAME)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserRole)
                .HasMaxLength(2)
                .IsUnicode(false);

            entity.HasOne(d => d.IDROLENavigation).WithMany(p => p.MsUser)
                .HasForeignKey(d => d.IDROLE)
                .HasConstraintName("MsUser_FK");
        });

        modelBuilder.Entity<MsUserMenu>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("MsUserMenu_PK");

            entity.HasOne(d => d.IDMENUNavigation).WithMany(p => p.MsUserMenu)
                .HasForeignKey(d => d.IDMENU)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MsUserMenu_Menu_FK");

            entity.HasOne(d => d.IDUSERNavigation).WithMany(p => p.MsUserMenu)
                .HasForeignKey(d => d.IDUSER)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MsUserMenu_User_FK");
        });

        modelBuilder.Entity<MsUserRole>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("MsUserRole_PK");

            entity.Property(e => e.CODE)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CREATED_BY)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CREATED_DATE).HasColumnType("datetime");
            entity.Property(e => e.DESCRIPT).IsUnicode(false);
            entity.Property(e => e.NAME)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MsUserToken>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("MsUserToken_PK");

            entity.Property(e => e.ACCESS_TOKEN).IsUnicode(false);
            entity.Property(e => e.ACCESS_TOKEN_EXPIRY).HasColumnType("datetime");
            entity.Property(e => e.REFRESH_TOKEN).IsUnicode(false);
            entity.Property(e => e.REFRESH_TOKEN_EXPIRY).HasColumnType("datetime");

            entity.HasOne(d => d.USER).WithMany(p => p.MsUserToken)
                .HasForeignKey(d => d.USERID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MsUserToken_FK");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedDate)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InvoiceDueDate)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.BuyerCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SellerCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.VABank)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.VAName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Temp_TrInvoiceUpload>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("Temp_TrInvoiceUpload_PK");

            entity.Property(e => e.AMOUNT_STRING)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BATCH_NO)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BUYER_CODE)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DUE_DATE).HasColumnType("datetime");
            entity.Property(e => e.DUE_DATE_STRING)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.INVOICE_NO)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SELLER_CODE)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.STATUS_RECEIVE)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UPLOAD_DATE).HasColumnType("datetime");
            entity.Property(e => e.UPLOAD_MESSAGE)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UPLOAD_STATUS)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrInvoicePayment>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("TrInvoicePayment_PK");

            entity.Property(e => e.CREATED_DATE).HasColumnType("datetime");
            entity.Property(e => e.PAYMENT_METHOD)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrInvoicePaymentDetail>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("TrInvoicePaymentDetail_PK");

            entity.Property(e => e.CREATED_DATE).HasColumnType("datetime");
        });

        modelBuilder.Entity<TrInvoiceUpload>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("TrInvoiceUpload_PK");

            entity.Property(e => e.AMOUNT_STRING)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BATCH_NO)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BUYER_CODE)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DUE_DATE).HasColumnType("datetime");
            entity.Property(e => e.DUE_DATE_STRING)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.INVOICE_NO)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SELLER_CODE)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.STATUS_RECEIVE)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UPLOAD_DATE).HasColumnType("datetime");
        });

        modelBuilder.Entity<VwUserMenu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwUserMenu");

            entity.Property(e => e.MENULINK)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MENUNAME)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.USERNAME)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwUserRoleMenu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwUserRoleMenu");

            entity.Property(e => e.MENULINK)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MENUNAME)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.USERNAME)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
