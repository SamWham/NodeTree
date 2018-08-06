using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeTree;
using System;


namespace UnitTest
{
    [TestClass]
    public class TreeUnitTest
    {
        [TestMethod]
        public void GreatestDepth_SimpleShouldFindDepth()
        {
            // arrange  
            NodeTree<string> root = new NodeTree<string>(Guid.NewGuid(), "test");
            root.SpawnChild(Guid.NewGuid(), "test2");
            root.SpawnChild(Guid.NewGuid(), "test3");
            root.SpawnChild(Guid.NewGuid(), "test4").SpawnChild(Guid.NewGuid(), "test5").SpawnChild(Guid.NewGuid(), "test6");

            // act  
            var actual = root.GreatestDepth(root);

            // assert  
            Assert.AreEqual(4, actual);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(7)]
        [DataRow(11)]
        [DataRow(17)]
        public void GreatestDepth_ShouldFindDepthRandom(int depth)
        {
            // arrange 
            NodeTree<int> root = new NodeTree<int>(Guid.NewGuid(), 6);
            root.GenrateTree(depth, root, 2324);

            // act  
            var actual = root.GreatestDepth(root);

            // assert  
            Assert.AreEqual(depth, actual);
        }


        [TestMethod]
        public void FindNode_ShouldReturnCorrectNodeSimple()
        {
            // arrange  
            NodeTree<bool> root = new NodeTree<bool>(Guid.NewGuid(), true);
            root.SpawnChild(Guid.NewGuid(), true);
            root.SpawnChild(Guid.NewGuid(), false);
            Guid targetGuid = Guid.NewGuid();
            var targetNode = root.SpawnChild(Guid.NewGuid(), true).SpawnChild(Guid.NewGuid(), false).SpawnChild(targetGuid, false); ;

            // act  
            var result = root.FindNode(root, targetGuid);

            // assert  
            Assert.AreEqual(targetNode, result);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(14)]
        public void FindNode_ShouldReturnCorrectNodeRandom(int depth)
        {
            // arrange 
            NodeTree<object> root = new NodeTree<object>(Guid.NewGuid(), new { testInt = 108, text = "test" });
            root.GenrateTree(depth, root, new { text = "test2" });
            NodeTree<object> randomNode = root.SelectRandomNode(root);

            // act  
            var actual = root.FindNode(root, randomNode.GetNodeID());

            // assert  
            Assert.AreEqual(randomNode, actual);
        }
    }
}
