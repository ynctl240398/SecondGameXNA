using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace SecondGameXNA
{
    enum DirectionOfMotion
    {
        Left, Rigth, Up, Down,
    }
    class Player : DrawableGameComponent
    {
        private SpriteBatch sBatch;
        private Texture2D PlayerTexture;


        private Point Frame;
        private Point LimitFrame;
        private Point Position;
        private Point FirstPosition;
        private Rectangle rectangle;
        private const int Speed = 65;
        private const int wPlayer = 65, hPlayer = 65;
        private int LastTickCount;
        private const int Timer = 70;
        private DirectionOfMotion directionOfmotion;
        private bool Moving;
        private int a = 0;


        public Player(Game game, ref Texture2D texture)
            : base(game)
        {
            this.PlayerTexture = texture;
            Position = new Point(0, 0);
            FirstPosition = Position;
            Frame = new Point(0, 0);
            LimitFrame = new Point(4, 4);
            LastTickCount = System.Environment.TickCount;
            rectangle = new Rectangle(0, 0, wPlayer, hPlayer);
            sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
        }

        public bool CheckCollision(Rectangle rec)
        {
            Rectangle spriterect = new Rectangle(Position.X, Position.Y, wPlayer, hPlayer);
            return spriterect.Intersects(rec);
        }


        public Rectangle GetBounds()
        {
            return new Rectangle(Position.X, Position.Y, wPlayer, hPlayer);
        }



        private void UpdateStatus()
        {
            if (Moving)
            {
                if (Frame.X < LimitFrame.X - 1)
                {
                    Frame.X += 1;
                }
                else Frame.X = 0;
            }
            else Frame.X = 0;
        }
        private void Move()
        {
            a = 1;
            Thread.Sleep(150);
            
            Moving = true;



            if (directionOfmotion == DirectionOfMotion.Down)
            {
                Position.Y += Speed;
                Frame.Y = 0;
            }
            else if (directionOfmotion == DirectionOfMotion.Up)
            {
                Position.Y -= Speed;
                Frame.Y = 3;
            }
            else if (directionOfmotion == DirectionOfMotion.Left)
            {
                Position.X -= Speed;
                Frame.Y = 1;
            }
            else if (directionOfmotion == DirectionOfMotion.Rigth)
            {
                Position.X += Speed;
                Frame.Y = 2;
            }

            Check();
            a = 0;
        }

        private void Check()
        {
            int x = Game.Window.ClientBounds.Width;
            int y = Game.Window.ClientBounds.Height;

            if (Position.X + 18 < FirstPosition.X)
            {
                Position.X += Speed;
            }
            else if (Position.X + wPlayer - 18 > x)
            {
                Position.X -= Speed;
            }
            if (Position.Y + 10 < FirstPosition.Y)
            {
                Position.Y += Speed;
            }
            else if (Position.Y + hPlayer - 5 > y)
            {
                Position.Y -= Speed;
            }
        }

        public override void Update(GameTime gameTime)
        {
           

            if (System.Environment.TickCount - LastTickCount > Timer - Speed)
            {
                LastTickCount = System.Environment.TickCount;
                UpdateStatus();
            }

            int lasttickcount1 = System.Environment.TickCount;

            if (a == 0)
            {
                KeyboardState keyboard = Keyboard.GetState();
                if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
                {
                    directionOfmotion = DirectionOfMotion.Left;
                    Move();
                    return;

                }
                else if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
                {
                    directionOfmotion = DirectionOfMotion.Rigth;
                    Move();

                    return;

                }
                else if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
                {
                    directionOfmotion = DirectionOfMotion.Up;
                    Move();

                    return;

                }
                else if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
                {
                    directionOfmotion = DirectionOfMotion.Down;
                    Move();

                    return;

                }
                this.Moving = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            sBatch.Begin();
            sBatch.Draw(PlayerTexture, new Rectangle(Position.X, Position.Y, wPlayer, hPlayer), new Rectangle(Frame.X * wPlayer, Frame.Y * hPlayer, wPlayer, hPlayer), Color.White);
            sBatch.End();

            base.Draw(gameTime);
        }
    }
}
