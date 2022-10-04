using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotBrothersSafe
{

    enum Orientation
    {
        Horizontal,
        Vertical
    }

    class Game
    {
        int size;

        Orientation[,] handlers;
        static Random rnd = new Random();

        public Game(int size)
        {
            this.size = size;
            handlers = new Orientation[size, size];
        }

        public void Start()
        {
            Orientation startOrientation;
            startOrientation = (rnd.Next(0, 1) == 0) ? Orientation.Horizontal : Orientation.Vertical;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    handlers[x, y] = startOrientation;
                }
            }
        }

        public void RandomStep()
        {
            int position = rnd.Next(0, size * size - 1);
            TurnHandlers(position);
        }

        // Ориентация
        public Orientation GetOrientation(int position)
        {
            int x, y;
            PositionToCoords(position, out x, out y);
            return handlers[x, y];
        }

        // Поворот рукоятки
        private void TurnHandler(int x, int y)
        {
            if (handlers[x, y] == Orientation.Horizontal)
            {
                handlers[x, y] = Orientation.Vertical;
            }
            else
                handlers[x, y] = Orientation.Horizontal;
        }

        // Поворот рукоятей
        public void TurnHandlers(int position)
        {
            int x, y;

            PositionToCoords(position, out x, out y);
            TurnHandler(x, y);

            for (int i = 0; i < size; i++)
            {
                if (i == y)
                    continue;
                TurnHandler(x, i);
            }
            for (int i = 0; i < size; i++)
            {
                if (i == x)
                    continue;
                TurnHandler(i, y);
            }
        }

        // Проверка положения рукояти
        public bool Check()
        {
            Orientation firstHandler = handlers[0, 0];
            for (int x = 0; x < size; x++)
            {
                for (int y = 1; y < size; y++)
                {
                    if (firstHandler != handlers[x, y])
                        return false;
                }
            }
            return true;
        }

        // Координаты
        private void PositionToCoords(int position, out int x, out int y)
        {
            if (position < 0)
            {
                position = 0;
            }

            if (position > size * size - 1)
            {
                position = size * size - 1;
            }

            x = position % size;
            y = position / size;
        }
    }
}
