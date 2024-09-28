using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomsOf2048
{
    public class KingdomsOf2048
    {
        public const int SIZE = 4;
        public int[] GameArea = new int[SIZE * SIZE];
        public Random Random = new Random();

        public void SpawnRandom()
        {
            if (GetFreeAreas() == 0)
                throw new Exception("No free spaces!");

            int pos = Random.Next(GameArea.Length);
            while (GameArea[pos] != 0)
                pos = Random.Next(GameArea.Length);

            GameArea[pos] = 2;
        }

        public int GetFreeAreas()
        {
            int res = 0;
            for(int i = 0; i < GameArea.Length; i++)
            {
                if (GameArea[i] == 0)
                    res++;
            }
            return res;
        }

        public bool MovePossible()
        {
            if (GetFreeAreas() > 0)
                return true;
            for(int y = 0; y < SIZE; y++)
                for (int x = 1; x < SIZE; x++)
                    if (GameArea[y * SIZE + x - 1] == GameArea[y * SIZE + x])
                        return true;
            for (int x = 0; x < SIZE; x++)
                for (int y = 1; y < SIZE; y++)
                    if (GameArea[(y - 1) * SIZE + x] == GameArea[y * SIZE + x])
                        return true;
            return false;
        }

        public void MoveLeft()
        {
            bool[] mask = new bool[SIZE * SIZE];
            for(int y = 0; y < SIZE; y++)
            {
                for(int x = 0; x < SIZE; x++)
                {
                    if (GameArea[y * SIZE + x] != 0 && x > 0)
                    {
                        int tx = x - 1;
                        while (tx > 0 && GameArea[y * SIZE + tx] == 0)
                            tx--;
                        if (GameArea[y * SIZE + x] == GameArea[y * SIZE + tx] && !mask[y * SIZE + tx])
                        {
                            GameArea[y * SIZE + tx] *= 2;
                            GameArea[y * SIZE + x] = 0;
                            mask[y * SIZE + tx] = true;
                        }
                        else if (tx + 1 != x)
                        {
                            if (GameArea[y * SIZE + tx] != 0)
                            {
                                GameArea[y * SIZE + tx + 1] = GameArea[y * SIZE + x];
                                GameArea[y * SIZE + x] = 0;
                            }
                            else
                            {
                                GameArea[y * SIZE + tx] = GameArea[y * SIZE + x];
                                GameArea[y * SIZE + x] = 0;
                            }
                        }
                        else if (GameArea[y * SIZE + tx] == 0)
                        {
                            GameArea[y * SIZE + tx] = GameArea[y * SIZE + x];
                            GameArea[y * SIZE + x] = 0;
                        }
                    }
                }
            }
        }

        public void MoveRight()
        {
            bool[] mask = new bool[SIZE * SIZE];
            for (int y = 0; y < SIZE; y++)
            {
                for(int x = SIZE - 1; x >= 0; x--)
                {
                    if (GameArea[y * SIZE + x] != 0 && x < SIZE - 1)
                    {
                        int tx = x + 1;
                        while (tx < SIZE - 1 && GameArea[y * SIZE + tx] == 0)
                            tx++;
                        if (GameArea[y * SIZE + x] == GameArea[y * SIZE + tx] && !mask[y * SIZE + tx])
                        {
                            GameArea[y * SIZE + tx] *= 2;
                            GameArea[y * SIZE + x] = 0;
                            mask[y * SIZE + tx] = true;
                        }
                        else if (tx - 1 != x)
                        {
                            if (GameArea[y * SIZE + tx] != 0)
                            {
                                GameArea[y * SIZE + tx - 1] = GameArea[y * SIZE + x];
                                GameArea[y * SIZE + x] = 0;
                            }
                            else
                            {
                                GameArea[y * SIZE + tx] = GameArea[y * SIZE + x];
                                GameArea[y * SIZE + x] = 0;
                            }
                        }
                        else if (GameArea[y * SIZE + tx] == 0)
                        {
                            GameArea[y * SIZE + tx] = GameArea[y * SIZE + x];
                            GameArea[y * SIZE + x] = 0;
                        }
                    }
                }
            }
        }

        public void MoveUp()
        {
            bool[] mask = new bool[SIZE * SIZE];
            for (int x = 0; x < SIZE; x++)
            {
                for(int y = 0; y < SIZE; y++)
                {
                    if (GameArea[y * SIZE + x] != 0 && y > 0)
                    {
                        int ty = y - 1;
                        while (ty > 0 && GameArea[ty * SIZE + x] == 0)
                            ty--;
                        if (GameArea[y * SIZE + x] == GameArea[ty * SIZE + x] && !mask[ty * SIZE + x])
                        {
                            GameArea[ty * SIZE + x] *= 2;
                            GameArea[y * SIZE + x] = 0;
                            mask[ty * SIZE + x] = true;
                        }
                        else if (ty + 1 != y)
                        {
                            if (GameArea[ty * SIZE + x] != 0)
                            {
                                GameArea[(ty + 1) * SIZE + x] = GameArea[y * SIZE + x];
                                GameArea[y * SIZE + x] = 0;
                            }
                            else
                            {
                                GameArea[ty * SIZE + x] = GameArea[y * SIZE + x];
                                GameArea[y * SIZE + x] = 0;
                            }
                        }
                        else if (GameArea[ty * SIZE + x] == 0)
                        {
                            GameArea[ty * SIZE + x] = GameArea[y * SIZE + x];
                            GameArea[y * SIZE + x] = 0;
                        }
                    }
                }
            }
        }

        public void MoveDown()
        {
            bool[] mask = new bool[SIZE * SIZE];
            for (int x = 0; x < SIZE; x++)
            {
                for(int y = SIZE - 1; y >= 0; y--)
                {
                    if (GameArea[y * SIZE + x] != 0 && y < SIZE - 1)
                    {
                        int ty = y + 1;
                        while (ty < SIZE - 1 && GameArea[ty * SIZE + x] == 0)
                            ty++;
                        if (GameArea[y * SIZE + x] == GameArea[ty * SIZE + x] && !mask[ty * SIZE + x])
                        {
                            GameArea[ty * SIZE + x] *= 2;
                            GameArea[y * SIZE + x] = 0;
                            mask[ty * SIZE + x] = true;
                        }
                        else if (ty - 1 != y)
                        {
                            if (GameArea[ty * SIZE + x] != 0)
                            {
                                GameArea[(ty - 1) * SIZE + x] = GameArea[y * SIZE + x];
                                GameArea[y * SIZE + x] = 0;
                            }
                            else
                            {
                                GameArea[ty * SIZE + x] = GameArea[y * SIZE + x];
                                GameArea[y * SIZE + x] = 0;
                            }
                        }
                        else if (GameArea[ty * SIZE + x] == 0)
                        {
                            GameArea[ty * SIZE + x] = GameArea[y * SIZE + x];
                            GameArea[y * SIZE + x] = 0;
                        }
                    }
                }
            }
        }
    }
}
