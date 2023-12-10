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
    public class Spacebar : Sprite
    {
        // List to store rectangles representing frames for the sprite's animation
        private List<Rectangle> _frames;

        // Width of an individual frame of the sprite's animation
        private int _frameWidth;

        // Height of an individual frame of the sprite's animation
        private int _frameHeight;

        // Keeps track of the current frame of the sprite's animation
        private int _frameTracker;

        public Spacebar(Texture2D texture, float layer) : base(texture, layer)
        {
            // Assign the provided texture to the private _texture field
            this._texture = texture;

            // Calculate the width of an individual frame based on half of the texture's width
            this._frameWidth = this._texture.Width / 2; // Assuming the texture is divided into 2 frames horizontally

            // Assign the height of an individual frame to the texture's height
            this._frameHeight = this._texture.Height;

            // Initialize the FrameTracker to start from the first frame (frame index 0)
            this._frameTracker = 0;

            // Initialize the list to store rectangles representing frames of the sprite's animation
            _frames = new List<Rectangle>();

            // Create rectangles for each frame of the sprite's animation (assuming 2 frames)
            for (int i = 0; i < 2; i++)
            {
                _frames.Add(new Rectangle(_frameWidth * i, 0, _frameWidth, _frameHeight));
            }
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            // Store the CurrentKey state as the PreviousKey state for the next update
            this.PreviousKey = this.CurrentKey;

            // Get the current state of the keyboard
            this.CurrentKey = Keyboard.GetState();

            // Check if the Space key is currently pressed
            if (this.CurrentKey.IsKeyDown(Keys.Space))
            {
                // If the Space key is pressed, set the FrameTracker to 1 (indicating a specific frame or action)
                _frameTracker = 1;
            }
            else
            {
                // If the Space key is not pressed, set the FrameTracker to 0 (indicating another frame or default action)
                _frameTracker = 0;
            }

            // Call the base class's Update method to perform additional updates
            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _frames[_frameTracker],Color.White);
        }
    }
}
