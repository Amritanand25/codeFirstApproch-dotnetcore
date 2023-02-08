using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using codeFirstApprochExample.Models;

namespace codeFirstApprochExample.Data
{
    public partial class codeFirstApprochExampleContext : DbContext
    {
        public codeFirstApprochExampleContext (DbContextOptions<codeFirstApprochExampleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Courses> Course { get; set; } = null!;
        public virtual DbSet<Author> Author { get; set; } = null!;
        public virtual DbSet<Tag> Tag { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Courses>(entity =>
            {
                entity.ToTable("Course");

                entity.HasOne(d => d.Author)
                    .WithMany(d=> d.Courses)
                    .HasForeignKey(d => d.AuthorId) 
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Course_Author_AuthorId");

                entity.HasMany(d => d.Tags)
                    .WithMany(d => d.Courses)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseTag",
                        l => l.HasOne<Tag>().WithMany().HasForeignKey("TagsId").OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_CourseTag_Tag_TagsId"),
                        r => r.HasOne<Courses>().WithMany().HasForeignKey("CoursesId").OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_CourseTag_Course_CoursesId"),
                        j => {
                            j.HasKey("CoursesId", "TagsId").HasName("PK_CourseTag");
                            j.ToTable("CourseTag");
                        }
                    );
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelbuilder);
    }
}
