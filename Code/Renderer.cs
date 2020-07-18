﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Raycaster;
using System;
using System.Collections.Generic;
using System.IO;

namespace DungeonsAndDungeons
{
    public class Renderer
    {
        public readonly int ScreenWidth;
        public readonly int ScreenHeight;
        private readonly int MapWidth;
        private readonly int MapHeight;
        private const int TexWidth = 64;
        private const int TexHeight = 64;

        private int[,] _worldMap;
        private double rotSpeed = 0.5;
        private double moveSpeed = 0.5;

        private Color[] _buffer;

        Vector2 position = new Vector2(20.5f, 8.5f);
        Vector2 direction = new Vector2(-1, 0);
        Vector2 cameraPlane = new Vector2(0, 0.66f);

        private readonly List<Texture2D> _textures;
        //private Font _textFont = new Font(FontFamily.GenericSansSerif, 25);
        private Color wallColor;
        private Color floorColor;
        private Color ceilingColor;
        private List<Color[]> _colors;

        public Renderer(int h, int w, List<Texture2D> textures)
        {
            ScreenWidth = w;
            ScreenHeight = h;

            _textures = textures;

            _buffer = new Color[ScreenWidth * ScreenHeight];

            _worldMap = new int[,] {
                { 4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,7,7,7,7,7,7,7,7},
                { 4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,7},
                { 4,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7},
                { 4,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7},
                { 4,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,7},
                { 4,0,4,0,0,0,0,5,5,5,5,5,5,5,5,5,7,7,0,7,7,7,7,7},
                { 4,0,5,0,0,0,0,5,0,5,0,5,0,5,0,5,7,0,0,0,7,7,7,1},
                { 4,0,6,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,0,0,0,8},
                { 4,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,7,7,1},
                { 4,0,8,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,0,0,0,8},
                { 4,0,0,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,7,7,7,1},
                { 4,0,0,0,0,0,0,5,5,5,5,0,5,5,5,5,7,7,7,7,7,7,7,1},
                { 6,6,6,6,6,6,6,6,6,6,6,0,6,6,6,6,6,6,6,6,6,6,6,6},
                { 8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                { 6,6,6,6,6,6,0,6,6,6,6,0,6,6,6,6,6,6,6,6,6,6,6,6},
                { 4,4,4,4,4,4,0,4,4,4,6,0,6,2,2,2,2,2,2,2,3,3,3,3},
                { 4,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,0,0,0,2},
                { 4,0,0,0,0,0,0,0,0,0,0,0,6,2,0,0,5,0,0,2,0,0,0,2},
                { 4,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,2,0,2,2},
                { 4,0,6,0,6,0,0,0,0,4,6,0,0,0,0,0,5,0,0,0,0,0,0,2},
                { 4,0,0,5,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,2,0,2,2},
                { 4,0,6,0,6,0,0,0,0,4,6,0,6,2,0,0,5,0,0,2,0,0,0,2},
                { 4,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,0,0,0,2},
                { 4,4,4,4,4,4,4,4,4,4,1,1,1,2,2,2,2,2,2,3,3,3,3,3}
            };

            MapWidth = _worldMap.GetLength(1);
            MapHeight = _worldMap.GetLength(0);

            _colors = new List<Color[]>(textures.Count);

            GenerateColorsFromTexture(textures);
        }

        private void GenerateColorsFromTexture(List<Texture2D> textures)
        {
            Color[] colors = new Color[TexWidth * TexHeight];

            for (int i = 0; i < textures.Count; i++)
            {
                textures[i].GetData<Color>(colors);
                _colors.Add(colors);
                colors = new Color[TexWidth * TexHeight];
            }
        }

        public Color[] Render()
        {

            for (int y = 0; y < ScreenHeight; y++)
            {
                // rayDir for leftmost ray (x = 0) and rightmost ray (x = w)
                float rayDirX0 = direction.X - cameraPlane.X;
                float rayDirY0 = direction.Y - cameraPlane.Y;
                float rayDirX1 = direction.X + cameraPlane.X;
                float rayDirY1 = direction.Y + cameraPlane.Y;

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

                float floorX = (position.X + rowDistance * rayDirX0);
                float floorY = (position.Y + rowDistance * rayDirY0);

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

                    floorColor = GetPixel(_colors[floorTexture], tx, ty);
                    _buffer[x + (y * ScreenWidth)] = floorColor;

                    ceilingColor = GetPixel(_colors[ceilingTexture], tx, ty);
                    _buffer[x + ((ScreenHeight - y - 1) * ScreenWidth)] = ceilingColor;
                }
            }

            for (int x = 0; x < ScreenWidth; x++)
            {
                double cameraX = 2 * (x) / (double)ScreenWidth - 1;
                double rayDirX = direction.X + cameraPlane.X * cameraX;
                double rayDirY = direction.Y + cameraPlane.Y * cameraX;

                // which map tile the ray is currently in
                int mapX = (int)position.X;
                int mapY = (int)position.Y;

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
                    sideDistX = (position.X - mapX) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistX = (mapX + 1.0 - position.X) * deltaDistX;
                }

                if (rayDirY < 0)
                {
                    stepY = -1;
                    sideDistY = (position.Y - mapY) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistY = (mapY + 1.0 - position.Y) * deltaDistY;
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

                    if (_worldMap[mapX, mapY] > 0)
                    {
                        hit = 1;
                    }
                }

                //caluclate distance to wall
                if (side == 0)
                {
                    perpWallDist = (mapX - position.X + (1 - stepX) / 2) / rayDirX;
                }
                else
                {
                    perpWallDist = (mapY - position.Y + (1 - stepY) / 2) / rayDirY;
                }

                //calculate height of wall strip
                int lineHeight = (int)(ScreenHeight / perpWallDist);

                //calculate lowest and highest pixel to fill in
                int drawStart = -lineHeight / 2 + ScreenHeight / 2;
                drawStart = Math.Max(drawStart, 0);

                int drawEnd = lineHeight / 2 + ScreenHeight / 2;
                drawEnd = Math.Min(drawEnd, ScreenHeight - 1);

                int texNum = _worldMap[mapX, mapY] - 1;

                double wallX;

                if (side == 0)
                {
                    wallX = position.Y + perpWallDist * rayDirY;
                }
                else
                {
                    wallX = position.X + perpWallDist * rayDirX;
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

                    wallColor = GetPixel(_colors[texNum], texX, texY);

                    if (side == 1)
                    {
                        wallColor = ChangeBrightness(wallColor, 0.7f);
                    }

                    //_buffer.DrawString(perpWallDist.ToString(), _textFont, new SolidBrush(wallColor.White), 50, 50, 
                    //    new StringFormat(StringFormatFlags.NoClip));

                    _buffer[(x + (y * ScreenWidth))] = wallColor;
                }
            }

            return _buffer;
        }

        private double GetDirectionQuarter(double dX, double dY)
        {
            double angle = Math.Atan2(dY, dX) * 180 / Math.PI;
            return Math.Round(angle / 90) * 90;
        }

        public Color ChangeBrightness(Color color, float f)
        {
            Color changed = Color.Multiply(color, f);
            changed.A = 255;
            return changed;
        }

        public void MoveForward(GameTime time)
        {
            double angle = GetDirectionQuarter(direction.X, direction.Y);

            switch (angle)
            {
                case 180:
                    position.X -= 1;
                    break;

                case 0:
                    position.X += 1;
                    break;

                case 90:
                    position.Y += 1;
                    break;

                case -90:
                    position.Y -= 1;
                    break;
            }
        }

        public void RotateLeft()
        {
            direction = new Vector2(-direction.Y, direction.X);
            cameraPlane = new Vector2(-cameraPlane.Y, cameraPlane.X);
        }

        public void RotateRight()
        {
            direction = new Vector2(direction.Y, -direction.X);
            cameraPlane = new Vector2(cameraPlane.Y, -cameraPlane.X);
        }

        private Color GetPixel(Color[] texture, int x, int y)
        {
            return texture[x + (y * TexWidth)];
        }

    }

}