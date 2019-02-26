namespace Solid.Principles
{
    /// <summary>
    /// I — Interface segregation principle
    /// No client should be forced to depend on methods it does not use.
    /// </summary>
    public class I
    {
        // Let’s look at an example of how to violate the interface segregation principle.

        public interface IPost
        {
            void CreatePost();
        }

        public interface IPostNew
        {
            void CreatePost();
            void ReadPost();
        }

        // In this example, let’s pretend that I first have an IPost interface 
        // with the signature of a CreatePost() method. 
        // Later on, I modify this interface by adding a new method ReadPost(), 
        // so it becomes like the IPostNew interface.

        // This is where we violate the interface segregation principle.
        // Instead, simply create a new interface.

        public interface IPostCreate
        {
            void CreatePost();
        }

        public interface IPostRead
        {
            void ReadPost();
        }
    }
}