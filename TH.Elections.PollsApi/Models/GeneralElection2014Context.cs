using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TH.Elections.PollsApi.Models
{
    public partial class GeneralElection2014Context : DbContext
    {
        public virtual DbSet<PartyVotingIntentions> PartyVotingIntentions { get; set; }
        public virtual DbSet<PoliticalParties> PoliticalParties { get; set; }
        public virtual DbSet<PollingCompanies> PollingCompanies { get; set; }
        public virtual DbSet<Polls> Polls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB;Database=GeneralElection2014;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartyVotingIntentions>(entity =>
            {
                entity.HasKey(e => new { e.PartyId, e.PollId })
                    .HasName("PK_PartyVote");

                entity.Property(e => e.PartyId).HasColumnName("PartyID");

                entity.Property(e => e.PollId).HasColumnName("PollID");

                entity.HasOne(d => d.Party)
                    .WithMany(p => p.PartyVotingIntentions)
                    .HasForeignKey(d => d.PartyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__PartyVoti__Party__59FA5E80");

                entity.HasOne(d => d.Poll)
                    .WithMany(p => p.PartyVotingIntentions)
                    .HasForeignKey(d => d.PollId)
                    .HasConstraintName("FK_PartyVotingIntentions_0");
            });

            modelBuilder.Entity<PoliticalParties>(entity =>
            {
                entity.HasKey(e => e.PartyId)
                    .HasName("PK_Party");

                entity.Property(e => e.PartyId).HasColumnName("PartyID");

                entity.Property(e => e.PartyColour).HasColumnType("nchar(7)");

                entity.Property(e => e.PartyName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<PollingCompanies>(entity =>
            {
                entity.HasKey(e => e.PollingCompanyId)
                    .HasName("PK_PollingCompany");

                entity.Property(e => e.PollingCompanyId).HasColumnName("PollingCompanyID");

                entity.Property(e => e.CurrentlyPolling).HasDefaultValueSql("1");

                entity.Property(e => e.PollingCompanyName)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Polls>(entity =>
            {
                entity.HasKey(e => e.PollId)
                    .HasName("PK_Poll");

                entity.HasIndex(e => new { e.PollingCompanyId, e.PollDate })
                    .HasName("UNQ_PollingCompany_Data")
                    .IsUnique();

                entity.Property(e => e.PollId).HasColumnName("PollID");

                entity.Property(e => e.PollDate).HasColumnType("date");

                entity.Property(e => e.PollingCompanyId).HasColumnName("PollingCompanyID");

                entity.HasOne(d => d.PollingCompany)
                    .WithMany(p => p.Polls)
                    .HasForeignKey(d => d.PollingCompanyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Polls__PollingCo__5DCAEF64");
            });
        }
    }
}