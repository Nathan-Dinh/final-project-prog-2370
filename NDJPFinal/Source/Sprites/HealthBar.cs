using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NDJPFinal.Source.Sprites
{
    internal class HealthBar : Sprite
    {
        private Texture2D _firstLayer;
        private Texture2D _secondLayer;
        private Rectangle _healthBarFrame;
        public float HealthBarStatus;
        private float _time;

        public HealthBar(Texture2D texture, Texture2D secondLayer, float layer) : base(texture, layer)
        {
            this._firstLayer = texture;
            this._secondLayer = secondLayer;
            this.HealthBarStatus = 1;
            this._healthBarFrame = new Rectangle(0, 0, (int)(_secondLayer.Width * HealthBarStatus), _secondLayer.Height);
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            this._healthBarFrame = new Rectangle(0, 0, (int)(_secondLayer.Width * HealthBarStatus), _secondLayer.Height);
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
