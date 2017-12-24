using strange.extensions.context.impl;
using strange.extensions.command.impl;
using strange.extensions.injector.api;
using strange.extensions.injector.impl;

namespace Test
{
    public class TestRoot : ContextView
    {
        void Awake()
        {
            context = new TestMainContext(this);
            //.Start();
        }
    }
}
