public abstract class WindowParam
{

}

public class LoadingParam : WindowParam
{
    public int SceneIndex = 0;
    public WindowID NextWindow = WindowID.None;
    public WindowParam Param = null;
}