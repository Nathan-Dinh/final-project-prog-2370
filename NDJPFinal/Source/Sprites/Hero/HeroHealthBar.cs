using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NDJPFinal.Source.Sprites.Hero
{
    internal class HeroHealthBar : Sprite
    {
        #region Texture
        private Texture2D _firstLayer;
        private Texture2D _secondLayer;
        #endregion

        #region Properties
        private Rectangle _healthBarFrame;
        private int _defultHealthBarStatus = 1;
        public float HealthBarStatus;
        #endregion

        public HeroHealthBar(Texture2D firstLayer, Texture2D secondLayer, float layer) : base(firstLayer, layer)
        {
            _firstLayer = firstLayer;
            _secondLayer = secondLayer;
            HealthBarStatus = _defultHealthBarStatus;
            _healthBarFrame = new Rectangle(0, 0, (int)(_secondLayer.Width * HealthBarStatus), _secondLayer.Height);
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            _healthBarFrame = new Rectangle(0, 0, (int)(_secondLayer.Width * HealthBarStatus), _secondLayer.Height);
            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_secondLayer, Position + new Vector2(6, 0), _healthBarFrame, Color.White, _rotation, Origin, _scale, SpriteEffects.None, _layer + 0.1f);
            spriteBatch.Draw(_firstLayer, Position, null, Color.White, _rotation, Origin, _scale, SpriteEffects.None, _layer);

            base.Draw(spriteBatch);
        }

        public void ChangeHealthBarState()
        {
            HealthBarStatus -= 0.25f;
        }
    }
}
