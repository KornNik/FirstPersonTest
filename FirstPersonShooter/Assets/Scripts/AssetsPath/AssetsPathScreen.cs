using System.Collections.Generic;


namespace ExampleTemplate
{
    public sealed class AssetsPathScreen
    {
        #region PrivateData

        public struct ScreenPath
        {
            public string Screen;
            public Dictionary<ScreenElementType, string> Elements;
        }

        #endregion


        #region Fields

        public static readonly Dictionary<ScreenType, ScreenPath> Screens = new Dictionary<ScreenType, ScreenPath>
        {
            {
                ScreenType.MainMenu, new ScreenPath
                {
                    Screen = "GUI/Screen/MainMenu/GUI_Screen_MainMenu",
                    Elements = new Dictionary<ScreenElementType, string>
                    {
                          { ScreenElementType.None, "" }
                    }
                }
            },
            {
                ScreenType.GameMenu, new ScreenPath
                {
                    Screen = "GUI/Screen/GameMenu/GUI_Screen_GameMenu",
                    Elements = new Dictionary<ScreenElementType, string>()
                }
            },
            {
                ScreenType.Settings, new ScreenPath
                {
                    Screen = "GUI/Screen/SettingsMenu/GUI_Screen_SetingsMenu",
                    Elements = new Dictionary<ScreenElementType, string>()
                }
            },
            {
                ScreenType.VideoSettings,new ScreenPath
                {
                    Screen = "GUI/Screen/VideoSettingsMenu/GUI_Screen_VideoSettingsMenu",
                    Elements = new Dictionary<ScreenElementType, string>()
                    {
                        {ScreenElementType.SliderSettings, "GUI/Elements/GUI_Elements_SliderSettings" }
                    }
                }
            },
        };

        #endregion
    }
}
