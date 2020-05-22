using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;


namespace lab5
{
    /// <summary>
    /// Основной класс
    /// </summary>

    class MainApp

    {
        /// <summary>
        /// Точка входа
        /// </summary>

        static void Main()
        {
            // Создаём новую фабрику частиц, объявляем массив двигающихся частиц
            ParticleFactory factory = new ParticleFactory();
            List<MovingParticle> moving_particles=new List<MovingParticle>();

            // Заполняем массив двигающихся частиц
            moving_particles.Add(new MovingParticle(4, 5, 10,new int[]{1,3},'A',factory));
            moving_particles.Add(new MovingParticle(0, 0, 5, new int[] { 5, 9 }, 'A', factory));
            moving_particles.Add(new MovingParticle(7, -9, 3, new int[] {7, 3 }, 'B', factory));
            moving_particles.Add(new MovingParticle(1, 5, 4, new int[] { 7, 5 }, 'A', factory));
            moving_particles.Add(new MovingParticle(4, 1, 2, new int[] { 9, 3 }, 'B', factory));
            moving_particles.Add(new MovingParticle(1, 1, 1, new int[] { 9, 9 }, 'B', factory));

            foreach (MovingParticle moving_particle in moving_particles) 
            {
                moving_particle.move();
            }
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Фабрика частиц. Хранит массив легковесов.
    /// </summary>

    class ParticleFactory

    {
        private Dictionary<char, Particle> _particles = new Dictionary<char, Particle>();

        /// <summary>
        /// Функция, возвращающая существующий, или добавляющая новый легковес
        /// </summary>
        /// <param name="key символьный код частицы"></param>
        /// <returns>Легковес, соответствующий key</returns>>
        public Particle GetParticle(char key)
        {
            // Uses "lazy initialization"

            Particle particle = null;
            if (_particles.ContainsKey(key))
            {
                particle = _particles[key];
            }
            else

            {
                switch (key)
                {
                    case 'A': particle = new Particle('A'); break;
                    case 'B': particle = new Particle('B'); break;
                }
                _particles.Add(key, particle);
                Console.WriteLine("Добавлена новая частица типа "+ key +", изображение частицы было установлено");
            }
            return particle;
        }
    }

    /// <summary>
    /// Абстрактный класс частицы
    /// </summary>

    abstract class AbstractParticle

    {
        /// <summary>
        /// Функция для передвижения частицы
        /// </summary>
        public abstract void move();
    }

    /// <summary>
    /// Класс, хранящий внутреннее (статичное) поле частицы
    /// </summary>

    class Particle : AbstractParticle

    {
        /// <summary>
        /// Изображение частицы
        /// </summary>
        private Image img;

        public Image Img 
        {
            get; set;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Particle(char c) 
        {
            switch (c)
            {
                case 'A': this.img = Image.FromFile("A.jpg"); ; break;
                case 'B': this.img = Image.FromFile("B.jpg"); ; break;
            }
        }

        public override void move()
        {
            Console.WriteLine("Частица не двигается");
        }
    }

    /// <summary>
    /// Класс двигающейся частицы. Содержит внешние поля
    /// </summary>

    class MovingParticle : AbstractParticle

    {
        private int x, y;
        private int velocity;
        private int[] vector;
        private Particle particle;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public MovingParticle(int x, int y, int velocity,int[] vector, char c, ParticleFactory factory)
        {
            this.x = x;
            this.y = y;
            this.velocity = velocity;
            this.vector = vector;
            particle = factory.GetParticle(c);
        }
        /// <summary>
        /// Функция движения частицы
        /// </summary>
        public override void move()
        {
            Console.WriteLine("Частица сдвинулась на"+ vector[0]*velocity+ "по координате x, и на "+ vector[1] * velocity+ "по координате y.");
        }
    }
}
