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
   
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Player player;
        private Texture2D texture, textureButton, textureboom,tetu;

        private const int sobutton = 10, sizeButton = 65, soboom = 30;
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

       
        protected override void Initialize()
        {
         
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

      
        protected override void LoadContent()
        {
         
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Services.AddService(typeof(SpriteBatch), spriteBatch);

            texture = Content.Load<Texture2D>("player");

            textureButton = Content.Load<Texture2D>("img_poit9");

            textureboom = Content.Load<Texture2D>("img_bom");

            tetu = Content.Load<Texture2D>("img_poit9");

            for (int i = 0; i < sobutton; i++)
                for (int j = 0; j < sobutton; j++)            
                        ButtonTexture[i, j] = Content.Load<Texture2D>("img_cell");
 
          
          
        }

   
        protected override void UnloadContent()
        {
           
        }


        protected void start()
        {
            if (player == null)
            {
                player = new Player(this, ref texture);
                Components.Add(player);
            }

            for (int i = 0; i < sobutton; i++)
            {
                for (int j = 0; j < sobutton; j++)
                {
                    if (player.GetBounds() == ButtonRectengle[i, j])

                        ButtonTexture[i, j] = textureButton;
                }
            }



            Random rand = new Random();
            int a = 0;
            int b = 0;
            bstate[0, 0] = 2;

            switch (rand.Next(1, 2))
            {
                case 1:
                    bstate[0, 1] = 2;
                    break;
                    b = 1;
                case 2:
                    bstate[1, 0] = 2;
                    a = 1;
                    break;
            }


            while (bstate[9, 9] != 2)
            {
                int[] r = new int[5];
                r[1] = 1;
                r[2] = 2;
                r[3] = 3;
                r[4] = 4;
                if (a == 0) r[3] = 0;
                if (a == 9) r[1] = 0;
                if (b == 0) r[4] = 0;
                if (b == 9) r[2] = 0;

                int t = rand.Next(1, 4);
                while (r[t] == 0)
                    t = rand.Next(1, 4);

                switch (t)
                {
                    case 1:
                        bstate[a + 1, b] = 2;
                        ButtonTexture[a + 1, b] = tetu;
                        a = a + 1;
                        break;
                    case 2:
                        bstate[a, b + 1] = 2;

                        ButtonTexture[a, b + 1] = tetu;
                        b = b + 1;
                        break;
                    case 3:
                        bstate[a - 1, b] = 2;
                        ButtonTexture[a - 1, b] = tetu;
                        a = a - 1;
                        break;
                    case 4:
                        bstate[a, b - 1] = 2;
                        ButtonTexture[a, b - 1] = tetu;
                        b = b - 1;
                        break;
                }


            }

            while (count < soboom)
            {
                int i = rand.Next(sobutton ), j = rand.Next(sobutton);
                if (bstate[i, j] == 1)
                {
                    bstate[i, j] = -1;
                    ButtonTexture[i, j] = textureboom;
                    count++;
                }
            }






        }
        protected override void Update(GameTime gameTime)
        {
         
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (player == null)
            {
                start();
            }

           

         
            
          

            base.Update(gameTime);
        }

       
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

         

            base.Draw(gameTime);
        }
    }
}
