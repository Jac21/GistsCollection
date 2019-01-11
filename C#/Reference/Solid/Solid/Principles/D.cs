using System;
using Solid.Helpers;

namespace Solid.Principles
{
    /// <summary>
    /// D - Dependency inversion principle
    /// High-level modules should not depend on low-level modules. 
    /// Both should depend on abstractions.
    /// Abstractions should not depend on details.Details should depend on abstractions.
    /// </summary>
    public class D
    {
        // Let’s look at an example.
        public class Post
        {
            private readonly S.ErrorLogger errorLogger = new S.ErrorLogger();

            public void CreatePost(Database db, string postMessage)
            {
                try
                {
                    db.Add(postMessage);
                }
                catch (Exception ex)
                {
                    errorLogger.Log(ex.ToString());
                }
            }
        }
    }

    // Observe how we create the ErrorLogger instance from within the Post class.
    // This is a violation of the dependency inversion principle.
    // If we wanted to use a different kind of logger, we would have to 
    // modify the Post class.

    // Let’s fix this by using dependency injection.

    public class PostProper
    {
        private readonly S.ErrorLogger errorLogger;

        public PostProper(S.ErrorLogger injectedLogger)
        {
            errorLogger = injectedLogger;
        }

        public void CreatePost(Database db, string postMessage)
        {
            try
            {
                db.Add(postMessage);
            }
            catch (Exception ex)
            {
                errorLogger.Log(ex.ToString());
            }
        }
    }
}