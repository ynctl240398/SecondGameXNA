using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace SecondGameXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Player player;
        private Texture2D texture, textureButton, textureboom;

        private const int sobutton = 10, sizeButton = 65, soboom = 40;
        private int[,] bstate = new int[sobutton, sobutton];
        private int count = 0;
        private Rectangle[,] ButtonRectengle = new Rectangle[sobutton, sobutton];
        private Texture2D[,] ButtonTexture = new Texture2D[sobutton, sobutton];
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = sizeButton * sobutton;
            graphics.PreferredBackBufferHeight = sizeButton * sobutton;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Dò Mìn";

            int y = 0;
            for (int i = 0; i < sobutton; i++)
            {
                int x = 0;
                for (int j = 0; j < sobutton; j++)
                {
                    ButtonRectengle[i,j] = new Rectangle(x, y, sizeButton, sizeButton);
                    x += sizeButton;
                    bstate[i, j] = 1;
                }
                y += sizeButton;
            }


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Services.AddService(typeof(SpriteBatch), spriteBatch);

            texture = Content.Load<Texture2D>("player");

            textureButton = Content.Load<Texture2D>("img_poit9");

            textureboom = Content.Load<Texture2D>("img_bom");

            //ButtonTexture[0, 0] = Content.Load<Texture2D>("img_poit9");

            for (int i = 0; i < sobutton; i++)
                for (int j = 0; j < sobutton; j++)            
                        ButtonTexture[i, j] = Content.Load<Texture2D>("img_cell");
 
          
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (player == null)
            {
                player = new Player(this, ref texture);
                Components.Add(player);
            }

            for (int i = 0; i < sobutton; i++)
            {
                for (int j = 0; j < sobutton; j++)
                {
                    if (player.GetBounds() == ButtonRectengle[i,j])
                                    
                        ButtonTexture[i, j] = textureButton;
                }
            }
            Random rand = new Random();
            while (count < soboom)
            {
                //for (int i = 0; i < sobutton; i++)
                //{
                //    for (int j = 0; j < sobutton; j++)
                //    {
                //        i = rand.Next(sobutton);
                //        j = rand.Next(sobutton);
                //        if ()
                //        {

                //        }
                //    }
                //}
                int i = rand.Next(sobutton), j = rand.Next(sobutton);
                if (bstate[i,j] == 1)
                {
                    bstate[i, j] = -1;
                    ButtonTexture[i, j] = textureboom;
                    count++;
                }
            }

         
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            for (int i = 0; i < sobutton; i++)
            {
                for (int j = 0; j < sobutton; j++)
                {
                    spriteBatch.Draw(ButtonTexture[i, j], ButtonRectengle[i, j], Color.White);
                }
            }
            
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
