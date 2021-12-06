using BugTracker.Dal;
using BugTracker.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BugTrackerTests {
    public class UnitTest1 {
        [Fact]
        public void GetProjects_AssertCount() {
            using (var db = new BugTrackerDbContext(Utilities.UtilitiesClass.TestDbContextOptions())) {

                var projects = Utilities.UtilitiesClass.GetSeedingProjects();
                db.AddRange(projects);
                db.SaveChanges();

                IList<Project> expectedProjects = db.Projects.ToList();

                Assert.Equal(4, expectedProjects.Count);
            }
        }

        [Fact]
        public void GetProjects_AssertDescription() {
            using (var db = new BugTrackerDbContext(Utilities.UtilitiesClass.TestDbContextOptions())) {

                var projects = Utilities.UtilitiesClass.GetSeedingProjects();
                db.AddRange(projects);
                db.SaveChanges();

                Project project = db.Projects.Where(p => p.Id == 2).FirstOrDefault();

                Assert.Equal("asp .net core project", project.ProjectDescreption);
            }
        }

        [Fact]
        public void GetIssue_AssertCreationDate() {
            using (var db = new BugTrackerDbContext(Utilities.UtilitiesClass.TestDbContextOptions())) {

                var issues = Utilities.UtilitiesClass.GetSeedingIssues();
                db.AddRange(issues);
                db.SaveChanges();

                Issue issue = db.Issues.Where(i => i.Id == 3).FirstOrDefault();

                Assert.Equal(new DateTime(2021, 9, 28), issue.CreationDate);
            }
        }

        [Fact]
        public void DeleteIssue_AssertNotFound() {
            using (var db = new BugTrackerDbContext(Utilities.UtilitiesClass.TestDbContextOptions())) {

                var issues = Utilities.UtilitiesClass.GetSeedingIssues();
                db.AddRange(issues);
                db.SaveChanges();

                IList<Issue> expectedIssues = db.Issues.Where(i => i.Id != 3).ToList();

                Issue issue = db.Issues.Where(i => i.Id == 3).FirstOrDefault();

                if (issue != null) {
                    db.Issues.Remove(issue);
                    db.SaveChanges();
                }

                IList<Issue> actualIssues = db.Issues.ToList();

                Assert.Equal(expectedIssues, actualIssues);
            }
        }
    }
}