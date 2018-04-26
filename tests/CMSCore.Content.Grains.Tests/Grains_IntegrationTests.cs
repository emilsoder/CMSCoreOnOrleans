using System;
using System.Linq;
using CMSCore.Content.Data;
using CMSCore.Content.Models;
using CMSCore.Content.Models.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CMSCore.Content.Grains.Tests
{
    public class Grains_IntegrationTests
    {
        public Grains_IntegrationTests()
        {
            _context = new ContentDbContext(ContentDbContextOptions.DefaultPostgresOptions);
            _accountGrain = new AccountGrain(_context, CreateMockLogger());
        }

        private readonly AccountGrain _accountGrain;
        private readonly ContentDbContext _context;
        private string adminId = "9d7ea03d-f23d-462e-a2a7-c40f9c2d4a87";

        private ILogger<AccountGrain> CreateMockLogger()
        {
            return new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider().GetService<ILoggerFactory>().CreateLogger<AccountGrain>();
        }

        private string test4UserId = "ab3615ee-f4ad-427f-bd01-d73af5a53a96";

        [Fact]
        public void Create_User_Throws()
        {
            // Arrange
            var user = new User(Guid.NewGuid().ToString())
            {
                FirstName = "Integration5",
                LastName = "Test5",
                Email = "integration5@test5.com",
            };
            var operation = new CreateOperation<User>(adminId, user);
            // Act
            var result = _accountGrain.Create(operation).GetAwaiter().GetResult();

            // Assert
            Assert.True(result.Succeeded == false && result.Message != null);
        }


        [Fact]
        public void Verify_History_OnCreate()
        {
            // Arrange
            var user = new User(Guid.NewGuid().ToString())
            {
                FirstName = "Integration5",
                LastName = "Test5",
                Email = "integration5@test5.com",
            };
            var operation = new CreateOperation<User>(adminId, user);

            // Act
            var result = _accountGrain.Create(operation).GetAwaiter().GetResult();

            // Assert
            Assert.True(result.Succeeded);


            var addedUser = _accountGrain.Find(user.Id).GetAwaiter().GetResult();

            var userEntityHistory = addedUser?.EntityHistory;

            Assert.True(userEntityHistory != null && userEntityHistory.Any());
        }

        [Fact]
        public void Verify_History_OnDelete()
        {
            var test3User = _accountGrain.Find(test4UserId).GetAwaiter().GetResult();


            var operation = new DeleteOperation<User>(adminId, test3User.Id);

            // Act
            var result = _accountGrain.Delete(operation).GetAwaiter().GetResult();

            Assert.True(result.Succeeded);

            var updatedUser = _accountGrain.Find(test4UserId).GetAwaiter().GetResult();

            var history = updatedUser.EntityHistory;

            var containsUpdate = history.Any(x => x.OperationType == OperationType.Delete);

            Assert.True(containsUpdate);

            var removedHistory = _context.RemovedEntities;
            var entityIsInRemovedTable = removedHistory.Any(x => x.EntityId == test4UserId);

            Assert.True(entityIsInRemovedTable);
        }

        [Fact]
        public void Verify_History_OnUpdate()
        {
            var test3User = _accountGrain.Find(test4UserId).GetAwaiter().GetResult();

            test3User.LastName = "UPDATEDffefesfsfe USER";

            var operation = new UpdateOperation<User>(adminId, test3User.Id, test3User);

            // Act
            var result = _accountGrain.Update(operation).GetAwaiter().GetResult();

            Assert.True(result.Succeeded);

            var updatedUser = _accountGrain.Find(test4UserId).GetAwaiter().GetResult();

            var history = updatedUser.EntityHistory;

            var containsUpdate = history.Any(x => x.OperationType == OperationType.Update);

            Assert.True(containsUpdate);
        }
    }
}