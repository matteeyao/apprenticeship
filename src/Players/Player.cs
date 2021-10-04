using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Players
{
    public abstract class Player
    {
        private string _name;
        private string _marker;

        public string GetName()
        {
            return _name;
        }

        public string GetMarker()
        {
            return _marker;
        }

        public void SetName(string name)
        {
            this._name = name;
        }

        public void SetMarker(string marker)
        {
            this._marker = marker;
        }

        public abstract int Move(string[] spaces);
    }
}