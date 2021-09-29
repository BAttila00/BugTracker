using BugTracker.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.EntityConfiguration {
    public class IssueEntityConfiguration : IEntityTypeConfiguration<Issue> {
        public void Configure(EntityTypeBuilder<Issue> builder) {
            builder.HasData(
                new Issue {
                    Id = 1,
                    Descreption = "Issue01",
                    CreatorId = 1,
                    IssueStatus = IssueStatus.Assigned,
                    CreationDate = new DateTime(2021, 9, 28),
                    AssignedToId = 1,
                    ProjectId = 1,
                    IssuePriority = IssuePriority.Low,
                    ModifiedOn = new DateTime(2021, 9, 29),
                    ModifiedById = 1
                },
                new Issue {
                    Id = 2,
                    Descreption = "Issue02",
                    CreatorId = 1,
                    IssueStatus = IssueStatus.Unassigned,
                    CreationDate = new DateTime(2021, 9, 28),
                    ProjectId = 1,
                    IssuePriority = IssuePriority.Low,
                    ModifiedOn = new DateTime(2021, 9, 29),
                    ModifiedById = 1
                },
                new Issue {
                    Id = 3,
                    Descreption = "Issue03",
                    CreatorId = 1,
                    IssueStatus = IssueStatus.Unassigned,
                    CreationDate = new DateTime(2021, 9, 28),
                    AssignedToId = 1,
                    ProjectId = 2,
                    IssuePriority = IssuePriority.Low,
                    ModifiedOn = new DateTime(2021, 9, 29),
                    ModifiedById = 1
                },
                new Issue {
                    Id = 4,
                    Descreption = "Issue03",
                    CreatorId = 1,
                    IssueStatus = IssueStatus.Assigned,
                    CreationDate = new DateTime(2021, 9, 28),
                    AssignedToId = 1,
                    ProjectId = 3,
                    IssuePriority = IssuePriority.Low,
                    ModifiedOn = new DateTime(2021, 9, 29),
                    ModifiedById = 1
                }

                );
        }
    }
}
