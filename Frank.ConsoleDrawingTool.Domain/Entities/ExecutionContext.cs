namespace Frank.ConsoleDrawingTool.Domain.Entities
{
    /// <summary>
    /// ExecutionContext
    /// 
    /// This class basically encompasses the scope of execution of the program. Setting up a context
    /// rather than pass in individual references to external fields such as the program's canvas
    /// allows to add addition fields/properties without having to change the method signature of
    /// anything taking a context object as a parameter
    /// </summary>
    public class ExecutionContext
    {
        public Canvas Canvas { get; set; }
    }
}
