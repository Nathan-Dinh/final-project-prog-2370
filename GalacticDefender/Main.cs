/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
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

        #region Scene 
        public StartScene StartScene;
        public StageOneScene StageOneScene;
        public HelpScene HelpScene;
        public AboutScene AboutScene;
        public BattleReportScene BattleReportScene;
        #endregion

        private SoundEffect _cursorReady;

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
            this.AboutScene = new AboutScene(this);
            this.BattleReportScene = new BattleReportScene(this);

            _cursorReady = this.Content.Load<SoundEffect>("Sound/Final Fantasy VII Sound Effects - Cursor Ready (mp3cut.net) (2)");

            this.Components.Add(BattleReportScene);
            this.Components.Add(StageOneScene);
            this.Components.Add(StartScene);
            this.Components.Add(HelpScene);
            this.Components.Add(AboutScene);
            StartScene.show();
        }

        protected override void Update(GameTime gameTime)
        {
            if (StartScene.Visible)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    int selectedScene = StartScene.getSelectedIndex();
                    HideAllScenes();
                    switch (selectedScene)
                    {
                        case 0:
                            BattleReportStats.ResetBattleReport();
                            StageOneScene.show();
                            break;
                        case 1:
                            AboutScene.show();
                            break;
                        case 2:
                            HelpScene.show();
                            break;
                        case 3:
                            Exit();
                            break;
                    }
                    _cursorReady.Play();
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    HideAllScenes();
                    StartScene.show();
                }else if (StageOneScene.GameResult) //Flag that checks if the there is ar result in the StageOneScene
                {
                    HideAllScenes();
                    StageOneScene.GameResult = false;//Resets the flag for the next game
                    BattleReportScene.show();
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


        //This function resets and all the scenes to keep consistency throughout runtime
        protected void HideAllScenes()
        {
            this.Components.Clear();
            this.StageOneScene = new StageOneScene(this);
            this.StartScene = new StartScene(this);
            this.HelpScene = new HelpScene(this);
            this.AboutScene = new AboutScene(this);
            this.BattleReportScene = new BattleReportScene(this);
            this.Components.Add(BattleReportScene);
            this.Components.Add(StageOneScene);
            this.Components.Add(StartScene);
            this.Components.Add(HelpScene);
            this.Components.Add(AboutScene);

            foreach (var scene in this.Components)
            {
                //Checks if the component is a scene or not
                if (scene is GameScene)
                {
                   var sceneTest = (GameScene) scene;
                   sceneTest.hide();

                }
            }
        }
    }


    //Main class
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