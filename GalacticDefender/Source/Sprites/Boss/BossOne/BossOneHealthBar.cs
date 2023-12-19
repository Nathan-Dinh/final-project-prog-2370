/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NDJPFinal.Source.Sprites.Boss.BossOne
{
    public class BossOneHealthBar : Sprite
    {
        // Texture for the first layer of the scrolling background
        private Texture2D _firstLayer;

        // Texture for the second layer of the scrolling background
        private Texture2D _secondLayer;

        // Instance of ScrolllingBackground class (assuming a typo in the class name "ScrolllingBackground")
        public ScrolllingBackground Scrollingackgorund;

        // Rectangle representing the health bar frame
        private Rectangle _healthBarFrame;

        // Status value indicating the health bar status
        public float HealthBarStatus;


        public BossOneHealthBar(Texture2D texture, Texture2D secondLayer, ScrolllingBackground scrollling, float layer) : base(texture, layer)
        {
            // Assign the provided texture to both the first and second layers
            _firstLayer = texture;
            _secondLayer = texture;

            // Assign the specified layer value to the private _layer field
            _layer = layer;

            // Set the initial health bar status to full (1.0)
            HealthBarStatus = 1;

            // Assign the provided ScrolllingBackground instance to the Scrollingackgorund field
            // (assuming a typo in the class name "ScrolllingBackground")
            Scrollingackgorund = scrollling;

            // Initialize the health bar frame based on the sprite width and health bar status
            _healthBarFrame = new Rectangle(
                0,                                                  // X-coordinate of the health bar frame (start from the left)
                0,                                                  // Y-coordinate of the health bar frame (start from the top)
                (int)(Scrollingackgorund.SpriteWidth * HealthBarStatus),   // Width of the health bar frame based on sprite width and health status
                20                                                  // Height of the health bar frame (set to 20 pixels)
            );
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            // Update the health bar frame based on the current health status and sprite width
            _healthBarFrame = new Rectangle(
                0,                                                  // X-coordinate of the health bar frame (start from the left)
                0,                                                  // Y-coordinate of the health bar frame (start from the top)
                (int)(Scrollingackgorund.SpriteWidth * HealthBarStatus),   // Width of the health bar frame based on sprite width and health status
                20                                                  // Height of the health bar frame (set to 20 pixels)
            );

            // Call the base class's Update method, possibly to update other functionalities related to this object
            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_secondLayer, Position, _healthBarFrame, Color.Green, _rotation, Origin, _scale, SpriteEffects.None, 0.2f);
            spriteBatch.Draw(_firstLayer, Position, new Rectangle(0, 0, Scrollingackgorund.SpriteWidth, 20), Color.Black, _rotation, Origin, _scale, SpriteEffects.None, 0.1f);
            base.Draw(spriteBatch);
        }

        public void ChangeHealthBarState()
        {
            HealthBarStatus -= 0.05f;
        }

    }
}
