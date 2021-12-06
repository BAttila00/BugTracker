using BugTracker.Dal;
using BugTracker.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTrackerTests.Utilities {
    public static class UtilitiesClass {
        public static DbContextOptions<BugTrackerDbContext> TestDbContextOptions() {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<BugTrackerDbContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        public static List<Project> GetSeedingProjects() {
            return new List<Project>()
            {
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
            };
        }

        public static List<Issue> GetSeedingIssues() {
            return new List<Issue>()
            {
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
            };
        }
    }
}

