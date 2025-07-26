using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_2
{
    internal class MenuContext
    {
        public Dictionary<string, List<Menu>> menus; // Main,Order에 따라 메뉴 그룹 저장
        public Dictionary<string, List<Item>> items;
        public List<Item> cart;
        public double totalPrice;
        // waiting번호 추가 
        private int waitingNumber;


        // 생성자에서 초기화 해주기!
        public MenuContext()
        {
            menus = new Dictionary<string, List<Menu>>(); // 초기화만
            items = new Dictionary<string, List<Item>>();
            cart = new List<Item>();
            totalPrice = 0;
            InitializeMenuItems(); 
        }
        public void InitializeMenuItems() // 실제 항목
        {
            List<Menu> mainMenus = new List<Menu>
            {
               new Menu("Burgers", "앵거스 비프 통살을 다져만든 버거"),
               new Menu("Frozen Custard", "매장에서 신선하게 만드는 아이스크림"),
               new Menu ("Drinks", "매장에서 직접 만드는 음료"),
               new Menu("Beer","뉴욕 브루클린 브루어리에서 양조한 맥주")
            };
            List<Menu> orderMenus = new List<Menu>
            {
                new Menu("Order","장바구니를 확인 후 주문합니다"),
                new Menu("Cancel","진행중인 주문을 취소합니다."),
                new Menu("OrderList", "대기/완료 된 주문목록을 조회합니다.")
            };


            menus.Add("Main",mainMenus);
            menus.Add("Order", orderMenus);


            List<Item> burgerMenu = new List<Item>
            {
               new Item("ShackBurger",6.9, "앵거스 비프 통살을 다져만든 버거"),
               new Item("Smoke",8.9, "매장에서 신선하게 만드는 아이스크림"),
               new Item ("Shroom Burger",9.4, "매장에서 직접 만드는 음료"),
               new Item("CheeseBurger",5.4,"뉴욕 브루클린 브루어리에서 양조한 맥주")
            };
            List<Item> FrozenCustardMenu = new List<Item>
            {
                new Item("Frozen Custard Menu Item 1",1.4,"Frozen Custard Menu Item 1 설명"),
                new Item("Frozen Custard Menu Item 2",1,"Frozen Custard Menu Item 2 설명"),
                new Item("Frozen Custard Menu Item 3",1.6, "Frozen Custard Menu Item 3 설명"),
                new Item("Frozen Custard Menu Item 4", 2.1, "Frozen Custard Menu Item 4 설명")
            };
            List<Item> DrinksMenu = new List<Item>
            {
                new Item("Drinks Menu Item 1",1,"Drinks Menu Item 1 설명"),
                new Item("Drinks Menu Item 2",1,"Drinks Menu Item 2 설명")
            };
            List<Item> BeerMenu = new List<Item>
            {
                new Item("Beer Menu Item 1",3,"Beer Menu Item 1 설명"),
                new Item("Beer Menu Item 2",4,"Beer Menu Item 2 설명")
            };

            items.Add("Burgers", burgerMenu);
            items.Add("Frozen Custard", FrozenCustardMenu);
            items.Add("Drinks", DrinksMenu);
            items.Add("Beer", BeerMenu);

        }
        public List<Menu> GetMenus(string key) // 메뉴 리스트 반환
        {
            // 없을경우  빈리스트 반환(맨끝)
            return menus.ContainsKey(key) ? menus[key] : new List<Menu>(); // Menus에 키가 있는지 없는 지 확인하는 용도
        }
        public List<Item> GetMenuItems(string key) // 메뉴 리스트 반환
        {
            // 없을경우  빈리스트 반환(맨끝)
            return items.ContainsKey(key) ? items[key] : new List<Item>(); // Items에 키가 있는지 없는 지 확인하는 용도
        }
        public void AddToCart(Item menuItem)
        {
            // 장바구니(cart)에 같은 이름의 상품이 존재할 경우 : count만 증가시킴
            // 그렇지 않으면 새로 cart에 추가
            // item은 카드에서 하나 꺼냈을때 이름
            Item? existingItem = cart.Find(item => item.name == menuItem.name); // ? -> null도 들어갈 수 있음
            if (existingItem!=null)
            {
                existingItem.count++;
            }
            else {
                // menuItem으로 넣을 경우 같은 주소를 참조할 위험이 있음
                cart.Add(new Item(menuItem.name,menuItem.price,menuItem.description));
            }
            totalPrice += menuItem.price;
        }

        public void DisplayCart()
        {
            foreach(var item in cart) { 
                Console.WriteLine($"{item.name} | {item.price} | {item.description} | x {item.count}");
            }
        }
        public double GetTotalPrice()
        {
            return totalPrice;
        }
        // 대기번호
        public int GenerateOrderNumber()
        {
            waitingNumber++;
            return waitingNumber;
        }
        public void ClearCart()
        {
            cart.Clear();
            totalPrice = 0.0;
        }

    }
}
