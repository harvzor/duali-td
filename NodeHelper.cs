public static class NodeHelper
{
    public static World FindWorld(this Node node)
    {
        return node
            .GetTree()
            .GetRoot()
            .GetNode<World>("World");
    }
    
    public static UserInterface FindUserInterface(this Node node, int player)
    {
        return node
            .FindWorld()
            .GetNode<UserInterface>("UserInterfaceP" + player)!;
    }
}
