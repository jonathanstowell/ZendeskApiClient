using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class OrganizationMembershipsResourceTests
    {
        private readonly OrganizationMembershipsResource _resource;

        public OrganizationMembershipsResourceTests()
        {
            IZendeskApiClient client = new DisposableZendeskApiClient<OrganizationMembership>(resource => new OrganizationMembershipsResourceSampleSite(resource));
            _resource = new OrganizationMembershipsResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync();

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var membership = results.ElementAt(i - 1);

                Assert.Equal(i, membership.Id);
                Assert.Equal(i, membership.UserId);
                Assert.Equal(i, membership.OrganizationId);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithPaging_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            });

            var membership = results.First();

            Assert.Equal(2, membership.Id);
            Assert.Equal(2, membership.UserId);
            Assert.Equal(2, membership.OrganizationId);
        }

        [Fact]
        public async Task GetAllAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllAsync(new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }));
        }

        [Fact]
        public async Task GetAllForOrganizationAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllForOrganizationAsync(1);

            Assert.Equal(1, results.Count);

            var membership = results.First();

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
        }

        [Fact]
        public async Task GetAllForOrganizationAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllForOrganizationAsync(int.MinValue));
        }

        [Fact]
        public async Task GetAllForOrganizationAsync_WhenCalledWithPaging_ShouldGetAll()
        {
            var results = await _resource.GetAllForOrganizationAsync(
                1,
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                });

            Assert.Equal(0, results.Count);
        }

        [Fact]
        public async Task GetAllForUserAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllForUserAsync(1);

            Assert.Equal(1, results.Count);

            var membership = results.First();

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
        }

        [Fact]
        public async Task GetAllForUserAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllForUserAsync(int.MinValue));
        }

        [Fact]
        public async Task GetAllForUserAsync_WhenCalledWithPaging_ShouldGetAll()
        {
            var results = await _resource.GetAllForUserAsync(
                1,
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                });

            Assert.Equal(0, results.Count);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldGet()
        {
            var membership = await _resource.GetAsync(1);

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
        }

        [Fact]
        public async Task GetAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetAsync(int.MaxValue);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAsync(int.MinValue));
        }

        [Fact]
        public async Task GetForUserAndOrganizationAsync_WhenCalled_ShouldGet()
        {
            var membership = await _resource.GetForUserAndOrganizationAsync(1, 1);

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
        }

        [Fact]
        public async Task GetForUserAndOrganizationAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetForUserAndOrganizationAsync(int.MaxValue, int.MaxValue);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetForUserAndOrganizationAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetForUserAndOrganizationAsync(int.MinValue, int.MinValue));
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_ShouldCreate()
        {
            var membership = await _resource.CreateAsync(new OrganizationMembership
            {
                Id = 101,
                UserId = 102,
                OrganizationId = 103
            });

            Assert.Equal(101, membership.Id);
            Assert.Equal(102, membership.UserId);
            Assert.Equal(103, membership.OrganizationId);
        }

        [Fact]
        public async Task CreateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new OrganizationMembership
            {
               Id = int.MinValue
            }));
        }

        [Fact]
        public async Task PostForUserAsync_WhenCalled_ShouldCreate()
        {
            var membership = await _resource.PostForUserAsync(new OrganizationMembership
            {
                Id = 101,
                UserId = 102,
                OrganizationId = 103
            }, 102);

            Assert.Equal(101, membership.Id);
            Assert.Equal(102, membership.UserId);
            Assert.Equal(103, membership.OrganizationId);
        }

        [Fact]
        public async Task PostForUserAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.PostForUserAsync(new OrganizationMembership
            {
                Id = int.MinValue
            }, 102));
        }

        [Fact]
        public async Task CreateAsync_WhenCalledWithMany_ShouldCreate()
        {
            var status = await _resource.CreateAsync(new List<OrganizationMembership>
            {
                new OrganizationMembership
                {
                    Id = 101,
                    UserId = 102,
                    OrganizationId = 103
                },
                new OrganizationMembership
                {
                    Id = 102,
                    UserId = 103,
                    OrganizationId = 104
                }
            });

            Assert.Equal(2, status.Total);
        }

        [Fact]
        public async Task CreateAsync_WithManyWhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new List<OrganizationMembership>
            {
                new OrganizationMembership
                {
                    Id = int.MinValue
                }
            }));
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ShouldDelete()
        {
            await _resource.DeleteAsync(1);
        }

        [Fact]
        public async Task DeleteAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAsync(int.MinValue));
        }

        [Fact]
        public async Task DeleteAsync_WhenCalledWithUserId_ShouldDelete()
        {
            await _resource.DeleteAsync(1, 1);
        }

        [Fact]
        public async Task DeleteAsync_WithUserIdWhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAsync(int.MinValue, int.MinValue));
        }
    }
}
