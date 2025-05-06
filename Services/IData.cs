using System;
using System.Collections.Generic;
namespace Demo
{
    internal interface IData
    {
        public string Chemin { get; }
        void Charger();
        void Enregistrer();
        
    }
}
