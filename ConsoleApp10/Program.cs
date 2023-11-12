using System;
using System.Collections.Generic;

namespace ConsoleApp10
{
    interface IStructureComponent
    {
        bool IsConstructed { get; set; }
    }

    interface IConstructionWorker
    {
        void Construct(Building building);
    }

    class Building
    {
        public Foundation Foundation { get; set; }
        public List<Wall> WallSections { get; set; }
        public Entrance Entrance { get; set; }
        public List<Window> WindowSections { get; set; }
        public Ceiling Ceiling { get; set; }

        public Building()
        {
            Foundation = new Foundation();
            WallSections = new List<Wall> { new Wall(), new Wall(), new Wall(), new Wall() };
            Entrance = new Entrance();
            WindowSections = new List<Window> { new Window(), new Window(), new Window(), new Window() };
            Ceiling = new Ceiling();
        }
    }

    class Foundation : IStructureComponent
    {
        public bool IsConstructed { get; set; }
    }

    class Wall : IStructureComponent
    {
        public bool IsConstructed { get; set; }
    }

    class Entrance : IStructureComponent
    {
        public bool IsConstructed { get; set; }
    }

    class Window : IStructureComponent
    {
        public bool IsConstructed { get; set; }
    }

    class Ceiling : IStructureComponent
    {
        public bool IsConstructed { get; set; }
    }

    class Builder : IConstructionWorker
    {
        public void Construct(Building building)
        {
            if (!building.Foundation.IsConstructed)
            {
                building.Foundation.IsConstructed = true;
            }
            else if (building.WallSections.Exists(w => !w.IsConstructed))
            {
                building.WallSections.Find(w => !w.IsConstructed).IsConstructed = true;
            }
            else if (!building.Entrance.IsConstructed)
            {
                building.Entrance.IsConstructed = true;
            }
            else if (building.WindowSections.Exists(w => !w.IsConstructed))
            {
                building.WindowSections.Find(w => !w.IsConstructed).IsConstructed = true;
            }
            else if (!building.Ceiling.IsConstructed)
            {
                building.Ceiling.IsConstructed = true;
            }
        }
    }

    class Foreman : IConstructionWorker
    {
        public void Construct(Building building)
        {
            Console.WriteLine("Building progress:");
            Console.WriteLine($"Foundation: {(building.Foundation.IsConstructed ? "constructed" : "not constructed")}");
            Console.WriteLine($"Walls: {building.WallSections.FindAll(w => w.IsConstructed).Count} out of 4 constructed");
            Console.WriteLine($"Entrance: {(building.Entrance.IsConstructed ? "installed" : "not installed")}");
            Console.WriteLine($"Windows: {building.WindowSections.FindAll(w => w.IsConstructed).Count} out of 4 installed");
            Console.WriteLine($"Ceiling: {(building.Ceiling.IsConstructed ? "constructed" : "not constructed")}");
        }
    }

    class ConstructionCrew
    {
        private List<IConstructionWorker> crewMembers;
        private Foreman crewLeader;

        public ConstructionCrew()
        {
            crewMembers = new List<IConstructionWorker>
            {
                new Builder(),
                new Builder(),
                new Builder()
            };
            crewLeader = new Foreman();
        }

        public void ConstructBuilding(Building building)
        {
            while (!building.Foundation.IsConstructed || building.WallSections.Exists(w => !w.IsConstructed) ||
                   !building.Entrance.IsConstructed || building.WindowSections.Exists(w => !w.IsConstructed) ||
                   !building.Ceiling.IsConstructed)
            {
                foreach (var worker in crewMembers)
                {
                    worker.Construct(building);
                    crewLeader.Construct(building);
                }
            }
            Console.WriteLine("Building construction completed!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Building myBuilding = new Building();
            ConstructionCrew crew = new ConstructionCrew();
            crew.ConstructBuilding(myBuilding);
            DisplayBuilding();
        }

        static void DisplayBuilding()
        {
            Console.WriteLine("    /\\");
            Console.WriteLine("   /  \\");
            Console.WriteLine("  /----\\");
            Console.WriteLine("  |    |");
            Console.WriteLine("  | [] |");
            Console.WriteLine("  |    |");
            Console.WriteLine(" _|____|_");
        }
    }
}
