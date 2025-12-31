public static class NodeHelper
{
    public static UserInterface FindUserInterface(this Node node, int player)
    {
        return node
            .GetTree()
            .GetRoot()
            .GetChild(0)
            .GetNode<UserInterface>("UserInterfaceP" + player)!;
    }
}
