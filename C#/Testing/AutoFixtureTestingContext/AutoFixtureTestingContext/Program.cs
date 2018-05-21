using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace AutoFixtureTestingContext
{
    class Program
    {
        static void Main()
        {
        }
    }

    /// <summary>
    /// Tenants:
    /// 1. Any testing helper should limit the testing scope to a single class only.
    /// 2. Changes to a constructor of a class should not require editing all tests for that class.
    /// 3. Boilerplate code within the test class should be kept to a minimum.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TestingContext<T> where T : class
    {
        private Fixture fixture;
        private Dictionary<Type, Mock> injectedMocks;
        private Dictionary<Type, object> injectedConcreteClasses;

        public virtual void Setup()
        {
            fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            injectedMocks = new Dictionary<Type, Mock>();
            injectedConcreteClasses = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Generates a mock for a class and injects it into the final fixture
        /// </summary>
        /// <typeparam name="TMockType"></typeparam>
        /// <returns></returns>
        public Mock<TMockType> GetMockFor<TMockType>() where TMockType : class
        {
            var existingMock = injectedMocks.FirstOrDefault(x => x.Key == typeof(TMockType));

            if (existingMock.Key == null)
            {
                var newMock = new Mock<TMockType>();
                existingMock = new KeyValuePair<Type, Mock>(typeof(TMockType), newMock);
                injectedMocks.Add(existingMock.Key, existingMock.Value);
                fixture.Inject(newMock.Object);
            }

            return existingMock.Value as Mock<TMockType>;
        }

        /// <summary>
        /// Injects a concrete class to be used when generating the fixture
        /// </summary>
        /// <typeparam name="TClassType"></typeparam>
        /// <param name="injectedClass"></param>
        public void InjectClassFor<TClassType>(TClassType injectedClass) where TClassType : class
        {
            var existingClass = injectedConcreteClasses.FirstOrDefault(x => x.Key == typeof(TClassType));

            if (existingClass.Key != null)
            {
                throw new Exception($"{injectedClass.GetType().Name} has been injected more than once");
            }

            injectedConcreteClasses.Add(typeof(TClassType), injectedClass);
            fixture.Inject(injectedClass);
        }

        public T ClassUnderTest => fixture.Create<T>();
    }
}