using Core.Extensions.Claims;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Tests.Core.Extensions {
	[TestFixture]
	public class ClaimExtensionTests {
#pragma warning disable CS8618 // Non-nullable field '_claimList' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
		private List<Claim> _claimList;
#pragma warning restore CS8618 // Non-nullable field '_claimList' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning disable CS8618 // Non-nullable field '_roles' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
		private System.String[] _roles;
#pragma warning restore CS8618 // Non-nullable field '_roles' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.

		[SetUp]
		public void Setup() {
			this._claimList = new List<Claim>();
			this._roles = new System.String[3] { "Admin", "User", "MasterUser" };
		}

		[Test]
		[TestCase("test@test.com")]
		public void AddEmail(System.String email) {
			this._claimList.AddEmail(email);
			this._claimList.Where(x => x.Type == JwtRegisteredClaimNames.Email && x.Value == email).Should().HaveCount(1);
		}

		[Test]
		[TestCase("testName")]
		public void AddName(System.String name) {
			this._claimList.AddName(name);
			this._claimList.Where(x => x.Type == ClaimTypes.Name && x.Value == name).Should().HaveCount(1);
		}

		[Test]
		[TestCase("testIdentifier")]
		public void AddNameIdentifier(System.String identifier) {
			this._claimList.AddNameIdentifier(identifier);
			this._claimList.Where(x => x.Type == ClaimTypes.NameIdentifier && x.Value == identifier).Should().HaveCount(1);
		}

		[Test]
		public void AddRoles() {
			this._claimList.AddRoles(this._roles);
			this._claimList.Where(x => x.Type == ClaimTypes.Role).Should().HaveCount(3);
		}
	}
}