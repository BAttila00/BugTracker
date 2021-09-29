using BugTracker.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.EntityConfiguration {
    public class ProjectEntityConfiguration : IEntityTypeConfiguration<Project> {
        public void Configure(EntityTypeBuilder<Project> builder) {
            builder.HasData(
                new Project {
                    Id = 1,
                    CreationDate = new DateTime(2021, 9, 28),
                    PlannedFinishDate = new DateTime(2022, 10, 20),
                    CreatorId = 1,
                    ProjectName = "Proj01",
                    ProjectDescreption = "Just a project",
                    ProjectStatus = ProjectStatus.Development,
                    ModifiedOn = new DateTime(2021, 9, 28),
                    ModifiedById = 1
                },
                new Project {
                    Id = 2,
                    CreationDate = new DateTime(2021, 9, 28),
                    PlannedFinishDate = new DateTime(2022, 10, 20),
                    CreatorId = 1,
                    ProjectName = "Proj02",
                    ProjectDescreption = "asp .net core project",
                    ProjectStatus = ProjectStatus.Development,
                    ModifiedOn = new DateTime(2021, 9, 28),
                    ModifiedById = 1,
                }, 
                new Project {
                    Id = 3,
                    CreationDate = new DateTime(2021, 9, 28),
                    PlannedFinishDate = new DateTime(2022, 10, 20),
                    CreatorId = 1,
                    ProjectName = "Proj03",
                    ProjectDescreption = "java project",
                    ProjectStatus = ProjectStatus.Development,
                    ModifiedOn = new DateTime(2021, 9, 28),
                    ModifiedById = 1,
                },
                new Project {
                    Id = 4,
                    CreationDate = new DateTime(2021, 9, 28),
                    PlannedFinishDate = new DateTime(2022, 10, 20),
                    CreatorId = 1,
                    ProjectName = "Proj04",
                    ProjectDescreption = "android project",
                    ProjectStatus = ProjectStatus.Development,
                    ModifiedOn = new DateTime(2021, 9, 28),
                    ModifiedById = 1,
                }

                );
        }
    }
}
