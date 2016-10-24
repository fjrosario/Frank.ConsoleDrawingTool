using Huge.DrawingTool.Domain.Entities;
using ExecutionContext = System.Threading.ExecutionContext;

namespace Huge.DrawingTool.Domain.Tests.Factories
{
    public static class CanvasFactory
    {
        public static Entities.ExecutionContext CreatExecutionContextWithCanvas()
        {
            var ec = new Entities.ExecutionContext();
            ec.Canvas = new Entities.Canvas(20,20);
            return ec;
        }
    }
}
