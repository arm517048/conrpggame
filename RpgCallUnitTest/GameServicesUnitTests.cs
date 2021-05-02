using conrpggame.Adventures;
using conrpggame.Adventures.Interfaces;
using conrpggame.Entities.Interfaces;
using conrpggame.Entities.Models;
using conrpggame.Game;
using conrpggame.Utilities.Interfaces;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace RpgCallUnitTest
{
    public class GameServicesUnitTests
    {
        private GameService gameService;


        private readonly Mock<IAdventrueService> mockAdventureService = new Mock<IAdventrueService>();
        private readonly Mock<ICharacterService> mockCharacterService = new Mock<ICharacterService>();
        private readonly Mock<IMessageHandler> mockmessageHameler = new Mock<IMessageHandler>();

        public GameServicesUnitTests()
        {
            gameService = new GameService(mockAdventureService.Object, mockCharacterService.Object,mockmessageHameler.Object);
        }
                     
        [Fact]
        public void Meth_Retrue_Excepton()  //單元測試:有異常狀況
        {
            //Arrang安排(排列) 
            //模擬冒險開始之後得異常狀況回傳
            mockAdventureService.Setup(_ => _.GetInitalAdventrue()).Throws(new Exception());

            //Act行為(動作) 
            //Assert斷言(樹立)
            //當執行遊戲時應該出現的異常
            Should.Throw<Exception>(() => gameService.StartGame());
        }
        [Fact]
        public void Meth_Should_Reture_False_In_When_No_Characters_In_Range()   //單元測試:回傳Flase的狀況
        {
            //Arrang安排(排列) 
            //模擬冒險開始之後得異常狀況回傳，模擬有標題
            mockAdventureService.Setup(_ => _.GetInitalAdventrue()).Returns(new Adventrues { Title = "testTitle", Description = "testDescription" });
            //模擬述職的最大最小都是整數，並回傳到Character(實施偵錯測試數值應為0
            mockCharacterService.Setup(_ => _.GetCharactersInRange(It.IsAny<int>(),It.IsAny<int>())).Returns(new List<Character>());

            //Act行為(動作) 
            var MethodReturn = gameService.StartGame();
            //Assert斷言(樹立)
            //當執行遊戲時應該出現的異常
            MethodReturn.ShouldBeFalse();

        }

        [Fact]
        public void Meth_Should_Reture_Ture_In_When_There_Are_Characters_In_Range() //單元測試:當回傳ture的狀況
        {
            //Arrang安排(排列) 
            //建立模擬人物
            var characterList = new List<Character>
            {
                new Character{ Name = "conan" },
                new Character{ Name = "todd" }
            };

            //模擬冒險開始之後得異常狀況回傳，模擬有標題
            mockAdventureService.Setup(_ => _.GetInitalAdventrue()).Returns(new Adventrues { Title = "testTitle", Description = "testDescription" });
            //模擬述職的最大最小都是整數，並回傳到Character(實施偵錯測試數值應為0
            mockCharacterService.Setup(_ => _.GetCharactersInRange(It.IsAny<int>(), It.IsAny<int>())).Returns(characterList);
            //應設立MessageHandler所以原本的  Console.SetIn(new StringReader("0"))  ;可以移除改成下列
            mockmessageHameler.Setup(_ => _.Read()).Returns("0");

            //Act行為(動作) 
            var MethodReturn = gameService.StartGame();
            //Assert斷言(樹立)
            //當執行遊戲時應該出現的異常
            MethodReturn.ShouldBeTrue();
        }
        [Fact]
        public void Meth_Should_Throw_Exception_When_CharacterInput_Not_A_Number() //單元測試:當回傳不是數字
        {
            //Arrang安排(排列) 
            //建立模擬人物
            var characterList = new List<Character>
            {
                new Character{ Name = "tony" },
                new Character{ Name = "todd" }
            };
            //模擬冒險開始之後得異常狀況回傳，模擬有標題
            mockAdventureService.Setup(_ => _.GetInitalAdventrue()).Returns(new Adventrues { Title = "testTitle", Description = "testDescription" });
            //模擬述職的最大最小都是整數，並回傳到Character(實施偵錯測試數值應為0
            mockCharacterService.Setup(_ => _.GetCharactersInRange(It.IsAny<int>(), It.IsAny<int>())).Returns(characterList);
            //同 Meth_Should_Reture_Ture_In_When_There_Are_Characters_In_Range()
            mockmessageHameler.Setup(_ => _.Read()).Returns("Z");

            //Act行為(動作) 
            //Assert斷言(樹立)
            //當執行遊戲時應該出現的異常
            Should.Throw<Exception>(() => gameService.StartGame());
        }
    }
}
