using conrpggame.Game;
using conrpggame.Adventures.Interfaces;
using Moq;
using System;
using conrpggame.Entities.Interfaces;
using Shouldly;
using Xunit;

namespace RpgCallUnitTest
{
    public class GameServicesUnitTests
    {
        private GameService gameService;


        private Mock<IAdventrueService> mockAdventureService = new Mock<IAdventrueService>();
        private Mock<ICharacterService> mockCharacterService = new Mock<ICharacterService>();

        public GameServicesUnitTests()
        {
            gameService = new GameService(mockAdventureService.Object, mockCharacterService.Object);
        }
                     
        [Fact]
        public void gametest()
        {
            //Arrang安排(排列) //Act行為(動作) //Assert斷言(樹立)
            gameService.StartGame().ShouldBe(true);
        }
        [Fact]
        public void Meth_Retrue_Excepton()
        {
            //Arrang安排(排列) 
            //模擬冒險開始之後得異常狀況回傳
            mockAdventureService.Setup(_ => _.GetInitalAdventrue()).Throws(new Exception());
            
            //Act行為(動作) 
            //Assert斷言(樹立)
            //當執行遊戲時應該出現的異常
            Should.Throw<Exception>(() => gameService.StartGame());
        }
    }
}
