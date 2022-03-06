using System.Reflection;
using DddInPractice.Domain.Common;

namespace DddInPractice.Data.Common;

public static class DomainEventDispatcher
{
    private static List<Type>? _handlers;

    public static void Init()
    {
        _handlers = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<>)))
            .ToList();
    }

    public static void Dispatch(IDomainEvent domainEvent)
    {
        if (_handlers is null)
            return;

        foreach (Type handlerType in _handlers)
        {
            bool canHandleEvent = handlerType.GetInterfaces()
                .Any(x => x.IsGenericType
                          && x.GetGenericTypeDefinition() == typeof(IHandler<>)
                          && x.GenericTypeArguments[0] == domainEvent.GetType());

            if (canHandleEvent)
            {
                dynamic? handler = Activator.CreateInstance(handlerType);
                if (handler is not null)
                    handler.Handle((dynamic)domainEvent);
            }
        }
    }
}
