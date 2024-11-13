using CarGO_Control.Tools;

public static class MainWindowFactory
{
    public enum WindowType
    {
        Operator,
        Driver
    }

    public static IMainWindow CreateWindow(WindowType windowType)
    {
        switch (windowType)
        {
            case WindowType.Operator:
                return new OperatorMain();
            case WindowType.Driver:
                return new DriverMain();
            default:
                throw new ArgumentException("Invalid window type");
        }
    }
}