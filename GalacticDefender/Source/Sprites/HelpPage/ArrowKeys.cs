/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace NDJPFinal.Source.Sprites.HelpPage
{
    public class ArrowKeys : Sprite
    {
        #region 2DTexture
        // Texture for the up arrow
        private Texture2D _textureUPArrow;

        // Texture for the down arrow
        private Texture2D _textureDownArrow;

        // Texture for the left arrow
        private Texture2D _textureLeftArrow;

        // Texture for the right arrow
        private Texture2D _textureRightArrow;
        #endregion

        #region List of Frames
        // Lists to store rectangles representing frames for each arrow direction's animation
        private List<Rectangle> _upArrowFrames;
        private List<Rectangle> _downArrowFrames;
        private List<Rectangle> _leftArrowFrames;
        private List<Rectangle> _rightArrowFrames;
        #endregion

        // Width of the individual frame for each arrow direction
        public int TextureWidth;
        // Height of the individual frame for each arrow direction
        public int TextureHeight;


        #region Frame Tracker
        // Trackers to keep the current frame index for each arrow direction's animation
        private int _upArrowFramesTracker;
        private int _downArrowFramesTracker;
        private int _rightArrowFramesTracker;
        private int _leftArrowFramesTracker;
        #endregion
        public ArrowKeys(Texture2D textureUpArrow,
            Texture2D textureDownArrow,
            Texture2D textureRightArrow,
            Texture2D textureLeftArrow, float layer) : base(textureUpArrow, layer)
        {
            // Assign textures for each arrow direction (up, down, left, right)
            this._textureUPArrow = textureUpArrow;
            this._textureDownArrow = textureDownArrow;
            this._textureLeftArrow = textureLeftArrow;
            this._textureRightArrow = textureRightArrow;

            // Calculate dimensions of frames for each arrow direction based on the up arrow texture
            this.TextureWidth = _textureUPArrow.Width / 2; // Assuming the texture is divided into 2 frames horizontally
            this.TextureHeight = _textureUPArrow.Height;

            // Initialize lists to store frames for each arrow direction
            this._upArrowFrames = new List<Rectangle>();
            this._downArrowFrames = new List<Rectangle>();
            this._leftArrowFrames = new List<Rectangle>();
            this._rightArrowFrames = new List<Rectangle>();

            // Initialize arrow frames trackers to 0 (assuming they represent the current frame index)
            _upArrowFramesTracker = 0;
            _downArrowFramesTracker = 0;
            _rightArrowFramesTracker = 0;
            _leftArrowFramesTracker = 0;

            // Create rectangles for each frame of each arrow direction's animation
            for (int i = 0; i < 2; i++) // Assuming 2 frames for each arrow direction
            {
                _upArrowFrames.Add(new Rectangle(TextureWidth * i, 0, TextureWidth, TextureHeight));
                _downArrowFrames.Add(new Rectangle(TextureWidth * i, 0, TextureWidth, TextureHeight));
                _leftArrowFrames.Add(new Rectangle(TextureWidth * i, 0, TextureWidth, TextureHeight));
                _rightArrowFrames.Add(new Rectangle(TextureWidth * i, 0, TextureWidth, TextureHeight));
            }
        }


        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            // Update the PreviousKey and CurrentKey states to store the keyboard input states
            this.PreviousKey = this.CurrentKey;
            this.CurrentKey = Keyboard.GetState();

            // Check if the left arrow key is pressed; if yes, set the left arrow frames tracker to 1, otherwise set it to 0
            if (this.CurrentKey.IsKeyDown(Keys.Left))
            {
                _leftArrowFramesTracker = 1;
            }
            else
            {
                _leftArrowFramesTracker = 0;
            }

            // Check if the right arrow key is pressed; if yes, set the right arrow frames tracker to 1, otherwise set it to 0
            if (this.CurrentKey.IsKeyDown(Keys.Right))
            {
                _rightArrowFramesTracker = 1;
            }
            else
            {
                _rightArrowFramesTracker = 0;
            }

            // Check if the down arrow key is pressed; if yes, set the down arrow frames tracker to 1, otherwise set it to 0
            if (this.CurrentKey.IsKeyDown(Keys.Down))
            {
                _downArrowFramesTracker = 1;
            }
            else
            {
                _downArrowFramesTracker = 0;
            }

            // Check if the up arrow key is pressed; if yes, set the up arrow frames tracker to 1, otherwise set it to 0
            if (this.CurrentKey.IsKeyDown(Keys.Up))
            {
                _upArrowFramesTracker = 1;
            }
            else
            {
                _upArrowFramesTracker = 0;
            }

            // Call the base class's Update method to perform additional updates
            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureUPArrow, Position - new Vector2(0, 80), _upArrowFrames[_upArrowFramesTracker], Color.White);
            spriteBatch.Draw(_textureDownArrow, Position + new Vector2(0, 0), _downArrowFrames[_downArrowFramesTracker], Color.White);
            spriteBatch.Draw(_textureLeftArrow, Position - new Vector2(80, 0), _leftArrowFrames[_leftArrowFramesTracker], Color.White);
            spriteBatch.Draw(_textureRightArrow, Position + new Vector2(80, 0), _rightArrowFrames[_rightArrowFramesTracker], Color.White);
        }
    }
}
