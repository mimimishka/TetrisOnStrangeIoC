using strange.extensions.signal.impl;

namespace Tetris
{
    public class AppStartSignal : Signal { }
    public class NestTouchSignal : Signal<NestDescriptor> { }
    public class GameStartedSignal : Signal { }
    public class NestDropSignal : Signal { }
    public class NestsOverSignal : Signal { }
    public class ValidateFieldSignal : Signal { }
    public class GameOverSignal : Signal { }
    public class RestartSignal : Signal { }
}