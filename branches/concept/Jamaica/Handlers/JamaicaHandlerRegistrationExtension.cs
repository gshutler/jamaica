using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenRasta.Configuration.Fluent;
using OpenRasta.Diagnostics;
using OpenRasta.DI;

namespace Jamaica.Handlers
{
    public static class JamaicaHandlerRegistrationExtension
    {
        public static void JamaicanHandlerRegistration(this IUses anchor)
        {
            var log = anchor.Resolver.Resolve<ILogger>();

            using (log.Operation(typeof(JamaicaHandlerRegistrationExtension), "Handler registration convention"))
            foreach (var registionMethod in HandlerRegistionMethods(log))
            {
                log.WriteInfo("    - Registering handler");
                registionMethod.Invoke(null, new object[] {});
            }
        }

        private static IEnumerable<MethodInfo> HandlerRegistionMethods(ILogger log)
        {
            foreach (var handler in Handlers(log))
            {
                var registrationMethod = RegistrationMethod(handler);

                if (registrationMethod == null)
                {
                    log.WriteInfo("    - No registration method");
                    continue;
                }

                log.WriteInfo("    - Registration method found");
                yield return registrationMethod;
            }
        }

        private static MethodInfo RegistrationMethod(Type handlerType)
        {
            return handlerType.GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(method => method.Name == "Registration")
                .SingleOrDefault();
        }

        private static IEnumerable<Type> Handlers(ILogger log)
        {
            var handlers = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                           from type in assembly.GetTypes()
                           where typeof(IHandler).IsAssignableFrom(type)
                           where !type.IsAbstract
                           where type.IsPublic
                           select type;

            log.WriteInfo("Found {0} handlers", handlers.Count());

            var index = 0;
            foreach (var handler in handlers)
            {
                log.WriteInfo("[{0}] {1}", index++, handler);
                yield return handler;
            }
        }
    }
}