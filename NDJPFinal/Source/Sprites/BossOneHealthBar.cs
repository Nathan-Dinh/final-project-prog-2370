using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Sprites
{
    internal class BossOneHealthBar : Sprite
    {
        private Texture2D _firstLayer;
        private Texture2D _secondLayer;
        public ScrolllingBackground Scrollingackgorund;
        private Rectangle _healthBarFrame;
        public float HealthBarStatus;

        public BossOneHealthBar(Texture2D texture,Texture2D secondLayer,ScrolllingBackground scrollling ,float layer) : base(texture, layer)
        {
            this._firstLayer = texture;
            this._secondLayer = texture;
            this._layer = layer;
            this.HealthBarStatus = 1;
            this.Scrollingackgorund= scrollling;
            this._healthBarFrame = new Rectangle(0,0, (int)(Scrollingackgorund._frameWidth * HealthBarStatus), 20);
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            this._healthBarFrame = new Rectangle(0, 0, (int)(Scrollingackgorund._frameWidth * HealthBarStatus), 20);
            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_secondLayer, Position, _healthBarFrame, Color.Green,_rotation,Origin,_scale, SpriteEffects.None, 0.2f);
            spriteBatch.Draw(_firstLayer, Position, new Rectangle(0, 0, Scrollingackgorund._frameWidth, 20), Color.Black, _rotation, Origin, _scale, SpriteEffects.None, 0.1f);
            base.Draw(spriteBatch);
        }

        public void ChangeHealthBarState()
        {
            HealthBarStatus -= 0.50f;
        }

    }
}
