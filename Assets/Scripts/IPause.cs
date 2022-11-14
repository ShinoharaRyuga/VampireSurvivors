
/// <summary>一時停止と再開を行うインターフェイス</summary>
public interface IPause
{
    /// <summary>一時停止 </summary>
    void Pause();

    /// <summary>再開 </summary>
    void Restart();
}