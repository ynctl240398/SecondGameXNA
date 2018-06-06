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
        private Texture2D texture, textureboom;
        private const int sobutton = 10, sizeButton = 65, soboom = 10;
        private int[,] bstate = new int[sobutton + 2, sobutton + 2];
        private int count = 0;
        private Rectangle[,] ButtonRectengle = new Rectangle[sobutton + 1, sobutton + 1];
        private Texture2D[,] ButtonTexture = new Texture2D[sobutton + 1, sobutton + 1];
        private Rectangle[,] ButtonRectengle1 = new Rectangle[sobutton + 1, sobutton + 1];
        private Texture2D[,] ButtonTexture1 = new Texture2D[sobutton + 1, sobutton + 1];


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
            for (int i = 1; i <= sobutton; i++)
            {
                int x = 0;
                for (int j = 1; j <= sobutton; j++)
                {
                    ButtonRectengle[i, j] = new Rectangle(x, y, sizeButton, sizeButton);
                    ButtonRectengle1[i, j] = new Rectangle(x, y, sizeButton, sizeButton);                    
                    bstate[i, j] = 1;
                    x += sizeButton;
                }
                y += sizeButton;
            }

            for (int i = 0; i <= sobutton + 1; i++)
            {
                bstate[i, 0] = 10;
                bstate[0, i] = 10;
                bstate[sobutton + 1, i] = 10;
                bstate[i, sobutton + 1] = 10;
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

            textureboom = Content.Load<Texture2D>("img_bom");

            //ButtonTexture[0, 0] = Content.Load<Texture2D>("img_poit9");

            for (int i = 1; i <= sobutton; i++)
                for (int j = 1; j <= sobutton; j++)            
                        ButtonTexture[i, j] = Content.Load<Texture2D>("img_cell");           

            for (int i = 1; i <= sobutton; i++)
                for (int j = 1; j <= sobutton; j++)
                    ButtonTexture1[i, j] = Content.Load<Texture2D>("img_poit9");

            ButtonTexture1[10, 10] = Content.Load<Texture2D>("Img_finish");

            // TODO: use this.Content to load your game content here
        }

        public void CheckBoom()
        {
            for (int i = 1; i <= sobutton; i++)
            {
                for (int j = 1; j <= sobutton; j++)
                {
                    int count = 0;
                    if (bstate[i, j] != -1)
                    {
                        if (i == 10 && j == 10)
                        {
                            break;
                        }
                        else
                        {
                            if (bstate[i - 1, j - 1] == -1) count++;
                            if (bstate[i - 1, j] == -1) count++;
                            if (bstate[i - 1, j + 1] == -1) count++;
                            if (bstate[i, j - 1] == -1) count++;
                            if (bstate[i, j + 1] == -1) count++;
                            if (bstate[i + 1, j - 1] == -1) count++;
                            if (bstate[i + 1, j] == -1) count++;
                            if (bstate[i + 1, j + 1] == -1) count++;
                        }
                        
                        bstate[i, j] = count;
                    }
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void Start()
        {
            Random rand = new Random(); 
            int a = 1;
            int b = 1;
            bstate[1, 1] = 2;

            switch (rand.Next(1, 2)) // tìm đường đi đầu tiên
            {
                case 1:
                    bstate[1, 2] = 2;
                    b = 2;
                    break;
                case 2:
                    bstate[2, 1] = 2;
                    a = 2;
                    break;
                default:
                    break;
            }


            while (bstate[10, 10] != 2)
            {
                int[] r = new int[5];
                r[1] = 1;
                r[2] = 2;
                r[3] = 3;
                r[4] = 4;
                if (a == 1) r[3] = 0;
                if (a == 10) r[1] = 0;
                if (b == 1) r[4] = 0;
                if (b == 10) r[2] = 0;

                int t = rand.Next(1, 4);
                while (r[t] == 0)
                    t = rand.Next(1, 4);

                switch (t)
                {
                    case 1:
                        bstate[a + 1, b] = 2;
                        //  ButtonTexture[a + 1, b] = tetu;
                        a = a + 1;
                        break;
                    case 2:
                        bstate[a, b + 1] = 2;

                        //   ButtonTexture[a, b + 1] = tetu;
                        b = b + 1;
                        break;
                    case 3:
                        bstate[a - 1, b] = 2;
                        //   ButtonTexture[a - 1, b] = tetu;
                        a = a - 1;
                        break;
                    case 4:
                        bstate[a, b - 1] = 2;
                        // ButtonTexture[a, b - 1] = tetu;
                        b = b - 1;
                        break;
                    default:
                        break;
                }


            }

            while (count < soboom) // random boom
            {
                int i = rand.Next(1, sobutton + 1), j = rand.Next(1, sobutton + 1);
                if (bstate[i, j] == 1)
                {
                    bstate[i, j] = -1;
                    ButtonTexture1[i, j] = textureboom;
                    count++;
                }
            }

            CheckBoom();


            for (int i = 1; i <= sobutton; i++)
            {
                for (int j = 1; j <= sobutton; j++)
                {
                    switch (bstate[i, j])
                    {
                        case 1:
                            ButtonTexture1[i, j] = Content.Load<Texture2D>("img_poit1");
                            break;
                        case 2:
                            ButtonTexture1[i, j] = Content.Load<Texture2D>("img_poit2");
                            break;
                        case 3:
                            ButtonTexture1[i, j] = Content.Load<Texture2D>("img_poit3");
                            break;
                        case 4:
                            ButtonTexture1[i, j] = Content.Load<Texture2D>("img_poit4");
                            break;
                        case 5:
                            ButtonTexture1[i, j] = Content.Load<Texture2D>("img_poit5");
                            break;
                        case 6:
                            ButtonTexture1[i, j] = Content.Load<Texture2D>("img_poit6");
                            break;
                        case 7:
                            ButtonTexture1[i, j] = Content.Load<Texture2D>("img_poit7");
                            break;
                        case 8:
                            ButtonTexture1[i, j] = Content.Load<Texture2D>("img_poit8");
                            break;
                        default:
                            break;
                    }
                }
            }

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
                Start();
            }

            for (int i = 1; i <= sobutton; i++)
            {
                for (int j = 1; j <= sobutton; j++)
                {
                    if (player.getbound(ButtonRectengle[i, j]))
                    {
                        if (i != 10 && j != 10)
                            ButtonTexture[i, j] = ButtonTexture1[i, j];
                        else
                            ButtonTexture[i, j] = ButtonTexture1[10, 10];
                                
                        if (bstate[i,j] == -1)
                        {
                            Components.Remove(player);
                        }

                    }
                    
                }
            }
            // 1 là ô bình thường, -1 là cô có boom


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
            for (int i = 1; i <= sobutton; i++)
            {
                for (int j = 1; j <= sobutton; j++)
                {
                    spriteBatch.Draw(ButtonTexture1[i, j], ButtonRectengle[i, j], Color.White);
                }
            }
            
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
