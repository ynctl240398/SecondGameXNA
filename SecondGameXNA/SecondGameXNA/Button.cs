using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SecondGameXNA
{
    class Button : DrawableGameComponent
    {
        private SpriteBatch sBatch;

        private Rectangle rectanggle;

        private const int SizeButton = 65;

        private Texture2D texture;

        private Point Position;

        public Button(Game game, ref Texture2D texture, Point Position)
            : base(game)
        {
            this.texture = texture;

            this.Position = Position;

            rectanggle = new Rectangle(0, 0, SizeButton, SizeButton);

            sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
        }

        public Rectangle Getbounds()
        {
            return new Rectangle(Position.X, Position.Y, SizeButton, SizeButton);
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }
    }
}
