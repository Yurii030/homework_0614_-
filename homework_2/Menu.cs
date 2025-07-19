using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_2
{
    internal class Menu
    {
        public string name;
        public string description;

        public Menu(string name, string description)
        {
            //Menu menu1 = new Menu("Burgers", "앵거스 비프 통살을 다져만든 버거");
            this.name = name;
            this.description= description;
        }
    }
}
