using System;
using System.Collections.Generic;
using Solid.Helpers;

namespace Solid.Principles
{
    /// <summary>
    /// L — Liskov substitution principle
    /// In programming, the Liskov substitution principle states that 
    /// if S is a subtype of T, then objects of type T may be replaced 
    /// (or substituted) with objects of type S.
    /// </summary>
    public class L
    {
        // Let’s take a look at an example of how to violate this principle
        public class Post
        {
            public virtual void CreatePost(Database db, string postMessage)
            {
                db.Add(postMessage);
            }
        }

        class TagPost : Post
        {
            public override void CreatePost(Database db, string postMessage)
            {
                db.AddAsTag(postMessage);
            }
        }

        class MentionPost : Post
        {
            public void CreateMentionPost(Database db, string postMessage)
            {
                string user = ParseUser(postMessage);

                db.NotifyUser(user);
                db.OverrideExistingMention(user, postMessage);
                CreatePost(db, postMessage);
            }

            private string ParseUser(string postMessage)
            {
                throw new NotImplementedException();
            }
        }

        public class PostHandler
        {
            private readonly Database database = new Database();

            public void HandleNewPosts()
            {
                List<string> newPosts = database.GetUnhandledPostsMessages();

                foreach (string postMessage in newPosts)
                {
                    Post post;

                    if (postMessage.StartsWith("#"))
                    {
                        post = new TagPost();
                    }
                    else if (postMessage.StartsWith("@"))
                    {
                        post = new MentionPost();
                    }
                    else
                    {
                        post = new Post();
                    }

                    post.CreatePost(database, postMessage);
                }
            }
        }

        // Observe how the call of CreatePost() in the case of a subtype MentionPost 
        // won’t do what it is supposed to do; notify the user and override existing mention.

        // Since the CreatePost() method is not overridden in MentionPost the 
        // CreatePost() call will simply be delegated upwards in the class hierarchy 
        // and call CreatePost() from its parent class.

        public class MentionPostProper : Post
        {
            private readonly Database db = new Database();


            private string ParseUser(string postMessage)
            {
                throw new NotImplementedException();
            }

            public override void CreatePost(Database database, string postMessage)
            {
                string user = ParseUser(postMessage);

                NotifyUser(user);
                OverrideExistingMention(user, postMessage);
                base.CreatePost(database, postMessage);
            }

            private void NotifyUser(string user)
            {
                db.NotifyUser(user);
            }

            private void OverrideExistingMention(string user, string postMessage)
            {
                db.OverrideExistingMention(user, postMessage);
            }
        }
    }
}