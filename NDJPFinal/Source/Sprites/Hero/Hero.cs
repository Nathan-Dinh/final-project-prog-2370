/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NDJPFinal.Source.Global;
using System.Collections.Generic;
using System.Linq;


namespace NDJPFinal.Source.Sprites.Hero
{
    public class Hero : Sprite
    {
        #region Sprites
        public Bullet Bullet;
        public Rocket Rocket;
        #endregion

        #region Sound Effects
        public SoundEffect DamageSoundEffect;
        #endregion

        #region Animation
        private Texture2D _currentTexture;
        private Texture2D _heroTexture;
        private Texture2D _deathTexture;
        #endregion

        #region Properties
        // List to store rectangles representing frames for the hero's regular animations
        private List<Rectangle> _heroAnimationFrames;

        // List to store rectangles representing frames for the hero's death animation
        private List<Rectangle> _deathAnimationFrames;

        // List to store the current animation frames (either hero or death animations)
        private List<Rectangle> _currentAnimationFrames;

        // Keeps track of the current frame of the sprite animation
        private int _spriteFrameTracker;

        // Represents the status or health of the hero
        public float HeroStatus;

        // Variable to track time or animation progression
        private float _time;

        // Represents the width of the texture for the hero's regular animations
        public int TextureWidth;

        // Represents the height of the texture for the hero's regular animations
        public int TextureHeight;

        // Represents the width of the texture for the hero's death animation
        public int DeathAnimationTextureWidth;

        // Represents the height of the texture for the hero's death animation
        public int DeathAnimationTextureHeight;

        // Indicates whether the hero is dead or alive
        public bool IsDead = false;

        // Property to get the boundary of the sprite (used for collision detection or positioning)
        public Rectangle SpriteBoundry
        {
            get
            {
                // Returns a rectangle representing the boundary of the sprite based on its position and dimensions
                return new Rectangle((int)Position.X, (int)Position.Y, TextureWidth, TextureHeight);
            }
        }
        #endregion

        #region Inputes
        float LastSpacebarPressTime;
        #endregion

        public Hero(Texture2D texture, Texture2D deathAnimation, float layer, int frames, int secondaryFrames) : base(texture, layer)
        {
            // Calculate dimensions of frames for hero's regular animations
            TextureWidth = texture.Width / frames;
            TextureHeight = texture.Height;

            // Calculate dimensions of frames for hero's death animation
            DeathAnimationTextureWidth = deathAnimation.Width / secondaryFrames;
            DeathAnimationTextureHeight = deathAnimation.Height / secondaryFrames;

            // Set initial hero status
            HeroStatus = 1f;

            // Assign textures to corresponding fields
            _heroTexture = texture;
            _deathTexture = deathAnimation;

            // Initialize lists to store frames for hero's regular and death animations
            _heroAnimationFrames = new List<Rectangle>();
            _deathAnimationFrames = new List<Rectangle>();

            // Create rectangles for each frame of hero's regular animation
            for (int x = 0; x < frames; x++)
            {
                _heroAnimationFrames.Add(new Rectangle(x * TextureWidth, 0, TextureWidth, TextureHeight));
            }

            // Create rectangles for each frame of hero's death animation
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    _deathAnimationFrames.Add(new Rectangle(c * DeathAnimationTextureWidth, r * DeathAnimationTextureHeight, DeathAnimationTextureWidth, DeathAnimationTextureHeight));
                }
            }

            // Set initial current animation frames and texture to hero's regular animation frames and texture
            _currentAnimationFrames = _heroAnimationFrames;
            _currentTexture = _heroTexture;
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            // Update time for game logic
            _time = (float)gametime.TotalGameTime.TotalSeconds;

            // Store the current keyboard state and the previous keyboard state
            PreviousKey = CurrentKey;
            CurrentKey = Keyboard.GetState();

            // Check if Spacebar is pressed and add a bullet sprite
            if (CurrentKey.IsKeyDown(Keys.Space) && gametime.TotalGameTime.TotalSeconds - LastSpacebarPressTime > 0.50)
            {
                AddBullet(sprites);
                LastSpacebarPressTime = (float)gametime.TotalGameTime.TotalSeconds;
            }

            // Manage animation frames
            AnimationTracker(gametime);

            // Call the base class's Update method to handle additional updates
            base.Update(gametime, sprites);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_currentTexture, Position, _currentAnimationFrames[_spriteFrameTracker], Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, _layer);
        }

        private void AnimationTracker(GameTime gametime)
        {
            if (HeroStatus <= 0)
            {
                // Check if the hero's status is zero or negative (indicating the hero is dead or dying)
                if (!IsDead)
                {
                    // If the hero is not already marked as dead, set death animation and mark as dead
                    SetDeathAnimation();
                    IsDead = true;
                }

                // If the time elapsed since the hero's death animation started is less than 4 seconds
                if (gametime.TotalGameTime.Seconds - _time < 4)
                {
                    // Check if the current frame index is the last frame in the death animation frames
                    if (_spriteFrameTracker >= _currentAnimationFrames.Count() - 1)
                    {
                        // Mark the hero for removal if it has reached the end of the death animation frames
                        this.IsRemoved = true;
                    }
                    else
                    {
                        // Move to the next frame of the death animation if not at the end of the animation frames
                        _spriteFrameTracker += 1;
                    }
                }
            }
            else if (_spriteFrameTracker != _currentAnimationFrames.Count() - 1)
            {
                // If the hero is not dead and the current frame is not the last frame of the current animation frames, reset the frame index
                _spriteFrameTracker = 0;
            }
            else
            {
                // If the hero is not dead and the current frame is the last frame of the current animation frames, move to the next frame
                _spriteFrameTracker += 1;
            }

            // Method to set the current animation frames and texture to the death animation frames and texture

        }

        private void SetDeathAnimation()
        {
            _currentAnimationFrames = _deathAnimationFrames;
            _currentTexture = _deathTexture;
        }

        private void AddBullet(List<Sprite> sprites)
        {
            // Create a new bullet based on the cloned Bullet object
            var bullet = Bullet.Clone() as Bullet;

            // Set bullet position relative to the hero's position
            bullet.Position = Position + new Vector2(6, 0);

            // Set bullet's linear velocity and lifespan
            bullet.LinearVelocity = LinearVelocity * 2;
            bullet.LifeSpan = 2f;

            // Set the bullet's parent to the current hero instance
            bullet.Parent = this;

            // Increment the ammo count in battle report statistics
            BattleReportStats.AmmoShot++;

            // Add the bullet to the list of sprites in the game
            sprites.Add(bullet);

            // Play the sound effect for bullet firing
            DamageSoundEffect.Play();
        }
    }
}
