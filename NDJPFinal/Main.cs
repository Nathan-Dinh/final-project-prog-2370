using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NDJPFinal.Source.Fonts;
using NDJPFinal.Source.Manager;
using NDJPFinal.Source.Scenes.Stages;
using NDJPFinal.Source.Sprites;
using System;
using System.Collections.Generic;

namespace NDJPFinal
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch;

        private List<Sprite> _sprites;

        public StartScene StartScene;
        public StageOneScene StageOneScene;

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
            StartScene = new StartScene(this);
            StageOneScene = new StageOneScene(this);
            this.Components.Add(StageOneScene);
            this.Components.Add(StartScene);
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
                            StageOneScene.show();
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            Exit();
                            break;
                    }
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    StartScene.show();
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