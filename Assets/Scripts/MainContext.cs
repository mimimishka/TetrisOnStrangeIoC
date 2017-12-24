using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.command.api;
using strange.extensions.injector.impl;
using UnityEngine.UI;

namespace Tetris
{
    public class MainContext : MVCSContext
    {
        public MainContext(Root view) : base(view) { }
        public MainContext(Root view, ContextStartupFlags flags) : base(view, flags) { }
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>();
        }
        protected override void mapBindings()
        {
            base.mapBindings();

            InitMonoBindings();
            commandBinder.Bind<AppStartSignal>().
                InSequence().
                To<InitFieldCommand>().
                To<ShowGreetingsCommand>().
                Once();
            injectionBinder.Bind<GameStartedSignal>().ToSingleton();
            commandBinder.Bind<GameStartedSignal>().To<BuildShapesCommand>().To<EnableDetectorsCommand>();
            injectionBinder.Bind<IExecutor>().To<Executor>().ToSingleton();
            injectionBinder.Bind<CellAlphaProcessingManager>().ToSingleton();
            injectionBinder.Bind<MainModel>().ToSingleton();
            injectionBinder.Bind<ShapesGenerator>().ToSingleton();
            injectionBinder.Bind<NestTouchSignal>().ToSingleton();
            commandBinder.Bind<NestTouchSignal>().To<DragStartCommand>();
            injectionBinder.Bind<NestDropSignal>().ToSingleton();
            commandBinder.Bind<NestDropSignal>().InSequence().To<DropCursorCommand>().To<CleanupFieldCommand>().To<ValidateFieldCommand>();
            injectionBinder.Bind<NestsOverSignal>().ToSingleton();
            commandBinder.Bind<NestsOverSignal>().To<BuildShapesCommand>();
            injectionBinder.Bind<GameOverSignal>().ToSingleton();
            commandBinder.Bind<GameOverSignal>().To<GameOverCommand>();
            injectionBinder.Bind<int>().To(10).ToSingleton().ToName(SizeType.FIELD);
            injectionBinder.Bind<TextMover>().ToSingleton();
            injectionBinder.Bind<RestartSignal>().ToSingleton();
            commandBinder.Bind<RestartSignal>().To<RestartCommand>();
            mediationBinder.Bind<RestartView>().To<RestartMediator>();
            mediationBinder.Bind<ProtectorView>().To<ProtectorMediator>();
#if UNITY_EDITOR || UNITY_STANDALONE
            injectionBinder.Bind<ITouch>().To<MouseTouch>().ToSingleton();
#else
            injectionBinder.Bind<ITouch>().To<FingerTouch>().ToSingleton();
#endif
        }
        public override void Launch()
        {
            base.Launch();
            injectionBinder.GetInstance<AppStartSignal>().Dispatch();
        }
        void InitMonoBindings()
        {
            MonoBehaviourBindings monoBinds = GameObject.FindObjectOfType<MonoBehaviourBindings>();
            injectionBinder.Bind<ITextAccessor>().ToValue(new TextAccessor
            {
                Text = monoBinds.StartText
            }).ToName(TextType.START);
            injectionBinder.Bind<ITextAccessor>().ToValue(new TextAccessor
            {
                Text = monoBinds.GameOverText
            }).ToName(TextType.GAMEOVER);
            injectionBinder.Bind<IPositionsSetAccessor>().ToValue(new PositionsSetAccessor
            {
                Container = monoBinds.VerticalPosSet
            }).ToName(MotionType.VERTICAL);
            injectionBinder.Bind<IPositionsSetAccessor>().ToValue(new PositionsSetAccessor
            {
                Container = monoBinds.HorizontalPosSet
            }).ToName(MotionType.HORIZONTAL);
            injectionBinder.Bind<IGrid<CellDescriptor>>().ToValue(new GridAccessor<CellDescriptor>()
            {
                Container = monoBinds.CellsGrid
            });
            foreach(Transform tr in monoBinds.NestsGrid)
            {
                NestDescriptor nest = tr.GetComponent<NestDescriptor>();
                nest.Container = tr;
            }
            injectionBinder.Bind<IGrid<NestDescriptor>>().ToValue(new GridAccessor<NestDescriptor>()
            {
                Container = monoBinds.NestsGrid
            });
            injectionBinder.Bind<Cursor>().ToValue(monoBinds.Cursor);
            injectionBinder.Bind<FillingGenerator>().ToValue(monoBinds.FillGenerator);
            injectionBinder.Bind<ButtonAccessor>().ToValue(new ButtonAccessor
            {
                Button = monoBinds.RestartButton
            });
        }
    }
}