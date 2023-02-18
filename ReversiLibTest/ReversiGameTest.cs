namespace ReversiLibTest
{
    [TestClass]
    public class ReversiGameTest
    {
        [TestMethod]
        public void TestFieldCreation()
        {
            var game = new ReversiGame(7, 2);
            Assert.IsNotNull(game);
            Assert.IsNotNull(game.Field);
            Assert.AreEqual(game.Height, 7);
            Assert.AreEqual(game.Width, 2);
            Assert.IsNotNull(game.Field[0][0]);
        }
    }
}