using System;

namespace Assets.Script.singleton
{
    internal class CurrentScene
    {
        private static CurrentScene _instance;
        public static CurrentScene Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CurrentScene();
                }
                return _instance;
            }
        }

        public int CurrentSceneIndex { get; set; }
        public int CurrentCherry { get; set; }

        private CurrentScene()
        {
        }
    }
}
