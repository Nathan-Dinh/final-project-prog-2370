/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NDJPFinal.Source.Sprites.Hero
{
    public class HeroHealthBar : Sprite
    {
        #region Texture
        private Texture2D _firstLayer;
        private Texture2D _secondLayer;
        #endregion

        #region Properties
        private Rectangle _healthBarFrame;
        private int _defaultHealthBarStatus;
        public float HealthBarStatus;
        #endregion

        public HeroHealthBar(Texture2D firstLayer, Texture2D secondLayer, float layer) : base(firstLayer, layer)
        {
            // Initialize the first and second layers of the health bar
            _firstLayer = firstLayer;
            _secondLayer = secondLayer;
            _defaultHealthBarStatus = 1;
            // Set the initial health bar status to the default value
            HealthBarStatus = _defaultHealthBarStatus;

            // Create the initial health bar frame based on the current health status
            _healthBarFrame = new Rectangle(0, 0, (int)(_secondLayer.Width * HealthBarStatus), _secondLayer.Height);
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            // Update the health bar frame based on the current health status
            _healthBarFrame = new Rectangle(0, 0, (int)(_secondLayer.Width * HealthBarStatus), _secondLayer.Height);

            // Call the base class's Update method to perform additional updates
            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the second layer of the health bar (represents remaining health) with adjusted dimensions
            spriteBatch.Draw(_secondLayer, Position + new Vector2(6, 0), _healthBarFrame, Color.White, _rotation, Origin, _scale, SpriteEffects.None, _layer + 0.1f);

            // Draw the first layer of the health bar (background) at the specified position and properties
            spriteBatch.Draw(_firstLayer, Position, null, Color.White, _rotation, Origin, _scale, SpriteEffects.None, _layer);

            // Call the base class's Draw method to handle additional drawing operations
            base.Draw(spriteBatch);
        }

        public void ChangeHealthBarState()
        {
            // Reduce the health bar status by a fixed amount when called
            HealthBarStatus -= 0.25f;
        }
    }
}
