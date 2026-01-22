public static class NodeHelper
{
    public static UserInterface FindUserInterface(this Node node, int player)
    {
        return node
            .GetTree()
            .GetRoot()
            .GetNode<World>("World")
            .GetNode<UserInterface>("UserInterfaceP" + player)!;
    }
}
