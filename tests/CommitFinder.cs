﻿using NUnit.Framework;
using System;
using clio;
using System.Linq;

namespace clio.Tests
{
	[TestFixture]
	public class CommitFinderTests
	{
		[Test]
		public void CommitFinder_ParseInvalidPath_ReturnsEmpty ()
		{
			var commits = CommitFinder.Parse ("/not/a/path", "master");
			Assert.Zero (commits.Count ());
		}

		[Test]
		public void CommitFinder_ParseInvalidBranch_ReturnsEmpty ()
		{
			var commits = CommitFinder.Parse (TestDataLocator.GetPath (), "a-non-existant-branch");
			Assert.Zero (commits.Count ());
		}

		[Test]
		public void CommitFinder_Parse_ReturnsEntries ()
		{
			var commits = CommitFinder.Parse (TestDataLocator.GetPath (), "master");
			int count = commits.Count ();
			Assert.NotZero (commits.Count ());
			foreach (var commit in commits)
			{
				Assert.IsNotNull (commit.Hash);
				Assert.IsNotNull (commit.Title);
				Assert.IsNotNull (commit.Description);
			}
		}

		[Test]
		public void CommitFinder_ParseSingle_ReturnsEntry ()
		{
			var commit = CommitFinder.ParseSingle (TestDataLocator.GetPath (), "98fff3172956031c4443574d0f6c9d1e204893ae");
			Assert.IsTrue (commit.HasValue);
		}

		[Test]
		public void CommitFinder_ParseInvalidHash_ReturnsEmpty ()
		{
			var commit = CommitFinder.ParseSingle (TestDataLocator.GetPath (), "NOT_A_HASH");
			Assert.IsFalse (commit.HasValue);
		}
	}
}