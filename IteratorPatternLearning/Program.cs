using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorPatternLearning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Armory armory = new Armory();
            Hero hero = new Hero();
            hero.SeeWeapons(armory);

            Console.Read();
        }
    }
    
    class Hero
    {
        public void SeeWeapons(Armory armory)
        {
            IWeaponIterator iterator = armory.CreateNumerator();
            while (iterator.HasNext())
            {
                Weapon weapon = iterator.Next();
                Console.WriteLine($"Имя оружия: {weapon.Name} | Урон: {weapon.Damage} | Цена: {weapon.Cost} золота");
            }
        }
    }
    //iterator
    interface IWeaponIterator
    {
        bool HasNext();
        Weapon Next();
    }

    //aggregate
    interface IWeaponNumerable
    {
        IWeaponIterator CreateNumerator();
        int Count { get; }
        Weapon this[int index] { get; }
    }
    class Weapon
    {
        public string Name { get; set; }
        public int Damage { get; set; }

        public int Cost { get; set; }
    }

    //concrete aggregator
    class Armory : IWeaponNumerable
    {
        private Weapon[] weapons;
        public Armory()
        {
            weapons = new Weapon[]
            {
            new Weapon{Name="Меч стражника", Damage = 12, Cost = 5},
            new Weapon {Name="Заточенный клинок палладина", Damage = 16, Cost = 10},
            new Weapon {Name="Бич рассвета", Damage = 150, Cost = 1000}
            };
        }
        public int Count
        {
            get { return weapons.Length; }
        }

        public Weapon this[int index]
        {
            get { return weapons[index]; }
        }
        public IWeaponIterator CreateNumerator()
        {
            return new ArmoryNumerator(this);
        }
    }

    //concrete iterator
    class ArmoryNumerator : IWeaponIterator
    {
        IWeaponNumerable _aggregate;
        int index = 0;
        public ArmoryNumerator(IWeaponNumerable aggregate)
        {
            _aggregate = aggregate;
        }
        public bool HasNext()
        {
            return index < _aggregate.Count;
        }

        public Weapon Next()
        {
            return _aggregate[index++];
        }
    }
}
