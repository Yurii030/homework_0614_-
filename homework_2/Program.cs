using System.Collections.Generic;
using System.ComponentModel.Design;

namespace homework_2
{

        internal class Program
    {
        private static MenuContext menuContext;
        static void Main(string[] args)
        {
            menuContext = new MenuContext();
            DisplayMainMenu();
            HandleMainMenuInput(); // 사용자 입력을 받도록 설정

        }
        static void DisplayMainMenu()  // 수정사항 _ 해당메뉴 상품의 상품목록 조회 및 출력
        {
            Console.WriteLine("SHACK SHACK BURGER 에 오신걸 환영합니다.");
            Console.WriteLine("아래 메뉴판을 보시고 메뉴를 골라 입력해주세요");
            Console.WriteLine("[ SHACK SHACK MENU ]");
            List<Menu> mainMenus = menuContext.GetMenus("Main"); 
            int nextNum = PrintMenu(mainMenus,1); 
            Console.WriteLine("[ORDER MENU]");
            List<Menu> orderMenus= menuContext.GetMenus("Order");
            PrintMenu(orderMenus, nextNum);
        }
        static int PrintMenu(List<Menu> menus,int num)
        {
            for(int i = 0; i < menus.Count; i++,num++)
            {
                Console.WriteLine($"{num}. {menus[i].name}   |  {menus[i].description}");
            }
            return num;
        }
        static void HandleMainMenuInput() // 사용자의 숫자 입력을 받아 선택된 메뉴에 따라 동작을 분기
        {
            Console.WriteLine("\n 메뉴를 선택하세요 : ");
            int userNum = int.Parse(Console.ReadLine());
            List<Menu> mainMenus = menuContext.GetMenus("Main");
            List<Menu> orderMenus = menuContext.GetMenus("Order");

            int mainMenuSize = mainMenus.Count;
            int orderMenuSize = orderMenus.Count;
            // 입력값이 메인 메뉴 범위일 경우 
            if (userNum > 0 && userNum <= mainMenuSize)
            {
                DisplayMenu(mainMenus[userNum-1]);//세부메뉴
            }
            // 입력값이 주문메뉴 메뉴 범위 일 경우
            else if (userNum <= mainMenuSize+orderMenuSize)
            {
                int orderIndex = userNum - mainMenuSize;
                if (orderIndex == 1)
                {
                    DisplayOrderMenu();
                }
                else if (orderIndex == 2)
                {
                    Console.WriteLine("해당기능은 아직 준비중입니다.");
                    DisplayMainMenu();
                    HandleMainMenuInput();
                }
                else if (orderIndex == 3)
                {
                    Console.WriteLine("해당기능은 아직 준비중입니다.");
                    DisplayMainMenu();
                    HandleMainMenuInput();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해 주세요");
                    DisplayMainMenu();
                    HandleMainMenuInput();
                }

            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해 주세요");
                DisplayMainMenu();
                HandleMainMenuInput();
            }
        }

        static void DisplayMenu(Menu menu)  // 추가 _ 세부메뉴 추가
        {
            Console.WriteLine($"\n[{ menu.name}MENU ]");
            List<Item> items = menuContext.GetMenuItems(menu.name); 
            PrintMenuItems(items);
            HandleMenuItemInput(items);
        }
        static void PrintMenuItems(List<Item> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                int num = i + 1;
                Console.WriteLine($"{num}.{items[i].name} | {items[i].price}   |  {items[i].description}");
            }
        }
        static void HandleMenuItemInput(List<Item> items)
        {
            int Inputnum = int.Parse(Console.ReadLine());
            if(1<=Inputnum &&  Inputnum <= items.Count)
            {
                // 세부항목(메뉴) 받아온후 장바구니에 담을 건지 확인.
                Item selectedItem = items[Inputnum-1];
                DisplayConfirmation(selectedItem);
            }
            else
            {
                // 안내메시지 출력 후 재귀호출로 다시 입력 유도
                Console.WriteLine("잘못된 입력입니다. 다시입력해주세요");
                HandleMenuItemInput(items); // 
            }
        }
        // 장바구니에 담을 건지 묻는 것.
        static void DisplayConfirmation(Item item)
        {
            Console.WriteLine($"{item.name} | {item.price} | {item.description}");
            Console.WriteLine("위 메뉴를 장바구니에 추가하시겠습니까?");
            Console.WriteLine("1.확인   2. 취소 ");
            HandleConfirmationInput(item);

        }
        static void HandleConfirmationInput(Item item)
        {
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    menuContext.AddToCart(item);
                    Console.WriteLine("장바구니에 추가 되었습니다");
                    DisplayMainMenu();
                    HandleMainMenuInput(); // 사용자한테 메인메뉴 선택받기
                    break;
                case 2:
                    Console.WriteLine("취소되었습니다");
                    DisplayMainMenu();
                    HandleMainMenuInput();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                    HandleConfirmationInput(item);
                    break;

            }
        }
        static void DisplayOrderMenu()
        {
            Console.WriteLine("아래와 같이 주문 하시겠습니까?");
            menuContext.DisplayCart();
            Console.WriteLine("[ Total ]");
            Console.WriteLine($" W {menuContext.GetTotalPrice()}");
            Console.WriteLine();
            Console.WriteLine("1. 주문 / 2. 메뉴판");
            HandleOrderMenuInput();
        }
        static void HandleOrderMenuInput()
        {
            Console.WriteLine("");
            int inputNum = int.Parse(Console.ReadLine());
            if (inputNum == 1)
            {
                DisplayOrderComplete();
                //Console.WriteLine("주문완료"); // 5단계에서 구현
            }
            else if(inputNum == 2)
            {
                DisplayMainMenu();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 주문해주세요");
                DisplayOrderMenu();
            }
        }
        static void DisplayOrderComplete()
        {

            Console.WriteLine("주문이 완료되었습니다!");
            Console.WriteLine("");

            int number = menuContext.GenerateOrderNumber();
            Console.WriteLine($"대기 번호는 [{number}]번 입니다.");
            Console.WriteLine("( 3초 후 메뉴판으로 돌아갑니다.)");

            ResetCartAndDisplayMainMenu();

        }
        static void ResetCartAndDisplayMainMenu()
        {
            menuContext.ClearCart();
            Thread.Sleep(3000);
            DisplayMainMenu();
            HandleMainMenuInput();
        }


    }
}
