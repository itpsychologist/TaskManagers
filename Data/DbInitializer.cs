using Microsoft.EntityFrameworkCore;
using TaskManagers.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManagers.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(TaskManagersDbContext context)
        {
            await context.Database.MigrateAsync();

            // Check if database already has data
            if (context.Positions.Any() || context.TaskTypes.Any() || context.Workers.Any())
            {
                return; // Database has been seeded
            }

            // Seed Positions
            var positions = new List<Position>
        {
            new Position { Name = "Developer" },
            new Position { Name = "Project Manager" },
            new Position { Name = "QA" },
            new Position { Name = "Designer" },
            new Position { Name = "DevOps" }
        };
            await context.Positions.AddRangeAsync(positions);
            await context.SaveChangesAsync();

            // Seed TaskTypes
            var taskTypes = new List<TaskType>
        {
            new TaskType { Name = "Bug" },
            new TaskType { Name = "New feature" },
            new TaskType { Name = "Breaking change" },
            new TaskType { Name = "Refactoring" },
            new TaskType { Name = "QA" }
        };
            await context.TaskTypes.AddRangeAsync(taskTypes);
            await context.SaveChangesAsync();

            // Seed Workers
            var workers = new List<Worker>
        {
            new Worker
            {
                Username = "john.dev",
                Email = "john@example.com",
                Password = "hashedPassword123", // In real app, use proper password hashing
                FirstName = "John",
                LastName = "Smith",
                Position = positions[0] // Developer
            },
            new Worker
            {
                Username = "mary.pm",
                Email = "mary@example.com",
                Password = "hashedPassword123",
                FirstName = "Mary",
                LastName = "Johnson",
                Position = positions[1] // Project Manager
            },
            new Worker
            {
                Username = "bob.qa",
                Email = "bob@example.com",
                Password = "hashedPassword123",
                FirstName = "Bob",
                LastName = "Wilson",
                Position = positions[2] // QA
            },
            new Worker
            {
                Username = "alice.designer",
                Email = "alice@example.com",
                Password = "hashedPassword123",
                FirstName = "Alice",
                LastName = "Brown",
                Position = positions[3] // Designer
            },
            new Worker
            {
                Username = "dave.devops",
                Email = "dave@example.com",
                Password = "hashedPassword123",
                FirstName = "Dave",
                LastName = "Miller",
                Position = positions[4] // DevOps
            }
        };
            await context.Workers.AddRangeAsync(workers);
            await context.SaveChangesAsync();

            // Seed Tasks
            var tasks = new List<WorkTask>
        {
            new WorkTask
            {
                Name = "Fix Dashboard for Vacancies",
                Description = "Dashboard showing incorrect number of vacancies. Need to fix the counting logic.",
                Deadline = DateTime.Now.AddDays(7),
                IsCompleted = false,
                Priority = Priority.High,
                TaskType = taskTypes[0], // Bug
                Assignees = new List<Worker> { workers[0], workers[2] } // Developer and QA
            },
            new WorkTask
            {
                Name = "Add sign-in with Google button",
                Description = "Implement OAuth2 authentication with Google.",
                Deadline = DateTime.Now.AddDays(14),
                IsCompleted = false,
                Priority = Priority.Medium,
                TaskType = taskTypes[1], // New feature
                Assignees = new List<Worker> { workers[0], workers[4] } // Developer and DevOps
            },
            new WorkTask
            {
                Name = "Update API endpoints",
                Description = "Breaking changes in API v2. Need to update all endpoints.",
                Deadline = DateTime.Now.AddDays(5),
                IsCompleted = false,
                Priority = Priority.Urgent,
                TaskType = taskTypes[2], // Breaking change
                Assignees = new List<Worker> { workers[0], workers[4], workers[2] } // Developer, DevOps, and QA
            },
            new WorkTask
            {
                Name = "Refactor authentication module",
                Description = "Clean up authentication code and improve error handling.",
                Deadline = DateTime.Now.AddDays(10),
                IsCompleted = false,
                Priority = Priority.Low,
                TaskType = taskTypes[3], // Refactoring
                Assignees = new List<Worker> { workers[0] } // Developer
            },
            new WorkTask
            {
                Name = "Test new features in production",
                Description = "Comprehensive testing of all new features deployed to production.",
                Deadline = DateTime.Now.AddDays(3),
                IsCompleted = false,
                Priority = Priority.High,
                TaskType = taskTypes[4], // QA
                Assignees = new List<Worker> { workers[2] } // QA
            }
        };
            await context.WorkTasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();
        }
    }
}
