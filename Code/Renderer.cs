using DungeonsAndDungeons.Entities;
using DungeonsAndDungeons.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Renderer
    {
        public readonly int ScreenWidth;
        public readonly int ScreenHeight;
        private const int TexWidth = 64;
        private const int TexHeight = 64;

        private readonly Color[] _buffer;

        private Color wallColor;
        private Color floorColor;
        private Color ceilingColor;

        //Sprite variables
        private readonly double[] zBuffer;
        private readonly List<int> spriteOrder;
        private readonly List<double> spriteDistance;
        readonly List<IRenderable> Renderables;


        public Renderer(int h, int w)
        {
            ScreenWidth = w;
            ScreenHeight = h;

            _buffer = new Color[ScreenWidth * ScreenHeight];
            zBuffer = new double[ScreenWidth];
            spriteOrder = new List<int>();
            spriteDistance = new List<double>();
            Renderables = new List<IRenderable>();
        }

        public Color[] Render(Camera camera, Level level)
        {
            RenderFloorAndCeiling(camera, level);
            RenderWalls(camera, level);

            Renderables.Clear();
            Renderables.AddRange(level.Items);
            Renderables.AddRange(level.Entities);

            RenderSprites(camera, Renderables);

            if (level.Player.Inventory.Selected != null)
            {
                RenderPlayerItem(camera, level);
            }

            return _buffer;
        }



        /// <summary>
        /// Draws floor and ceiling to internal buffer based on <paramref name="camera"/> perspective
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="level"></param>
        private void RenderFloorAndCeiling(Camera camera, Level level)
        {
            for (int y = 0; y < ScreenHeight; y++)
            {
                // ray.Direction for leftmost ray (x = 0) and rightmost ray (x = w)
                float rayDirX0 = camera.Direction.X - camera.Plane.X;
                float rayDirY0 = camera.Direction.Y - camera.Plane.Y;
                float rayDirX1 = camera.Direction.X + camera.Plane.X;
                float rayDirY1 = camera.Direction.Y + camera.Plane.Y;

                // Current y position compared to the center of the screen (the horizon)
                int p = y - ScreenHeight / 2;

                // Vertical position of the camera.
                float posZ = (float)(0.5 * ScreenHeight);

                // Horizontal distance from the camera to the floor for the current row.
                // 0.5 is the z position exactly in the middle between floor and ceiling.
                float rowDistance = posZ / p;

                // calculate the real world step vector we have to add for each x (parallel to camera plane)
                // adding step by step avoids multiplications with a weight in the inner loop
                float floorStepX = rowDistance * (rayDirX1 - rayDirX0) / ScreenWidth;
                float floorStepY = rowDistance * (rayDirY1 - rayDirY0) / ScreenWidth;

                float floorX = (camera.Position.X + rowDistance * rayDirX0);
                float floorY = (camera.Position.Y + rowDistance * rayDirY0);

                for (int x = 0; x < ScreenWidth; ++x)
                {
                    // the cell coord is simply got from the integer parts of floorX and floorY
                    int cellX = (int)(floorX);
                    int cellY = (int)(floorY);

                    // get the texture coordinate from the fractional part
                    int tx = (int)(TexWidth * (floorX - cellX)) & (TexWidth - 1);
                    int ty = (int)(TexHeight * (floorY - cellY)) & (TexHeight - 1);

                    floorX += floorStepX;
                    floorY += floorStepY;

                    // choose texture and draw the pixel
                    int floorTexture = 3;
                    int ceilingTexture = 6;

                    floorColor = GetPixel(level.Map.Textures[floorTexture], tx, ty);
                    _buffer[x + (y * ScreenWidth)] = floorColor;

                    ceilingColor = GetPixel(level.Map.Textures[ceilingTexture], tx, ty);
                    _buffer[x + ((ScreenHeight - y - 1) * ScreenWidth)] = ceilingColor;
                }
            }
        }

        /// <summary>
        /// Draws walls to internal buffer based on <paramref name="camera"/> perspective and <paramref name="level"/>.Map
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="level"></param>
        private void RenderWalls(Camera camera, Level level)
        {
            for (int x = 0; x < ScreenWidth; x++)
            {
                double cameraX = (2 * x / (double)ScreenWidth) - 1;
                Vector2 rayDirection = new Vector2((float)(camera.Direction.X + camera.Plane.X * cameraX),
                                                   (float)(camera.Direction.Y + camera.Plane.Y * cameraX));
                
                Ray ray = new Ray(camera.Position, rayDirection);

                double perpWallDist;

                int hit = 0; // 1 if wall has been hit, 0 otherwise


                while (hit == 0)
                {
                    //jump to next ray.Map square 
                    ray.GoToNextSquare();

                    if (!level.Map.IsEmpty(ray.MapX, ray.MapY))
                    {
                        hit = 1;
                    }
                }

                //caluclate distance to wall
                if (ray.Side == 0)
                {
                    perpWallDist = (ray.MapX - camera.Position.X + (1 - ray.StepX) / 2) / ray.DirectionX;
                }
                else
                {
                    perpWallDist = (ray.MapY - camera.Position.Y + (1 - ray.StepY) / 2) / ray.DirectionY;
                }

                //calculate height of wall strip
                int lineHeight = (int)(ScreenHeight / perpWallDist);

                //calculate lowest and highest pixel to fill in
                int drawStart = -lineHeight / 2 + ScreenHeight / 2;
                drawStart = Math.Max(drawStart, 0);

                int drawEnd = lineHeight / 2 + ScreenHeight / 2;
                drawEnd = Math.Min(drawEnd, ScreenHeight - 1);

                double wallX;

                if (ray.Side == 0)
                {
                    wallX = camera.Position.Y + perpWallDist * ray.DirectionY;
                }
                else
                {
                    wallX = camera.Position.X + perpWallDist * ray.DirectionX;
                }

                wallX -= Math.Floor(wallX);

                int texX = (int)(wallX * TexWidth);
                if (ray.Side == 0 && ray.DirectionX > 0)
                {
                    texX = TexWidth - texX - 1;
                }
                else if (ray.Side == 1 && ray.DirectionY < 0)
                {
                    texX = TexWidth - texX - 1;
                }

                double step = 1.0 * TexHeight / lineHeight;
                double texPos = (drawStart - ScreenHeight / 2 + lineHeight / 2) * step;

                for (int y = drawStart; y < drawEnd; y++)
                {
                    int texY = (int)texPos & (TexHeight - 1);
                    texPos += step;

                    wallColor = GetPixel(level.Map.GetTileTexture(ray.MapX, ray.MapY), texX, texY);

                    if (ray.Side == 1)
                    {
                        wallColor = ChangeBrightness(wallColor, 0.7f);
                    }

                    _buffer[(x + (y * ScreenWidth))] = wallColor;
                }

                zBuffer[x] = perpWallDist; //perpendicular distance is used
            }
        }

        /// <summary>
        /// Draws a list of drawable objects to internal buffer, based on <paramref name="camera"/> perspective
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="camera"></param>
        /// <param name="renderables">List of objects implementing the <c>IRenderable</c> interface</param>
        private void RenderSprites<T>(Camera camera, List<T> renderables) where T : class, IRenderable
        {
            spriteOrder.Clear();
            spriteDistance.Clear();

            for (int i = 0; i < renderables.Count; i++)
            {
                spriteOrder.Add(i);
                spriteDistance.Add(Math.Pow(camera.Position.X - renderables[i].Position.X, 2) +
                                   Math.Pow(camera.Position.Y - renderables[i].Position.Y, 2));
            }

            SortSprites(spriteOrder, spriteDistance, renderables.Count); // TODO currently only sorts through given set of sprites

            for (int i = 0; i < renderables.Count; i++)
            {
                RenderSprite(camera, renderables[spriteOrder[i]]);
            }
        }

        private void RenderSprite<T>(Camera camera, T renderable) where T : class, IRenderable
        {
            //translate sprite position to relative to camera
            double spriteX = renderable.Position.X - camera.Position.X;
            double spriteY = renderable.Position.Y - camera.Position.Y;

            double invDet = 1.0 / (camera.Plane.X * camera.Direction.Y - camera.Direction.X * camera.Plane.Y); //required for correct matrix multiplication

            double transformX = invDet * (camera.Direction.Y * spriteX - camera.Direction.X * spriteY);
            double transformY = invDet * (-camera.Plane.Y * spriteX + camera.Plane.X * spriteY); //this is actually the depth inside the screen, that what Z is in 3D

            int spriteScreenX = (int)((ScreenWidth / 2) * (1 + transformX / transformY));

            //calculate height of the sprite on screen
            int spriteHeight = (int)(Math.Abs(Math.Floor(ScreenHeight / transformY))); //using 'transformY' instead of the real distance prevents fisheye
                                                                                       //calculate lowest and highest pixel to fill in current stripe
            int drawStartY = -spriteHeight / 2 + ScreenHeight / 2;
            if (drawStartY < 0) drawStartY = 0;
            int drawEndY = spriteHeight / 2 + ScreenHeight / 2;
            if (drawEndY >= ScreenHeight) drawEndY = ScreenHeight - 1;

            //calculate width of the sprite
            int spriteWidth = (int)Math.Abs(Math.Floor(ScreenWidth / transformY)) / 2;
            int drawStartX = -spriteWidth / 2 + spriteScreenX;
            if (drawStartX < 0) drawStartX = 0;
            int drawEndX = spriteWidth / 2 + spriteScreenX;
            if (drawEndX >= ScreenWidth) drawEndX = ScreenWidth - 1;

            for (int stripe = drawStartX; stripe < drawEndX; stripe++)
            {
                int texX = 256 * (stripe - (-spriteWidth / 2 + spriteScreenX)) * TexWidth / spriteWidth / 256;
                //the conditions in the if are:
                //1) it's in front of camera plane so you don't see things behind you
                //2) it's on the screen (left)
                //3) it's on the screen (right)
                //4) ZBuffer, with perpendicular distance
                if (transformY > 0 && stripe > 0 && stripe < ScreenWidth && transformY < zBuffer[stripe])
                {
                    for (int y = drawStartY; y < drawEndY; y++) //for every pixel of the current stripe
                    {
                        int d = (y) * 256 - ScreenHeight * 128 + spriteHeight * 128; //256 and 128 factors to avoid floats
                        int texY = ((d * TexHeight) / spriteHeight) / 256;
                        Color color = GetPixel(renderable.Sprite, texX, texY); //get current color from the texture
                        if (color != Color.Transparent)
                            _buffer[y * ScreenWidth + stripe] = color;
                    }
                }
            }
        }

        private void RenderPlayerItem(Camera camera, Level level)
        {
            Player player = level.Player;
            Item item = player.Inventory.Selected;
            item.Position = new Vector2((player.Position.X + player.Direction.X * 1), player.Position.Y + player.Direction.Y * 1);
            RenderSprite(camera, item);
        }
        public Color ChangeBrightness(Color color, float f)
        {
            Color changed = Color.Multiply(color, f);
            changed.A = 255;
            return changed;
        }

        private Color GetPixel(Color[] texture, int x, int y)
        {
            return texture[x + (y * TexWidth)];
        }

        private void SortSprites(List<int> spriteOrder, List<double> spriteDistance, int count)
        {
            List<Tuple<double, int>> sprites = new List<Tuple<double, int>>();

            for (int i = 0; i < count; i++)
            {
                sprites.Add(new Tuple<double, int>(spriteDistance[i], spriteOrder[i]));
            }

            sprites.Sort();

            for (int i = 0; i < count; i++)
            {
                spriteDistance[i] = sprites[count - i - 1].Item1;
                spriteOrder[i] = sprites[count - i - 1].Item2;
            }
        }
    }
}