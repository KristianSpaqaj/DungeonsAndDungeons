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

        public Renderer(int h, int w)
        {
            ScreenWidth = w;
            ScreenHeight = h;

            _buffer = new Color[ScreenWidth * ScreenHeight];
            zBuffer = new double[ScreenWidth];
            spriteOrder = new List<int>();
            spriteDistance = new List<double>();
        }

        public Color[] Render(Camera camera, Level level)
        {
            RenderFloorAndCeiling(camera, level);

            RenderWalls(camera, level);

            RenderSprites(camera, level);


            return _buffer;
        }

        private void RenderFloorAndCeiling(Camera camera, Level level)
        {
            for (int y = 0; y < ScreenHeight; y++)
            {
                // rayDir for leftmost ray (x = 0) and rightmost ray (x = w)
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

        private void RenderWalls(Camera camera, Level level)
        {
            for (int x = 0; x < ScreenWidth; x++)
            {
                double cameraX = 2 * (x) / (double)ScreenWidth - 1;
                double rayDirX = camera.Direction.X + camera.Plane.X * cameraX;
                double rayDirY = camera.Direction.Y + camera.Plane.Y * cameraX;

                // which map tile the ray is currently in
                int mapX = (int)camera.Position.X;
                int mapY = (int)camera.Position.Y;

                double sideDistX; // length to next x side
                double sideDistY; // length to next y side

                double deltaDistX = (rayDirY == 0) ? 0 : ((rayDirX == 0) ? 1 : Math.Abs(1 / rayDirX));
                double deltaDistY = (rayDirX == 0) ? 0 : ((rayDirY == 0) ? 1 : Math.Abs(1 / rayDirY));
                double perpWallDist;

                // which direction to step in  x or y direction (either -1 or +1)
                int stepX;
                int stepY;

                int hit = 0; // 1 if wall has been hit, 0 otherwise

                int side = 1; // NS or EW wall hit?

                //calculate step and initial sideDist

                if (rayDirX < 0)
                {
                    stepX = -1;
                    sideDistX = (camera.Position.X - mapX) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistX = (mapX + 1.0 - camera.Position.X) * deltaDistX;
                }

                if (rayDirY < 0)
                {
                    stepY = -1;
                    sideDistY = (camera.Position.Y - mapY) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistY = (mapY + 1.0 - camera.Position.Y) * deltaDistY;
                }

                while (hit == 0)
                {
                    //jump to next map square 
                    if (sideDistX < sideDistY)
                    {
                        sideDistX += deltaDistX;
                        mapX += stepX;
                        side = 0;
                    }
                    else
                    {
                        sideDistY += deltaDistY;
                        mapY += stepY;
                        side = 1;
                    }

                    if (level.Map[mapX, mapY] > 0)
                    {
                        hit = 1;
                    }
                }

                //caluclate distance to wall
                if (side == 0)
                {
                    perpWallDist = (mapX - camera.Position.X + (1 - stepX) / 2) / rayDirX;
                }
                else
                {
                    perpWallDist = (mapY - camera.Position.Y + (1 - stepY) / 2) / rayDirY;
                }

                //calculate height of wall strip
                int lineHeight = (int)(ScreenHeight / perpWallDist);

                //calculate lowest and highest pixel to fill in
                int drawStart = -lineHeight / 2 + ScreenHeight / 2;
                drawStart = Math.Max(drawStart, 0);

                int drawEnd = lineHeight / 2 + ScreenHeight / 2;
                drawEnd = Math.Min(drawEnd, ScreenHeight - 1);

                int texNum = level.Map[mapX, mapY] - 1;

                double wallX;

                if (side == 0)
                {
                    wallX = camera.Position.Y + perpWallDist * rayDirY;
                }
                else
                {
                    wallX = camera.Position.X + perpWallDist * rayDirX;
                }

                wallX -= Math.Floor(wallX);

                int texX = (int)(wallX * (double)TexWidth);
                if (side == 0 && rayDirX > 0)
                {
                    texX = TexWidth - texX - 1;
                }
                else if (side == 1 && rayDirY < 0)
                {
                    texX = TexWidth - texX - 1;
                }

                double step = 1.0 * TexHeight / lineHeight;
                double texPos = (drawStart - ScreenHeight / 2 + lineHeight / 2) * step;

                for (int y = drawStart; y < drawEnd; y++)
                {
                    int texY = (int)texPos & (TexHeight - 1);
                    texPos += step;

                    wallColor = GetPixel(level.Map.GetTileTexture(mapX, mapY), texX, texY);

                    if (side == 1)
                    {
                        wallColor = ChangeBrightness(wallColor, 0.7f);
                    }

                    _buffer[(x + (y * ScreenWidth))] = wallColor;
                }

                zBuffer[x] = perpWallDist; //perpendicular distance is used

            }
        }



        private void RenderSprites(Camera camera, Level level)
        {
            for (int i = 0; i < level.Entities.Count; i++)
            {
                spriteOrder.Add(i);
                spriteDistance.Add(((camera.Position.X - level.Entities[i].Position.X) * (camera.Position.X - level.Entities[i].Position.X) +
                                     (camera.Position.Y - level.Entities[i].Position.Y) * (camera.Position.Y - level.Entities[i].Position.Y))); //sqrt not taken, unneeded
            }

            sortSprites(spriteOrder, spriteDistance, level.Entities.Count);

            for (int i = 0; i < level.Entities.Count; i++)
            {
                //translate sprite position to relative to camera
                double spriteX = level.Entities[spriteOrder[i]].Position.X - camera.Position.X;
                double spriteY = level.Entities[spriteOrder[i]].Position.Y - camera.Position.Y;

                double invDet = 1.0 / (camera.Plane.X * camera.Direction.Y - camera.Direction.X * camera.Plane.Y); //required for correct matrix multiplication

                double transformX = invDet * (camera.Direction.Y * spriteX - camera.Direction.X * spriteY);
                double transformY = invDet * (-camera.Plane.Y * spriteX + camera.Plane.X * spriteY); //this is actually the depth inside the screen, that what Z is in 3D

                int spriteScreenX = (int)((ScreenWidth / 2) * (1 + transformX / transformY));

                //calculate height of the sprite on screen
                int spriteHeight = (int)Math.Abs(Math.Floor(ScreenHeight / transformY)); //using 'transformY' instead of the real distance prevents fisheye
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
                    int texX = (int)(256 * (stripe - (-spriteWidth / 2 + spriteScreenX)) * TexWidth / spriteWidth) / 256;
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
                            Color color = GetPixel(level.Entities[0].Sprite, texX, texY); //get current color from the texture
                            if (color != Color.White)
                            {
                                _buffer[y * ScreenWidth + stripe] = color; //paint pixel if it isn't black, black is the invisible color
                            }
                        }
                    }
                }
            }
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

        private void sortSprites(List<int> spriteOrder, List<double> spriteDistance, int count)
        {

        }
    }
}