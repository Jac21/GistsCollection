using Solid.Helpers;

namespace Solid.Principles
{
    /// <summary>
    /// O — Open/closed principle
    /// "software entities (classes, modules, functions, etc.) should be open for extensions, but closed for modification."
    /// </summary>
    public class O
    {
        // Let’s do an example of how to write a piece of code that violates this principle.

        /// <summary>
        /// Implementation violates the open/closed principle in the way this code differs the behavior on the starting letter.
        /// </summary>
        public class Post
        {
            public void CreatePost(Database db, string postMessage)
            {
                if (postMessage.StartsWith("#"))
                {
                    db.AddAsTag(postMessage);
                }
                else
                {
                    db.Add(postMessage);
                }
            }
        }

        // Let’s try to make this code compliant with the open/closed principle by simply using inheritance.

        public class PostProper
        {
            public virtual void CreatePost(Database db, string postMessage)
            {
                db.Add(postMessage);
            }
        }

        /// <summary>
        /// By using inheritance, it is now much easier to create extended behavior to the Post object 
        /// by overriding the CreatePost() method.
        /// </summary>
        public class TagPost : PostProper
        {
            public override void CreatePost(Database db, string postMessage)
            {
                db.AddAsTag(postMessage);
            }
        }
    }
}