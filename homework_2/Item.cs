using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_2
{
    internal class Item
    {
        public string name;
        public double price;
        public string description;
        // 수량 필드 추가
        public int count { get; set; } = 1; // 기본 수량은 1로 시작

        public Item(string name, double price, string description)
        {
            //Menu menu1 = new Menu("Burgers", "앵거스 비프 통살을 다져만든 버거");
            this.name = name;
            this.price = price;
            this.description = description;
        }
    }
}
