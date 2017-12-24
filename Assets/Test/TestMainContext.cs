using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.command.impl;
using strange.extensions.command.api;
using strange.extensions.signal.impl;

namespace Test
{
    public class StartSignal : Signal { }
    public class ClickSignal : Signal { }

    public class TestMainContext : MVCSContext
    {
        public TestMainContext(MonoBehaviour view) : base(view) { }
        public TestMainContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags) { }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }
        protected override void mapBindings()
        {
            base.mapBindings();
            mediationBinder.Bind<TestView>().To<TestMediator>();
            injectionBinder.Bind<ClickSignal>().ToSingleton();
            commandBinder.Bind<ClickSignal>().To<ButtonClickCommand>();
            commandBinder.Bind<StartSignal>().To<TestStartCommand>();
        }
        public override void Launch()
        {
            base.Launch();
            var signal = injectionBinder.GetInstance<StartSignal>();
            signal.Dispatch();
        }
    }
}
