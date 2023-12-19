using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JP_ND_FinalProject.Scenes
{
    // Represents an abstract game scene that inherits from DrawableGameComponent
    public abstract class GameScene : DrawableGameComponent
    {
        // List to store game components that belong to this scene
        public List<GameComponent> Components { get; set; }

        // Hides the scene by setting visibility and enabling state to false
        public virtual void hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }

        // Shows the scene by setting visibility and enabling state to true
        public virtual void show()
        {
            this.Visible = true;
            this.Enabled = true;
        }

        // Constructor for the GameScene class, initializes the Components list and hides the scene by default
        public GameScene(Game game) : base(game)
        {
            Components = new List<GameComponent>();
            hide(); // Hides the scene by default when constructed
        }

        // Overrides the Update method to update each enabled component in the Components list
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in Components)
            {
                // Updates the component if it is enabled
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }
            base.Update(gameTime); // Calls the base class's Update method
        }

        // Overrides the Draw method to draw each visible drawable component in the Components list
        public override void Draw(GameTime gameTime)
        {
            foreach (DrawableGameComponent component in Components)
            {
                // Draws the component if it is visible
                if (component.Visible)
                {
                    component.Draw(gameTime);
                }
            }
            base.Draw(gameTime); // Calls the base class's Draw method
        }
    }
}
