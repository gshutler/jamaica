using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rhino.Mocks;

namespace Jamaica.Test
{
    public class FakeManager
    {
        readonly IDictionary<Type, object> fakes = new Dictionary<Type, object>();
        readonly IDictionary<Type, object> injectedObjects = new Dictionary<Type, object>();

        public T Fake<T>()
        {
            return (T) Fake(typeof(T));
        }

        public object Fake(Type fakeType)
        {
            if (!fakes.ContainsKey(fakeType))
            {
                fakes.Add(fakeType, MockRepository.GenerateStub(fakeType));
            }

            return fakes[fakeType];
        }

        public T InjectedObject<T>()
        {
            var objectType = typeof(T);

            if (!injectedObjects.ContainsKey(objectType))
            {
                injectedObjects.Add(objectType, GenerateInjectedObject<T>());
            }

            return (T) injectedObjects[objectType];
        }

        private T GenerateInjectedObject<T>()
        {
            var constructor = GreediestConstructor<T>();
            var constructorParameters = new List<object>();

            foreach (var parameterInfo in constructor.GetParameters())
            {
                constructorParameters.Add(Fake(parameterInfo.ParameterType));
            }

            return (T) constructor.Invoke(constructorParameters.ToArray());
        }

        private static ConstructorInfo GreediestConstructor<T>()
        {
            return typeof(T).GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .First();
        }
    }
}