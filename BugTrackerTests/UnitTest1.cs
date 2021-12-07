using BugTracker.Dal;
using BugTracker.Dal.Entities;
using BugTracker.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
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





        [Fact]
        public void DeleteIssue_RedirectCheck() {
            Mock<UserManager<User>> _userManager;
            Mock<IUserStore<User>> _userStore;
            BugTracker.Web.Pages.Issues.DeleteModel _pageModel;
            Mock<ILogger<BugTracker.Web.Pages.Issues.DeleteModel>> _logger;
            Mock<IDbLogger> _dbLogger;

            _userStore = new Mock<IUserStore<User>>();
            _userManager = new Mock<UserManager<User>>(_userStore.Object, null, null, null, null, null, null, null, null);
            _logger = new Mock<ILogger<BugTracker.Web.Pages.Issues.DeleteModel>>();
            _dbLogger = new Mock<IDbLogger>();

            using (var db = new BugTrackerDbContext(Utilities.UtilitiesClass.TestDbContextOptions())) {
                _pageModel = new BugTracker.Web.Pages.Issues.DeleteModel(db, _logger.Object, _dbLogger.Object, _userManager.Object);
                var result = _pageModel.OnPostAsync(1);

                Assert.Equal(typeof(RedirectToPageResult), result.Result.GetType());

                RedirectToPageResult redirect = result.Result as RedirectToPageResult;
                Assert.Equal("./Index", redirect.PageName);
            }

        }
    }
}