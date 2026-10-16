using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Datalayer
{
    internal class DataHandler
    {
        string filepath = @"animals.txt";
        public void Add(Animal a)
        {
            using (StreamWriter sw = new StreamWriter(filepath, true))
            {
                sw.WriteLine($"{a.AnimalID} | {a.Name} | {a.Species} | {a.Age} | {a.RecoveryScore} | {a.Status} | {a.HousingUnit}");
            }
        }
    }
}