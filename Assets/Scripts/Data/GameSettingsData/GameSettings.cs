using System;

[Serializable]
public class GameSettings
{
    public int resolutionWidth = 1920;
    public int resolutionHeight = 1080;
    public float volume = -40f;
    public int qualityLevel = 3;
    public bool isFullScreen = true;
}