using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NDJPFinal.Source.Global;
using NDJPFinal.Source.Scenes.BattleReport;
using NDJPFinal.Source.Scenes.Menu.GameSetting;
using NDJPFinal.Source.Scenes.Menu.HelpScene;
using NDJPFinal.Source.Scenes.Menu.StartScene;
using NDJPFinal.Source.Scenes.Stages;
using System;

namespace NDJPFinal
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch;

        public StartScene StartScene;
        public StageOneScene StageOneScene;
        public HelpScene HelpScene;
        public GameSettingScene gameSettingScene;
        public BattleReportScene battleReportScene;

        public SoundEffect cursorReady;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.StageOneScene = new StageOneScene(this);
            this.StartScene = new StartScene(this);
            this.HelpScene = new HelpScene(this);
            this.gameSettingScene = new GameSettingScene(this);
            this.battleReportScene = new BattleReportScene(this);

            cursorReady = this.Content.Load<SoundEffect>("Sound/Final Fantasy VII Sound Effects - Cursor Ready (mp3cut.net) (2)");

            this.Components.Add(battleReportScene);
            this.Components.Add(StageOneScene);
            this.Components.Add(StartScene);
            this.Components.Add(HelpScene);
            this.Components.Add(gameSettingScene);
            StartScene.show();
        }

        protected override void Update(GameTime gameTime)
        {
            if (StartScene.Visible)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    int selectedScene = StartScene.getSelectedIndex();
                    hideAllScenes();
                    switch (selectedScene)
                    {
                        case 0:
                            BattleReportStats.ResetBattleReport();
                            StageOneScene.show();
                            break;
                        case 1:
                            gameSettingScene.show();
                            break;
                        case 2:
                            HelpScene.show();
                            break;
                        case 3:
                            Exit();
                            break;
                    }
                    cursorReady.Play();
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    StartScene.show();
                }else if (StageOneScene.GameResult)
                {
                    hideAllScenes();
                    StageOneScene.GameResult = false;
                    battleReportScene.show();
                }
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        public void hideAllScenes()
        {
            this.Components.Clear();
            this.StageOneScene = new StageOneScene(this);
            this.StartScene = new StartScene(this);
            this.HelpScene = new HelpScene(this);
            this.gameSettingScene = new GameSettingScene(this);
            this.battleReportScene = new BattleReportScene(this);
            this.Components.Add(battleReportScene);
            this.Components.Add(StageOneScene);
            this.Components.Add(StartScene);
            this.Components.Add(HelpScene);
            this.Components.Add(gameSettingScene);

            foreach (var scene in this.Components)
            {
                if (scene is GameScene)
                {
                   var sceneTest = (GameScene) scene;
                   sceneTest.hide();

                }
            }
        }
    }

    public static class Program 
    {
        [STAThread]
        static void Main()
        {
            using var game = new NDJPFinal.Main();
            game.Run();

        }
    }

}