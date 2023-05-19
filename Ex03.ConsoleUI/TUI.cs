using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class TUI
    {
        private GarageManeger m_myGarage;

        public void runGarage()
        {
            Console.WriteLine("Wellcome to the best garage in Jaffa!!");
            Console.WriteLine(String.Format(@"Right now the garage is empty(just because we working so fast) would you like to put your vehicle inside?"));
        }
    }
}
