using System;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PofRune
{   

    internal class Program
    {   // 플레이어 정보 
        // 레벨, 이름, 룬, 공격력, 방어력, 체력, 골드
        private static int level;
        private static string name;
        private static string rune;
        private static int atk;
        private static int def;
        private static int hp;
        private static int gold;

        private static int addatk;
        private static int adddef;

        // 상점 아이템 모음
        private static string[] itemNames = { "뱀 룬", "직검", "당파", "조선 환도", "지 찰갑", "가죽 찰갑", "철 찰갑" };
        private static int[] itemNum = { 0, 0, 0, 0, 1, 1, 1 };
        private static int[] itemValues = { 9, 2, 4, 6, 1, 3, 6 };
        private static string[] itemDesc = { "뱀 문자가 들어간 룬이다.", "날이 휘지 않은 직선 철 검이다.", "조선시대 삼지창", "매우 튼튼하고 날이 서있다.", "종이로 만든 갑주", "가죽으로 만든 갑주", "철조각을 겹쳐 만든 갑주", };
        private static int[] itemPrice = { 1500, 400, 500, 1100, 500, 600,1200,};

        private static List<int> inventory = new List<int>();
        private static List<int> equipList = new List<int>();



        //Main UI 모음
        static void Main(string[] ui)
        {
            SetData();
            DisplayMainUI();
        }

        // 플레이어 초기값
        static void SetData()
        {
            level = 1;
            name = "이름없는자";
            rune = "토끼";
            atk = 2;
            def = 2;
            hp = 30;
            gold = 1500;
        }

        //  스타트 메인UI
        static void DisplayMainUI()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("---당신은 룬 세상에 떨어졌습니다.---");
            Console.WriteLine();
            Console.WriteLine("정면에는 동굴(던전)이 보인다 정면 이외에는 어둠뿐이다.");
            Console.WriteLine("당신은 던전에 들어가야할 운명이다. 선택권은 없다. 죽든가 살든가..");
            Console.WriteLine();
            Console.WriteLine("던전에 들어가기 전에 준비활동을 할 수 있다.");
            Console.WriteLine("1. 상태확인");
            Console.WriteLine("2. 가방확인");
            Console.WriteLine("3. 상점");
            Console.WriteLine();
            Console.Write("1~3번 행동중에 선택하세요 = ");
            string result = Console.ReadLine();
            Console.WriteLine();

            //int result = CheckInput(1, 3);


            switch (result)
            {
                case "1":
                    PlayerStateUI();
                    break;

                case "2":
                    PlayerbagUI();
                    break;

                case "3":
                    PlayerShopUI();
                    break;

                default:
                    Console.WriteLine("당신은 법칙을 어겼어!!!!!!");
                    Thread.Sleep(1500); // 1초 대기
                    Console.WriteLine("[어디선가 날카로운 발톱이 당신을 할퀸다!]");
                    for (int i = 0; i < 3; i++)// 3초까지 (".")반복
                    {
                        Thread.Sleep(1500); // 1초 대기
                        Console.WriteLine("죽어!");

                    }
                    Thread.Sleep(1500);
                    Console.WriteLine("잘못된 선택을 하여 당신은 사망합니다...");
                    Thread.Sleep(1500); // 1초 대기
                    Console.WriteLine("-------END-------");
                    break;
            }

        }

        // 상태창
        static void PlayerStateUI()
        {
            Console.Write("상태창 확인중");
            for (int i = 0; i < 3; i++)// 3초까지 (".")반복
            {
                Thread.Sleep(1000); // 1초 대기
                Console.Write(".");
            }
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, Console.CursorTop); // 커서를 현재 줄의 맨 앞으로 이동
            Console.Write(new string(' ', 20));              // '공백' 덮어서 문구 삭제 20=공백 갯수
            Console.SetCursorPosition(0, Console.CursorTop); // 다시 커서를 맨 앞으로 이동
            Console.Write("◆완료◆");

            Thread.Sleep(1000);
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', 20));
            Console.SetCursorPosition(0, Console.CursorTop);

            Console.WriteLine("-상태창-");
            Console.WriteLine($"이름  :{name}");
            Console.WriteLine($"Lv.   :{level:D2}");
            Console.WriteLine($"Rune  :{rune}");
            Console.WriteLine(addatk == 0 ? $"공격력:{atk}" : $"공격력:{atk}(+{addatk})"); // 추가 공격력
            Console.WriteLine(adddef == 0 ? $"방어력:{def}" : $"방어력:{def}(+{adddef})"); // 추가 방어력
            Console.WriteLine($"체력  :{hp}");
            Console.WriteLine($"골드  :{gold}G");


            Console.WriteLine();
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, 0);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;
            }
        }

        // 아이템창
        static void PlayerbagUI()
        {
            Console.Write("가방 열고 닫는중");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.Clear();
            Console.WriteLine("[ 가방 속 물품 ]");

            for (int i = 0; i < inventory.Count; i++)
            {
                int targetItem = inventory[i];
                string displayequip = equipList.Contains(i) ? "[E]" : ""; // 
                Console.WriteLine($"- {i + 1} {displayequip} {itemNames[targetItem]}  | {(itemNum[inventory[i]] == 0 ? "공격력" : "방어력")} +{itemValues[targetItem]} | {itemDesc[targetItem]}");
            }
            Console.WriteLine();
            Console.WriteLine("1 : 장착 관리");
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;

                case 1:
                    DisplayEquipUI();
                    break;
            }
        }

        //------ 임시UI
        static void inUI()
        {
            Console.WriteLine("테스트");
            Console.WriteLine();
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, inventory.Count);

            switch (result)
            {
                case 0:
                    PlayerbagUI();
                    break;
            }
        }
        //------ 임시 UI


        // 가방 아이템 장착
        static void DisplayEquipUI()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("장착 할 아이템을 선택해 주세요.");
            Console.WriteLine();
            Console.WriteLine("[ 아이템 목록 ]");
            Console.WriteLine();

            for (int i = 0; i < inventory.Count; i++)
            {
                int targetItem = inventory[i];
                string displayequip = equipList.Contains(i) ? "[E]" : ""; // 
                Console.WriteLine($"- {i + 1} {displayequip} {itemNames[targetItem]}  | {(itemNum[inventory[i]] == 0 ? "공격력" : "방어력")} +{itemValues[targetItem]} | {itemDesc[targetItem]}");
            }

            Console.WriteLine();
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("장착할 아이템의 번호를 적어주세요.");

            int result = CheckInput(0, inventory.Count);

            switch (result)
            {
                case 0:
                    PlayerbagUI();
                    break;

                default: // 장착로직 구현

                    int targetItem = result - 1;
                    bool isEquipped = equipList.Contains(targetItem);

                    if (isEquipped)
                    {  //장착이 되어있다면 [E] 해제 + 추가 공격력 방어력 extra값 -
                        equipList.Remove(targetItem);
                        int itemIdx = inventory[targetItem];
                        if (itemNum[itemIdx] == 0)
                        {
                            addatk -= itemValues[itemIdx];
                        }
                        else
                        {
                            adddef -= itemValues[itemIdx];
                        }
                    }
                    else
                    {//장착이 안되어있으면 [E] 장착 + 추가 공격력 방어력 extra값 +
                        equipList.Add(targetItem);
                        int itemIdx = inventory[targetItem];
                        if (itemNum[itemIdx] == 0)
                        {
                            addatk += itemValues[itemIdx];
                        }
                        else
                        {
                            adddef += itemValues[itemIdx];
                        }
                    }
                    break;

            }
            PlayerbagUI();

        }

        //상점
        static void PlayerShopUI()
        {
            Console.Write("상점으로 빨려들어 가는중");
            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', 50));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine("◆공허의 상점◆");
            Console.WriteLine();
            Console.WriteLine("물건이 공중에 둥둥 떠다닌다.");
            Thread.Sleep(1000);
            Console.WriteLine("[물건에 가격표가 붙어있다 구입하고싶으면 돈을 지불하라..]");
            Thread.Sleep(1000);

            Console.WriteLine();
            Console.WriteLine("[ 보유골드 ]");
            Console.WriteLine($"{gold}G");// 보간문자열 사용
            Console.WriteLine();
            Console.WriteLine("[ 아이템 목록 ]");

            for (int i = 0; i < itemNames.Length; i++)
            {
                string displayPrice = inventory.Contains(i) ? "구매완료" : $"{itemPrice[i]}G";
                Console.WriteLine($"- {i + 1} {itemNames[i]} | {(itemNum[i] == 0 ? "공격력" : "방어력")} +{itemValues[i]} | {itemDesc[i]} | {displayPrice}"); // 에러 걸리면 공격력Num0, 방어력Num1 수정필요
            }

            Console.WriteLine();
            Console.WriteLine("1 : 아이템 구매");
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;

                case 1:
                    PlayerBuyUI();
                    break;
            }
        }

        // 상점 구매
        static void PlayerBuyUI()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("아이템을 구매할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[ 보유골드 ]");
            Console.WriteLine($"{gold}G");// 보간문자열 사용
            Console.WriteLine();
            Console.WriteLine("[ 아이템 목록 ]");
            //아이템을 써주어야한다.

            for (int i = 0; i < itemNames.Length; i++)
            {

                //구매 완료 시 금액이 아니라 구매완료가 떠야한다.
                string displayPrice = inventory.Contains(i) ? "구매완료" : $"{itemPrice[i]}G";
                // 앞에 숫자가 나오게 해야하기때문에 { i+1 }
                Console.WriteLine($"- {i + 1} {itemNames[i]}  | {(itemNum[i] == 0 ? "공격력" : "방어력")} +{itemValues[i]} | {itemDesc[i]} | {displayPrice}");
            }
            Console.WriteLine();
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, itemNames.Length); // 아이템 길이만큼..

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;

                default: // 0 이 아닌 다른값이 들어온다면 진입

                    if (inventory.Contains(result - 1)) // 이미 샀다면, 구매완료표시가 이미 있다면
                    {                                   // 이 구매완료표시가 있다는 뜻은 List에 들어가있다면! 으로 해석가능
                        Console.WriteLine(" ===  이미 구매 완료했습니다 === ");
                        Console.WriteLine("enter를 입력해주세요.");
                        Console.ReadLine();
                    }
                    //구매 완료 표시가 떠야함
                    else // 살 수 있다! 구매 표시가 없을때!
                    {
                        if (gold >= itemPrice[result - 1]) // 돈이 많을경우 (구매가능)
                        {
                            Console.WriteLine(" === 구매를 완료했습니다. === ");
                            Console.WriteLine("enter를 입력해주세요.");
                            Console.ReadLine();

                            gold -= itemPrice[result - 1]; // 금액 차감
                            inventory.Add(result - 1); // 인벤토리리스트에 배열 추가
                        }
                        else
                        {
                            Console.WriteLine(" === 골드가 부족합니다. === ");
                            Console.WriteLine("enter를 입력해주세요.");
                            Console.ReadLine();

                        }
                    }

                    PlayerBuyUI();
                    break;

            }



        }
        static int CheckInput(int min, int max)
        {
            //int result = int.Parse(Console.ReadLine());
            int result; // 값을 담을 변수선언

            while (true)
            {

                bool isNumber = int.TryParse(Console.ReadLine(), out result);
                if (isNumber)
                {
                    if (result >= min && result <= max) { return result; }

                }
                Console.WriteLine("잘못된 입력입니다.");
            }
        }


    }

}
