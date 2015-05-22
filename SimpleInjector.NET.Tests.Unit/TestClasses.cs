﻿namespace SimpleInjector.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public interface ILogger
    {
        void Log(string message);
    }

    public interface INonGenericService
    {
        void DoSomething();
    }

    public interface IStruct<T> where T : struct
    {
    }

    public interface IFoo<T>
    {
    }

    public interface IBar<T>
    {
    }

    public interface IInterface<TOne, TTwo, TThree>
    {
    }

    // This is the open generic interface that will be used as service type.
    public interface IService<TA, TB>
    {
    }

    public interface IValidate<T>
    {
        void Validate(T instance);
    }

    public interface IDoStuff<T>
    {
        IService<T, int> Service { get; }
    }

    public interface IProducer<TValue>
    {
    }

    public interface ICommand
    {
        void Execute();
    }

    public interface ICovariant<out T>
    {
    }

    public interface ITimeProvider
    {
        DateTime Now { get; }
    }

    public interface IPlugin
    {
    }

    public interface IGeneric<T>
    {
    }

    public interface IUserRepository
    {
    }

    public class RealTimeProvider : ITimeProvider
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }

    public class FakeTimeProvider : ITimeProvider
    {
        public DateTime Now { get; set; }
    }

    public class SqlUserRepository : IUserRepository
    {
    }

    public class InMemoryUserRepository : IUserRepository
    {
    }

    public class PluginDependantUserRepository : IUserRepository
    {
        public PluginDependantUserRepository(IPlugin plugin)
        {
        }
    }

    public abstract class UserServiceBase
    {
        protected UserServiceBase(IUserRepository repository)
        {
            this.Repository = repository;
        }

        public IUserRepository Repository { get; private set; }
    }

    public class RealUserService : UserServiceBase
    {
        public RealUserService(IUserRepository repository)
            : base(repository)
        {
        }
    }

    public class FakeUserService : UserServiceBase
    {
        public FakeUserService(IUserRepository repository)
            : base(repository)
        {
        }
    }

    public class UserController
    {
        public UserController(UserServiceBase userService)
        {
            this.UserService = userService;
        }

        public int UserKarmaOffset { get; set; }

        public UserServiceBase UserService { get; private set; }
    }

    public class ConcreteTypeWithConcreteTypeConstructorArgument
    {
        public ConcreteTypeWithConcreteTypeConstructorArgument(RealUserService userService)
        {
        }
    }

    public class ConcreteTypeWithMultiplePublicConstructors
    {
        public ConcreteTypeWithMultiplePublicConstructors()
        {
        }

        public ConcreteTypeWithMultiplePublicConstructors(IUserRepository userRepository)
        {
        }
    }

    public class GenericType<T> : IGeneric<T>
    {
        public GenericType()
        {
        }
    }

    public class PluginImpl : IPlugin
    {
    }

    public class PluginImpl2 : IPlugin
    {
    }

    public class PluginDecorator : IPlugin
    {
        public PluginDecorator(IPlugin decoratee)
        {
            this.Decoratee = decoratee;
        }

        public IPlugin Decoratee { get; private set; }
    }

    public class PluginDecorator<T> : IPlugin
    {
        public PluginDecorator(IPlugin decoratee)
        {
            this.Decoratee = decoratee;
        }

        public IPlugin Decoratee { get; private set; }
    }

    public class PluginDecoratorWithDependencyOfType<TDependency> : IPlugin
    {
        public PluginDecoratorWithDependencyOfType(TDependency dependency, IPlugin decoratee)
        {
            this.Decoratee = decoratee;
        }

        public IPlugin Decoratee { get; private set; }
    }

    public class PluginWithPropertyDependencyOfType<TDependency> : IPlugin
    {
        public TDependency Dependency { get; set; }
    }

    public class PluginWithDependencyOfType<TDependency> : IPlugin
    {
        public PluginWithDependencyOfType(TDependency dependency)
        {
        }
    }

    public class PluginManager
    {
        public PluginManager(IEnumerable<IPlugin> plugins)
        {
            this.Plugins = plugins.ToArray();
        }

        public IPlugin[] Plugins { get; private set; }
    }

    public class ConcreteTypeWithValueTypeConstructorArgument
    {
        public ConcreteTypeWithValueTypeConstructorArgument(int intParam)
        {
        }
    }

    public class ConcreteTypeWithStringConstructorArgument
    {
        public ConcreteTypeWithStringConstructorArgument(string stringParam)
        {
        }
    }

    public class ServiceWithUnregisteredDependencies
    {
        public ServiceWithUnregisteredDependencies(IDisposable a, IComparable b)
        {
        }
    }

    public class CovariantImplementation<T> : ICovariant<T>
    {
    }

    public class Consumer
    {
        public Consumer(Dep1 first, Dep2 second)
        {
        }
    }

    public class Dep1
    {
        public Dep1(FirstSub c, SecondSub d, ThirdSub e)
        {
        }
    }

    public class Dep2
    {
        public Dep2(FirstSub c, SecondSub d)
        {
        }
    }

    public class FirstSub
    {
    }

    public class SecondSub
    {
    }

    public class ThirdSub
    {
    }

    public class ConcreteCommand : ICommand
    {
        public void Execute()
        {
        }
    }

    public sealed class NullLogger : ILogger
    {
        public void Log(string message)
        {
        }
    }

    public sealed class NullValidator<T> : IValidate<T>
    {
        public void Validate(T instance)
        {
            // Do nothing.
        }
    }

    public sealed class ValidatorWithUnusedTypeArgument<T, TUnused> : IValidate<T>
    {
        public void Validate(T instance)
        {
            // Do nothing.
        }
    }

    public struct StructCommand
    {
    }
    
    public class MonoDictionary<T> : Dictionary<T, T>
    {
    }

    public class SneakyMonoDictionary<T, TUnused> : Dictionary<T, T>
    {
    }

    // Note: This class deliberately implements a second IProducer. This will verify whether the code can
    // handle types with multiple versions of the same interface.
    public class NullableProducer<T> : IProducer<T?>, IProducer<IValidate<T>>, IProducer<double>
        where T : struct
    {
    }

    public sealed class ServiceImpl<TA, TB> : IService<TA, TB>
    {
    }

    public sealed class ServiceImplWithMultipleCtors<TA, TB> : IService<TA, TB>
    {
        public ServiceImplWithMultipleCtors()
        {
        }

        public ServiceImplWithMultipleCtors(int x)
        {
        }
    }

    public sealed class ServiceImplWithDependency<TA, TB> : IService<TA, TB>
    {
        public ServiceImplWithDependency(IProducer<int> producer)
        {
        }
    }

    // The type constraint will prevent the type from being created when the arguments are ordered
    // incorrectly.
    public sealed class ServiceImplWithTypesArgsSwapped<B, A> : IService<A, B>
        where B : struct
        where A : class
    {
    }

    public class Bar
    {
    }

    public class Baz : IBar<Bar>
    {
    }

    public class Foo<T1, T2> : IFoo<T1> where T1 : IBar<T2>
    {
    }

    public class ServiceWhereTInIsTOut<TA, TB> : IService<TA, TB> where TA : TB
    {
    }

    public class ServiceWithDependency<TDependency>
    {
        public ServiceWithDependency(TDependency dependency)
        {
            this.Dependency = dependency;
        }

        public TDependency Dependency { get; private set; }
    }

    public class Implementation<X, TUnused1, TUnused2, Y> : IInterface<X, X, Y>
    {
    }

    public interface IOpenGenericWithPredicate<T>
    {
    }

    public class OpenGenericWithPredicate1<T> : IOpenGenericWithPredicate<T>
    {
    }

    public class OpenGenericWithPredicate2<T> : IOpenGenericWithPredicate<T>
    {
    }

    public class ServiceDecorator : IService<int, object>
    {
        public ServiceDecorator(IService<int, object> decorated)
        {
        }
    }
}